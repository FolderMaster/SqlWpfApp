using System.Windows;

using View.Windows.DbSet.Dependent;

using ViewModel.Interfaces;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class TeacherDisciplineConnectionsWindowProc : WindowProc
    {
        public TeacherDisciplineConnectionsWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService) =>
            new TeacherDisciplineConnectionsWindow(dbContextCreator, messageService);
    }
}