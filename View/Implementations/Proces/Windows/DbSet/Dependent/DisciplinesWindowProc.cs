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
    /// Класс оконной процедуры для работы с дисциплинами с методами вызова и создания окна.
    /// Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class DisciplinesWindowProc : DbWindowProc
    {
        /// <summary>
        /// Ключ ресурсов.
        /// </summary>
        private static string _keyResource = nameof(Discipline) + "s";

        /// <summary>
        /// Создаёт экземпляр класса <see cref="DisciplinesWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public DisciplinesWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("Disciplines", session, windowResourceService, messageService)
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
            var mainVM = new DbSetVM<Discipline>(session, windowResourceService,
                messageService);
            var dependentVM = new DbSetVM<StudyForm>(session, windowResourceService,
                messageService);
            var dependent2VM = new DbSetVM<Specialty>(session, windowResourceService,
                messageService);

            mainVM.SelectedItemChanged += (sender, e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.StudyForm;
                dependent2VM.SelectedItem = mainVM.SelectedItem?.Specialty;
            };

            return new ThreeGridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    mainVM, dependentVM, dependent2VM,
                    (string nameProperty) => nameProperty != nameof(Discipline.Specialty) &&
                    nameProperty != nameof(Discipline.StudentConnections) &&
                    nameProperty != nameof(Discipline.TeacherConnections) &&
                    nameProperty != nameof(Discipline.StudyForm) &&
                    nameProperty != nameof(Discipline.GradeStatements),
                    (string nameProperty) => nameProperty != nameof(StudyForm.Disciplines) &&
                    nameProperty != nameof(StudyForm.GradeMode),
                    (string nameProperty) => nameProperty != nameof(Specialty.Groups) &&
                    nameProperty != nameof(Specialty.Disciplines) &&
                    nameProperty != nameof(Specialty.Department)
                });
        }
    }
}