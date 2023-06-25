using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;

namespace View.Behaviors
{
    public class CloseWindowBehavior : Behavior<Window>
    {
        public Action CloseAction
        {
            get => (Action)GetValue(CloseActionProperty);
            set => SetValue(CloseActionProperty, value);
        }

        public static DependencyProperty CloseActionProperty =
            DependencyProperty.Register(nameof(CloseAction), typeof(Action),
                typeof(CloseWindowBehavior), new FrameworkPropertyMetadata());

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Closed += Closed;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Closed -= Closed;
        }

        private void Closed(object? sender, EventArgs e) => CloseAction?.Invoke();
    }
}