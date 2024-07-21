using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using View.Implementations.Proces;
using View.Implementations.ResourceService;

using ViewModel.Interfaces.Proces;

namespace View.Windows
{
    /// <summary>
    /// Класс основного окна.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="MainWindow"/> по умолчанию.
        /// <paramref name="proces"/>
        /// <paramref name="windowResourceService"/>
        /// </summary>
        public MainWindow(IEnumerable<IProc> proces, IWindowResourceService windowResourceService)
        {
            InitializeComponent();

            CreateUi(proces, windowResourceService);
        }

        private void CreateUi(IEnumerable<IProc> proces,
            IWindowResourceService windowResourceService)
        {
            if (proces != null)
            {
                var groupedMenu = new Dictionary<string, (MenuItem, MenuItem)>();
                var groupedData = proces.Select((p) => p.Data).
                    GroupBy((d) => ((ProcMetadata)d.Metadata).Path);
                var groupedDataCount = groupedData.Count();
                for (var i = 0; i < groupedDataCount; ++i)
                {
                    var group = groupedData.ElementAt(i);
                    var groupedMenuItems = GetGroupedMenuItems(group.Key,
                        groupedMenu, windowResourceService);
                    foreach (var data in group)
                    {
                        CreateMenuChild(data, groupedMenuItems);
                        CreateInputBinding(data);
                        CreateToolBarChild(data, windowResourceService);
                    }
                    if (i != groupedDataCount - 1)
                    {
                        CreateToolBarSeparator();
                    }
                }
            }
        }

        private (MenuItem?, MenuItem?) GetGroupedMenuItems(string path,
            Dictionary<string, (MenuItem, MenuItem)> groupedMenu,
            IWindowResourceService windowResourceService)
        {
            (MenuItem, MenuItem) result;
            if (groupedMenu.TryGetValue(path, out result))
            {
                return result;
            }
            else
            {
                var pathParts = path.Split('/');
                if (!pathParts.Any())
                {
                    return (null, null);
                }
                var partPath = "";
                var lastPartPath = "";
                for (var i = 0; i < pathParts.Length; i++)
                {
                    var currentPathPath = pathParts[i];
                    partPath += (i != 0 ? "/" : "") + currentPathPath;
                    if (!groupedMenu.ContainsKey(partPath))
                    {
                        result = (CreateGroupedMenuItem(currentPathPath, windowResourceService),
                            CreateGroupedMenuItem(currentPathPath, windowResourceService));
                        if (i == 0)
                        {
                            menu.Items.Add(result.Item1);
                            contextMenu.Items.Add(result.Item2);
                        }
                        else
                        {
                            var groupedMenuItems = groupedMenu[lastPartPath];
                            groupedMenuItems.Item1.Items.Add(result.Item1);
                            groupedMenuItems.Item2.Items.Add(result.Item2);
                        }
                        groupedMenu.Add(partPath, result);
                    }
                    lastPartPath = partPath;
                }
                return result;
            }
        }

        private MenuItem CreateGroupedMenuItem(string name,
            IWindowResourceService windowResourceService) => new MenuItem()
            {
                Header = windowResourceService.GetHeader(name)
            };

        private void CreateToolBarChild(IProcData data,
            IWindowResourceService windowResourceService)
        {
            var metadata = (ProcMetadata)data.Metadata;
            var image = new Image()
            {
                Source = (ImageSource)metadata.Icon
            };
            var button = new Button()
            {
                Command = data.Command,
                ToolTip = metadata.Title,
                Content = image,
                Style = windowResourceService.GetStyle("ToolbarButton")
            };
            toolBar.Items.Add(button);
        }

        private void CreateToolBarSeparator() => toolBar.Items.Add(new Separator());

        private void CreateMenuChild(IProcData data, (MenuItem?, MenuItem?) groupedMenuItems)
        {
            var newMenuItem = CreateMenuItem(data);
            if (groupedMenuItems.Item1 == null)
            {
                menu.Items.Add(newMenuItem);
            }
            else
            {
                groupedMenuItems.Item1.Items.Add(newMenuItem);
            }
            newMenuItem = CreateMenuItem(data);
            if (groupedMenuItems.Item2 == null)
            {
                contextMenu.Items.Add(newMenuItem);
            }
            else
            {
                groupedMenuItems.Item2.Items.Add(newMenuItem);
            }
        }

        private MenuItem CreateMenuItem(IProcData data)
        {
            var metadata = (ProcMetadata)data.Metadata;
            var image = new Image()
            {
                Source = (ImageSource)metadata.Icon
            };
            var menuItem = new MenuItem()
            {
                Command = data.Command,
                Header = metadata.Title,
                InputGestureText = metadata.InputText,
                Icon = image
            };
            return menuItem;
        }

        private void CreateInputBinding(IProcData data)
        {
            var metadata = (ProcMetadata)data.Metadata;
            InputBindings.Add(new InputBinding(data.Command, (KeyGesture)metadata.Input));
        }
    }
}