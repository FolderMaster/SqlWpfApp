using System;
using System.Configuration;
using System.Windows;

using SQLiteWpfApp.ViewModels;

namespace SQLiteWpfApp.Views
{
    class WindowConfiguration : IConfigurational
    {
        private Configuration _configuration;

        private KeyValueConfigurationCollection _settings;

        private Window _window;

        public string DataBasePath { get; set; } =
            "C:\\Users\\darko\\Documents\\UniversityDataBase.db";

        public double Left
        {
            get => _window.Left;
            set => _window.Left = value;
        }

        public double Top
        {
            get => _window.Top;
            set => _window.Top = value;
        }

        public double Width
        {
            get => _window.Width;
            set => _window.Width = value;
        }

        public double Height
        {
            get => _window.Height;
            set => _window.Height = value;
        }

        public WindowState WindowState
        {
            get => _window.WindowState;
            set => _window.WindowState = value;
        }

        public WindowConfiguration(Window window)
        {
            _configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _settings = _configuration.AppSettings.Settings;
            _window = window;
        }

        private void SetConfigurationValue(string key, object value)
        {
            if (_settings[key] == null)
            {
                _settings.Add(key, value.ToString());
            }
            else
            {
                _settings[key].Value = value.ToString();
            }
        }

        private void AssignByConfigurationValue(string key, Action<string> assign)
        {
            string? value = _settings[key] != null ? _settings[key].Value : null;
            if(value != null)
            {
                assign(value);
            }
        } 

        public void Save()
        {
            SetConfigurationValue(nameof(DataBasePath), DataBasePath);
            SetConfigurationValue(nameof(Left), Left);
            SetConfigurationValue(nameof(Top), Top);
            SetConfigurationValue(nameof(Width), Width);
            SetConfigurationValue(nameof(Height), Height);
            SetConfigurationValue(nameof(WindowState), WindowState);
            _configuration.Save();
        }

        public void Load()
        {
            AssignByConfigurationValue(nameof(DataBasePath), (value) => DataBasePath = value);
            AssignByConfigurationValue(nameof(Left), (value) => Left = double.Parse(value));
            AssignByConfigurationValue(nameof(Top), (value) => Top = double.Parse(value));
            AssignByConfigurationValue(nameof(Width), (value) => Width = double.Parse(value));
            AssignByConfigurationValue(nameof(Height), (value) => Height = double.Parse(value));
            AssignByConfigurationValue(nameof(WindowState), (value) =>
                WindowState = (WindowState)Enum.Parse(typeof(WindowState), value));
        }
    }
}