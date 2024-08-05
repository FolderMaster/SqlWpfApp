using System.Windows;

namespace View.Implementations.Configuration
{
    public class WindowData
    {
        /// <summary>
        /// Возвращает и задаёт положение окна слева.
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Возвращает и задаёт положение окна сверху.
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Возвращает и задаёт ширину окна.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Возвращает и задаёт высоту окна.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Возвращает и задаёт состояние окна.
        /// </summary>
        public WindowState WindowState { get; set; }

        public WindowData() { }

        public WindowData(Window window)
        {
            Left = window.Left;
            Top = window.Top;
            Width = window.Width;
            Height = window.Height;
            WindowState = window.WindowState;
        }

        public void SetData(Window window)
        {
            window.Left = Left;
            window.Top = Top;
            window.Width = Width;
            window.Height = Height;
            window.WindowState = WindowState;
        }
    }
}
