using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Model.Independent;

using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Data;

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
        /// Свойство зависимости <see cref="Passport"/>.
        /// </summary>
        public static DependencyProperty PassportProperty =
            DependencyProperty.Register(nameof(Passport), typeof(Passport),
                typeof(PassportControl));

        /// <summary>
        /// Свойство зависимости <see cref="IsReadOnly"/>.
        /// </summary>
        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(PassportControl));

        /// <summary>
        /// Свойство зависимости <see cref="LoadImageCommand"/>.
        /// </summary>
        public static DependencyProperty LoadImageCommandProperty =
            DependencyProperty.Register(nameof(LoadImageCommand), typeof(ICommand),
                typeof(PassportControl));

        /// <summary>
        /// Свойство зависимости <see cref="SaveImageCommand"/>.
        /// </summary>
        public static DependencyProperty SaveImageCommandProperty =
            DependencyProperty.Register(nameof(SaveImageCommand), typeof(ICommand),
                typeof(PassportControl));

        /// <summary>
        /// Свойство зависимости <see cref="ResourceService"/>.
        /// </summary>
        public static DependencyProperty ResourceServiceProperty = DependencyProperty.Register
            (nameof(ResourceService), typeof(IResourceService), typeof(PassportControl));

        /// <summary>
        /// Свойство зависимости <see cref="MessageService"/>.
        /// </summary>
        public static DependencyProperty MessageServiceProperty = DependencyProperty.Register
            (nameof(MessageService), typeof(IMessageService), typeof(PassportControl));

        /// <summary>
        /// Свойство зависимости <see cref="FileService"/>.
        /// </summary>
        public static DependencyProperty FileServiceProperty = DependencyProperty.Register
            (nameof(FileService), typeof(IFileService), typeof(PassportControl));

        /// <summary>
        /// Свойство зависимости <see cref="ImageService"/>.
        /// </summary>
        public static DependencyProperty ImageServiceProperty = DependencyProperty.Register
            (nameof(ImageService), typeof(IImageService), typeof(PassportControl));

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
        /// Возвращает и задаёт сервис ресурсов.
        /// </summary>
        public IResourceService ResourceService
        {
            get => (IResourceService)GetValue(ResourceServiceProperty);
            set => SetValue(ResourceServiceProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт сервис сообщений.
        /// </summary>
        public IMessageService MessageService
        {
            get => (IMessageService)GetValue(MessageServiceProperty);
            set => SetValue(MessageServiceProperty, value);
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
        /// Возвращает и задаёт сервис изображений.
        /// </summary>
        public IImageService ImageService
        {
            get => (IImageService)GetValue(ImageServiceProperty);
            set => SetValue(ImageServiceProperty, value);
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="PassportControl"/> по умолчанию.
        /// </summary>
        public PassportControl()
        {
            InitializeComponent();
        }
    }
}