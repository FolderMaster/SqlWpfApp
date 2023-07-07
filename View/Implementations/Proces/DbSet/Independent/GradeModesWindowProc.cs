using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Independent;
using View.Implementations.ResourceService;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services.Messages;

using Model.Independent;

namespace View.Implementations.Proces.DbSet.Independent
{
    /// <summary>
    /// Класс оконной процедуры для работы с режимами оценивания с методами вызова и создания окна.
    /// Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class GradeModesWindowProc : WindowProc
    {
        /// <summary>
        /// Ключ ресурсов.
        /// </summary>
        private static string _keyResource = nameof(GradeMode) + "s";

        /// <summary>
        /// Создаёт экземпляр класса <see cref="GradeModesWindowProc"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public GradeModesWindowProc(IDbContextBuilder dbContextBuilder,
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
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new GridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    new DbSetVM<GradeMode>(dbContextBuilder, windowResourceService,
                        messageService),
                    (string nameProperty) => nameProperty != nameof(GradeMode.Grades) &&
                    nameProperty != nameof(GradeMode.StudyForms)
                });
    }
}
