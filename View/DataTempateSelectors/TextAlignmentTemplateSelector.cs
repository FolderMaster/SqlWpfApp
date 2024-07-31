using System.Windows;
using System.Windows.Controls;

namespace View.DataTempateSelectors
{
    public class TextAlignmentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LeftTemplate { get; set; }

        public DataTemplate JustifyTemplate { get; set; }

        public DataTemplate CenterTemplate { get; set; }

        public DataTemplate RightTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case TextAlignment.Left:
                    return LeftTemplate;
                case TextAlignment.Justify:
                    return JustifyTemplate;
                case TextAlignment.Center:
                    return CenterTemplate;
                case TextAlignment.Right:
                    return RightTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
