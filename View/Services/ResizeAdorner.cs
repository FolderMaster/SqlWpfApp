using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace View.Services
{
    public class ResizeAdorner : Adorner
    {
        private VisualCollection _visualChildren;

        protected override int VisualChildrenCount => _visualChildren.Count;

        public ResizeAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _visualChildren = new VisualCollection(this);

            BuildAdornerCorner(1, 1);
            BuildAdornerCorner(-1, 1);
            BuildAdornerCorner(1, -1);
            BuildAdornerCorner(-1, -1);
            BuildAdornerCorner(0, -1);
            BuildAdornerCorner(0, 1);
            BuildAdornerCorner(-1, 0);
            BuildAdornerCorner(1, 0);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var adornerWidth = AdornedElement.DesiredSize.Width;
            var adornerHeight = AdornedElement.DesiredSize.Height;
            foreach (var children in _visualChildren)
            {
                var thumb = (Thumb)children;
                var thumbWidth = thumb.DesiredSize.Width;
                var thumbHeight = thumb.DesiredSize.Height;
                var tag = ((int, int))thumb.Tag;
                thumb.Arrange(new Rect(GetPosition(adornerWidth, tag.Item1) - thumbWidth / 2,
                     GetPosition(adornerHeight, tag.Item2) - thumbHeight / 2,
                     thumbWidth, thumbHeight));
            }
            return finalSize;
        }

        protected override Visual GetVisualChild(int index) => _visualChildren[index];

        private void BuildAdornerCorner(int widthCoefficient, int heightCoefficient)
        {
            var thumb = new Thumb
            {
                Style = (Style)Application.Current.Resources["ResizeThumbStyle"]
            };
            thumb.Tag = (widthCoefficient, heightCoefficient);
            thumb.DragDelta += (sender, args) =>
                DragDelta(args, widthCoefficient, heightCoefficient);
            _visualChildren.Add(thumb);
        }

        private void DragDelta(DragDeltaEventArgs args,
            int widthCoefficient, int heightCoefficient)
        {
            var element = (FrameworkElement)AdornedElement;
            element.Width = Math.Max(element.Width +
                widthCoefficient * args.HorizontalChange, 0);
            element.Height = Math.Max(element.Height +
                heightCoefficient * args.VerticalChange, 0);
        }

        private double GetPosition(double dimension, int coefficient)
        {
            switch (coefficient)
            {
                case -1:
                    return 0;
                case 0:
                    return dimension / 2;
                case 1:
                    return dimension;
                default:
                    throw new ArgumentOutOfRangeException(nameof(coefficient));
            }
        }
    }
}
