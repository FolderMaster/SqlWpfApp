using System;
using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Implementations.ResourceService;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services.Messages;

using Model.Dependent;
using Model.Independent;

namespace View.Implementations.Proces.DbSet.Dependent
{
    /// <summary>
    /// Класс оконной процедуры для работы с оценками с методами вызова и создания окна. Реализует
    /// <see cref="WindowProc"/>.
    /// </summary>
    public class GradesWindowProc : WindowProc
    {
        /// <summary>
        /// Ключ ресурсов.
        /// </summary>
        private static string _keyResource = nameof(Grade) + "s";

        /// <summary>
        /// Создаёт экземпляр класса <see cref="GradesWindowProc"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public GradesWindowProc(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextBuilder, windowResourceService, messageService) { }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService)
        {
            var mainVM = new DbSetVM<Grade>(dbContextBuilder, windowResourceService,
                messageService);
            var dependentVM = new DbSetVM<GradeMode>(dbContextBuilder, windowResourceService,
                messageService);

            mainVM.SelectedItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.GradeMode;
            };

            return new TwoGridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    mainVM, dependentVM,
                    (string nameProperty) => nameProperty != nameof(Grade.GradeStatements) &&
                    nameProperty != nameof(Grade.GradeMode), (string nameProperty) =>
                    nameProperty != nameof(GradeMode.Grades) &&
                    nameProperty != nameof(GradeMode.StudyForms)
                });
        }
    }
}