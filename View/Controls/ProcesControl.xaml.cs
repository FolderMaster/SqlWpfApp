using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using View.Implementations.Proces;

using ViewModel.Interfaces.Proces;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for ProcesControl.xaml
    /// </summary>
    public partial class ProcesControl : UserControl
    {
        private const string _headerKey = "Header";

        public static readonly DependencyProperty ProcesProperty =
            DependencyProperty.Register(nameof(Proces), typeof(IEnumerable<IProc>),
                typeof(ProcesControl), new PropertyMetadata(null, DataChanged));

        public IEnumerable<IProc> Proces
        {
            get => (IEnumerable<IProc>)GetValue(ProcesProperty);
            set => SetValue(ProcesProperty, value);
        }

        public ProcesControl()
        {
            InitializeComponent();

            Focusable = true;
            Loaded += (s, e) => Keyboard.Focus(this);
        }

        private static void DataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ProcesControl)d;
            var proces = (IEnumerable<IProc>)e.NewValue;
            if(proces != null)
            {
                var groupedMenu = new Dictionary<string, (MenuItem, MenuItem)>();
                var groupedData = proces.Select((p) => p.Data).
                    GroupBy((d) => ((ProcMetadata)d.Metadata).Path);
                var groupedDataCount = groupedData.Count();
                for (var i = 0; i < groupedDataCount; ++i)
                {
                    var group = groupedData.ElementAt(i);
                    var groupedMenuItems = GetGroupedMenuItems(group.Key, control, groupedMenu);
                    foreach (var data in group)
                    {
                        CreateMenuChild(data, control, groupedMenuItems);
                        CreateInputBinding(data, control);
                        CreateToolBarChild(data, control);
                    }
                    if(i != groupedDataCount - 1)
                    {
                        CreateToolBarSeparator(control);
                    }
                }
            }
        }

        private static (MenuItem?, MenuItem?) GetGroupedMenuItems(string path,
            ProcesControl control, Dictionary<string, (MenuItem, MenuItem)> groupedMenu)
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
                        result = (CreateGroupedMenuItem(currentPathPath),
                            CreateGroupedMenuItem(currentPathPath));
                        if (i == 0)
                        {
                            control.menu.Items.Add(result.Item1);
                            control.contextMenu.Items.Add(result.Item2);
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

        private static MenuItem CreateGroupedMenuItem(string name) => new MenuItem()
            { Header = (string)Application.Current.Resources[name + _headerKey] };

        private static void CreateToolBarChild(IProcData data, ProcesControl control)
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
                Style = (Style)Application.Current.Resources["ToolbarButtonStyle"]
            };
            control.toolBar.Items.Add(button);
        }

        private static void CreateToolBarSeparator(ProcesControl control) =>
            control.toolBar.Items.Add(new Separator());

        private static void CreateMenuChild(IProcData data, ProcesControl control,
            (MenuItem?, MenuItem?) groupedMenuItems)
        {
            var newMenuItem = CreateMenuItem(data);
            if (groupedMenuItems.Item1 == null)
            {
                control.menu.Items.Add(newMenuItem);
            }
            else
            {
                groupedMenuItems.Item1.Items.Add(newMenuItem);
            }
            newMenuItem = CreateMenuItem(data);
            if (groupedMenuItems.Item2 == null)
            {
                control.contextMenu.Items.Add(newMenuItem);
            }
            else
            {
                groupedMenuItems.Item2.Items.Add(newMenuItem);
            }
        }

        private static MenuItem CreateMenuItem(IProcData data)
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

        private static void CreateInputBinding(IProcData data, ProcesControl control)
        {
            var metadata = (ProcMetadata)data.Metadata;
            control.InputBindings.Add(new InputBinding(data.Command, (KeyGesture)metadata.Input));
        }
    }
}
