using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Independent;
using View.Implementations.ResourceService;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.Services.Messages;

using Model.Independent;
using ViewModel.Interfaces;

namespace View.Implementations.Proces.Windows.DbSet.Independent
{
    /// <summary>
    /// Класс оконной процедуры для работы с ролями с методами вызова и создания окна. Реализует
    /// <see cref="WindowProc"/>.
    /// </summary>
    public class RolesWindowProc : DbWindowProc
    {
        /// <summary>
        /// Ключ ресурсов.
        /// </summary>
        private static string _keyResource = nameof(Role) + "s";

        /// <summary>
        /// Создаёт экземпляр класса <see cref="RolesWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public RolesWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("Roles", session, windowResourceService, messageService)
        { }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new GridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    new DbSetVM<Role>(session, windowResourceService, messageService),
                    (string nameProperty) => nameProperty != nameof(Role.Connections)
                });
    }
}