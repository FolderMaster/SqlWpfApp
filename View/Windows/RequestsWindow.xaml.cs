using System;
using System.Windows;

namespace View.Windows
{
    public partial class RequestsWindow : Window
    {
        private static RequestsWindow? _instance = null;

        private static Action _call = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static RequestsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RequestsWindow();
                }
                return _instance;
            }
        }

        public static Action Call => _call;

        public RequestsWindow()
        {
            InitializeComponent();

            DataContext = (Action)(() => _instance = null);
        }
    }
}