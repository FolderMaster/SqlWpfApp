using System;
using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Services;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class StudentDisciplineConnectionsWindowProc : WindowProc
    {
        public StudentDisciplineConnectionsWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService)
        {
            var mainVM = new DbSetVM<StudentDisciplineConnection>(dbContextCreator,
                messageService);
            var dependentVM = new DbSetVM<Student>(dbContextCreator, messageService);
            var dependent2VM = new DbSetVM<Discipline>(dbContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Student;
                dependent2VM.SelectedItem = mainVM.SelectedItem?.Discipline;
            };

            return new ThreeGridDbSetWindow()
            {
                Title = AppResourceService.GetHeader(nameof(StudentDisciplineConnection) + "s"),
                Icon = AppResourceService.GetIcon(nameof(StudentDisciplineConnection) + "s"),
                DataContext = new List<object>()
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
                }
            };
        }
    }
}