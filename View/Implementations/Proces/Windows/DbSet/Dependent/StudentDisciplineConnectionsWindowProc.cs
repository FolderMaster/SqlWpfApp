using System;
using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Implementations.ResourceService;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.Services.Messages;

using Model.Dependent;
using ViewModel.Interfaces;

namespace View.Implementations.Proces.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс оконной процедуры для работы со связями между дисциплинами и студентами с методами
    /// вызова и создания окна. Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class StudentDisciplineConnectionsWindowProc : DbWindowProc
    {
        /// <summary>
        /// Ключ ресурсов.
        /// </summary>
        private static string _keyResource = nameof(StudentDisciplineConnection) + "s";

        /// <summary>
        /// Создаёт экземпляр класса <see cref="StudentDisciplineConnectionsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public StudentDisciplineConnectionsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("StudentDisciplineConnections", session, windowResourceService, messageService)
        { }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService)
        {
            var mainVM = new DbSetVM<StudentDisciplineConnection>(session,
                windowResourceService, messageService);
            var dependentVM = new DbSetVM<Student>(session, windowResourceService,
                messageService);
            var dependent2VM = new DbSetVM<Discipline>(session, windowResourceService,
                messageService);

            mainVM.SelectedItemChanged += (sender, e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Student;
                dependent2VM.SelectedItem = mainVM.SelectedItem?.Discipline;
            };

            return new ThreeGridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    mainVM, dependentVM, dependent2VM, (string nameProperty) =>
                    nameProperty != nameof(StudentDisciplineConnection.Student) &&
                    nameProperty != nameof(StudentDisciplineConnection.Discipline),
                    (string nameProperty) => nameProperty != nameof(Student.Group) &&
                    nameProperty != nameof(Student.Person) &&
                    nameProperty != nameof(Student.Scholarship) &&
                    nameProperty != nameof(Student.Connections) &&
                    nameProperty != nameof(Student.GradeStatements), (string nameProperty) =>
                    nameProperty != nameof(Discipline.Specialty) &&
                    nameProperty != nameof(Discipline.StudentConnections) &&
                    nameProperty != nameof(Discipline.TeacherConnections) &&
                    nameProperty != nameof(Discipline.StudyForm) &&
                    nameProperty != nameof(Discipline.GradeStatements)
                });
        }
    }
}