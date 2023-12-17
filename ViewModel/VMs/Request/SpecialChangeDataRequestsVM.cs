﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.ObjectModel;

using ViewModel.Enums;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для выполнения специализированных запросов изменения данных с
    /// специализированным запросом изменения данных, коллекцией представлений модели для
    /// параметров и командой выполнения.
    /// </summary>
    public partial class SpecialChangeDataRequestsVM : RequestsVM
    {
        /// <summary>
        /// Специализированный запрос изменения данных.
        /// </summary>
        [ObservableProperty]
        private SpecialChangeDataRequest request =
            SpecialChangeDataRequest.SetNullStudentDeductings;

        /// <summary>
        /// Коллекция представлений модели для параметров.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<ParametersVM> parametersVMs = new()
        {
            new ParametersVM(new object[] { DateTime.Now.Year, DateTime.Now.Year, "%%" }),
            new ParametersVM(new object[] { DateTime.Now.Year, DateTime.Now.Year, "%%" }),
            new ParametersVM(new object[] { 1, 3, 5 }),
            new ParametersVM(new object[] { 0 }),
            new ParametersVM(new object[] { 0, 1, 1 }),
            new ParametersVM(new object[] { "Стандартная стипендия", 2, 3, "%%", "%Томск%" }),
            new ParametersVM(new object[] { "Без стипендии", 1, 1, "%%" })
        };

        /// <summary>
        /// Возвращает и задаёт команду выполнения.
        /// </summary>
        public RelayCommand ExecuteCommand { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="SpecialChangeDataRequestsVM"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public SpecialChangeDataRequestsVM(IDbContextBuilder dbContextBuilder,
            IResourceService resourceService, IMessageService messageService) :
            base(dbContextBuilder, resourceService, messageService) =>
            ExecuteCommand = new RelayCommand(() => ExecuteCommand(CreateSpecialCommand()));

        /// <summary>
        /// Создаёт специализированную команду.
        /// </summary>
        /// <returns>Специализированная команда.</returns>
        private string CreateSpecialCommand()
        {
            var table = "";
            var command = "";
            /**var parametersIndex = (int)Request - 1;
            switch (Request)
            {
                case SpecialChangeDataRequest.SetNullStudentPassings:
                    table = "StudentDisciplineConnections";
                    command = CreateUpdateCommand(table, "IsPassed = NULL",
                        "StudentID IN (" +
                        CreateSelectCommand("ID",
                        "Students",
                        "(GroupFormationYear BETWEEN " +
                        $"{ParametersVMs[parametersIndex].Parameters[0]} " +
                        $"AND " +
                        $"{ParametersVMs[parametersIndex].Parameters[1]}) " +
                        "AND GroupNumber LIKE " +
                        $"'{ParametersVMs[parametersIndex].Parameters[2]}'") + ")"); break;
                case SpecialChangeDataRequest.SetNullStudentDeductings:
                    table = "Students";
                    command = CreateUpdateCommand(table, "IsDeductible = NULL",
                        "ID IN (" +
                        CreateSelectCommand("ID",
                        "Students",
                        "(GroupFormationYear BETWEEN " +
                        $"{ParametersVMs[parametersIndex].Parameters[0]} " +
                        $"AND " +
                        $"{ParametersVMs[parametersIndex].Parameters[1]}) " +
                        "AND GroupNumber LIKE " +
                        $"'{ParametersVMs[parametersIndex].Parameters[2]}'") + ")"); break;
                case SpecialChangeDataRequest.SetStudentPassingsByLastGrades:
                    table = "StudentDisciplineConnections";
                    command = CreateUpdateCommand(table, "IsPassed = " +
                        $"{ParametersVMs[parametersIndex].Parameters[0]}",
                        "(StudentID, DisciplineID) IN (" +
                        CreateSelectCommand("gs.StudentID, gs.DisciplineID",
                        "GradeStatements gs " +
                        "INNER JOIN(" +
                        CreateSelectCommand("gs.StudentID, gs.DisciplineID, MAX(gs.PassingDate), " +
                        "g.Coefficient",
                        "Grades g, GradeStatements gs",
                        "g.Name = gs.GradeName",
                        "gs.StudentID, gs.DisciplineID") +
                        ") AS lastGrades " +
                        "ON gs.StudentID = lastGrades.StudentID AND " +
                        "gs.DisciplineID = lastGrades.DisciplineID AND " +
                        "lastGrades.Coefficient BETWEEN " +
                        $"{ParametersVMs[parametersIndex].Parameters[1]} " +
                        "AND " +
                        $"{ParametersVMs[parametersIndex].Parameters[2]}") + ")"); break;
                case SpecialChangeDataRequest.SetStudentPassingsWithoutGrades:
                    table = "StudentDisciplineConnections";
                    command = CreateUpdateCommand(table, "IsPassed = " +
                        $"{ParametersVMs[parametersIndex].Parameters[0]}",
                        "(StudentID, DisciplineID) NOT IN (" +
                        CreateSelectCommand("DISTINCT gs.StudentID, gs.DisciplineID",
                        "GradeStatements gs") + ")"); break;
                case SpecialChangeDataRequest.SetStudentDeductings:
                    table = "Students";
                    command = CreateUpdateCommand(table, "IsDeductible = " +
                        $"{ParametersVMs[parametersIndex].Parameters[0]}",
                        "ID IN (" +
                        CreateSelectCommand("c.StudentID",
                        "StudentDisciplineConnections c " +
                        "INNER JOIN (" +
                        CreateSelectCommand("c.StudentID, c.DisciplineID, " +
                        "Count(c.IsPassed) AS Count",
                        "StudentDisciplineConnections c",
                        "c.IsPassed = " +
                        $"{ParametersVMs[parametersIndex].Parameters[1]}",
                        "c.StudentID") + ") AS passings",
                        "c.StudentID = passings.StudentID AND " +
                        "c.DisciplineID = passings.DisciplineID AND " +
                        "passings.Count >= " +
                        $"{ParametersVMs[parametersIndex].Parameters[2]}") + ")"); break;
                case SpecialChangeDataRequest.SetStudentScholarshipsByGrades:
                    table = "Students";
                    command = CreateUpdateCommand(table,
                        "ScholarshipName = " +
                        $"\"{ParametersVMs[parametersIndex].Parameters[0]}\"",
                        "ID IN (" +
                        CreateSelectCommand("s.ID",
                        "Students s " +
                        "JOIN (" +
                        CreateSelectCommand("s.ID, " +
                        "COUNT(MinLastGradeCoefficient) AS CountMinLastGrades, " +
                        "MIN(MinLastGradeCoefficient) AS MinLastGradeCoefficient, " +
                        "pa.PermanentResidenceAddress, " +
                        "s.ScholarshipName",
                        "Students s, Persons p, Passports pa " +
                        "LEFT JOIN (" +
                        CreateSelectCommand("gs.StudentID, " +
                        "MIN(g.Coefficient) AS MinLastGradeCoefficient",
                        "GradeStatements gs " +
                        "INNER JOIN Grades g ON gs.GradeName = g.Name",
                        "gs.PassingDate = (" +
                        CreateSelectCommand("MAX(PassingDate)",
                        "GradeStatements",
                        "StudentID = gs.StudentID") + ")",
                        "gs.StudentID") + ")" +
                        "t ON s.ID = t.StudentID",
                        "s.ID = p.ID AND p.PassportSerialNumber = pa.SerialNumber",
                        "S.ID") + ") AS minStatements",
                        "minStatements.ID = s.ID AND " +
                        "minStatements.CountMinLastGrades <= " +
                        $"{ParametersVMs[parametersIndex].Parameters[1]} " +
                        "AND minStatements.MinLastGradeCoefficient = " +
                        $"{ParametersVMs[parametersIndex].Parameters[2]} " +
                        "AND minStatements.ScholarshipName LIKE" +
                        $"'{ParametersVMs[parametersIndex].Parameters[3]}' " +
                        "AND minStatements.PermanentResidenceAddress NOT LIKE " +
                        $"'{ParametersVMs[parametersIndex].Parameters[4]}'") + ")"); break;
                case SpecialChangeDataRequest.SetStudentScholarshipsByPassings:
                    table = "Students";
                    command = CreateUpdateCommand(table,
                        "ScholarshipName = " +
                        $"\"{ParametersVMs[parametersIndex].Parameters[0]}\"",
                        "ID IN (" +
                        CreateSelectCommand("s.ID",
                        "Students s JOIN (" +
                        CreateSelectCommand("c.StudentID, COUNT(c.IsPassed) AS CountIsPassed, " +
                        "s.ScholarshipName",
                        "StudentDisciplineConnections c, Students s",
                        "s.ID = c.StudentID AND c.IsPassed = " +
                        $"{ParametersVMs[parametersIndex].Parameters[1]}",
                        "c.StudentID") + ") AS passings",
                        "passings.StudentID = s.ID AND passings.CountIsPassed >= " +
                        $"{ParametersVMs[parametersIndex].Parameters[2]} " +
                        "AND passings.ScholarshipName LIKE " +
                        $"'{ParametersVMs[parametersIndex].Parameters[3]}'") + ")"); break;
            }**/
            return $"{command}; SELECT * FROM {table}";
        }
    }
}