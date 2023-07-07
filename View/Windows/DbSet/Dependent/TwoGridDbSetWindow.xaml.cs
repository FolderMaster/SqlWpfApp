using System.Windows;

using View.Implementations.ResourceService;

namespace View.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс окна для работы с представлением таблицы из базы данных, связанной с другой, через
    /// таблицу.
    /// </summary>
    public partial class TwoGridDbSetWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="TwoGridDbSetWindow"/>.
        /// </summary>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="headerKey">Ключ заголовка.</param>
        /// <param name="iconKey">Ключ иконки.</param>
        /// <param name="dataContext">Контекст данных.</param>
        public TwoGridDbSetWindow(IWindowResourceService windowResourceService, string headerKey,
            string iconKey, object dataContext)
        {
            InitializeComponent();

            Title = windowResourceService.GetHeader(headerKey);
            Icon = windowResourceService.GetIcon(iconKey);
            DataContext = dataContext;
        }
    }
}