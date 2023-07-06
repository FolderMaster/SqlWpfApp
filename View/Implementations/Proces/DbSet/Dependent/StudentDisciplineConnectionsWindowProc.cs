using System;
using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class StudentDisciplineConnectionsWindowProc : WindowProc
    {
        private static string _keyResource = nameof(StudentDisciplineConnection) + "s";

        public StudentDisciplineConnectionsWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextCreator, windowResourceService, messageService) { }

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService)
        {
            var mainVM = new DbSetVM<StudentDisciplineConnection>(dbContextCreator,
                windowResourceService, messageService);
            var dependentVM = new DbSetVM<Student>(dbContextCreator, windowResourceService,
                messageService);
            var dependent2VM = new DbSetVM<Discipline>(dbContextCreator, windowResourceService,
                messageService);

            mainVM.SelectedItemChanged += (object? sender, EventArgs e) =>
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