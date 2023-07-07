using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Services.Messages;

using Model.Independent;

namespace View.Controls.DbSet
{
    /// <summary>
    /// Класс элемента управления для работы с паспортами с паспортом, логическим значением,
    /// указывающее только ли для чтения элемент управления, сервисами послания сообщений,
    /// файловым, путей и командами сохранения и загрузки изображения.
    /// </summary>
    public partial class PassportControl : UserControl
    {
        /// <summary>
        /// Возвращает и задаёт паспорт.
        /// </summary>
        public Passport Passport
        {
            get => (Passport)GetValue(PassportProperty);
            set => SetValue(PassportProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт логическое значение, указывающее только ли для чтения элемент
        /// управления.
        /// </summary>
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт команду загрузки изображения.
        /// </summary>
        public ICommand LoadImageCommand
        {
            get => (ICommand)GetValue(LoadImageCommandProperty);
            set => SetValue(LoadImageCommandProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт команду сохранения изображения.
        /// </summary>
        public ICommand SaveImageCommand
        {
            get => (ICommand)GetValue(SaveImageCommandProperty);
            set => SetValue(SaveImageCommandProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт сервис послания сообщений.
        /// </summary>
        public IMessengerService MessengerService
        {
            get => (IMessengerService)GetValue(MessengerServiceProperty);
            set => SetValue(MessengerServiceProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт файловый сервис.
        /// </summary>
        public IFileService FileService
        {
            get => (IFileService)GetValue(FileServiceProperty);
            set => SetValue(FileServiceProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт сервис путей.
        /// </summary>
        public IPathService PathService
        {
            get => (IPathService)GetValue(PathServiceProperty);
            set => SetValue(PathServiceProperty, value);
        }

        /// <summary>
        /// Свойство зависимости <see cref="Passport"/>.
        /// </summary>
        public static DependencyProperty PassportProperty =
            DependencyProperty.Register(nameof(Passport), typeof(Passport),
                typeof(PassportControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="IsReadOnly"/>.
        /// </summary>
        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(PassportControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="LoadImageCommand"/>.
        /// </summary>
        public static DependencyProperty LoadImageCommandProperty =
            DependencyProperty.Register(nameof(LoadImageCommand), typeof(ICommand),
                typeof(PassportControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="SaveImageCommand"/>.
        /// </summary>
        public static DependencyProperty SaveImageCommandProperty =
            DependencyProperty.Register(nameof(SaveImageCommand), typeof(ICommand),
                typeof(PassportControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="MessengerService"/>.
        /// </summary>
        public static DependencyProperty MessengerServiceProperty = DependencyProperty.Register
            (nameof(MessengerService), typeof(IMessengerService), typeof(PassportControl),
            new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="FileService"/>.
        /// </summary>
        public static DependencyProperty FileServiceProperty = DependencyProperty.Register
            (nameof(FileService), typeof(IFileService), typeof(PassportControl),
            new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="PathService"/>.
        /// </summary>
        public static DependencyProperty PathServiceProperty = DependencyProperty.Register
            (nameof(PathService), typeof(IPathService), typeof(PassportControl),
            new FrameworkPropertyMetadata());

        /// <summary>
        /// Создаёт экземпляр класса <see cref="PassportControl"/> по умолчанию.
        /// </summary>
        public PassportControl()
        {
            InitializeComponent();
        }
    }
}