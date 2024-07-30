using Microsoft.Xaml.Behaviors;

using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace View.Behaviors
{
    public class ExtendedListBoxBehavior : Behavior<ListBox>
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(nameof(SelectedItems), typeof(IList),
                typeof(ExtendedListBoxBehavior), new PropertyMetadata(null, SelectedItemsChanged));

        private bool _selectedItemsChanged = false;

        public IList? SelectedItems
        {
            get => (IList?)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionMode = SelectionMode.Multiple;
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItems != null && !_selectedItemsChanged)
            {
                foreach (var item in e.RemovedItems)
                {
                    SelectedItems.Remove(item);
                }
                foreach (var item in e.AddedItems)
                {
                    SelectedItems.Add(item);
                }
            }
        }

        private static void SelectedItemsChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            var behavior = (ExtendedListBoxBehavior)sender;
            var listBox = behavior.AssociatedObject;
            var list = (IList)e.NewValue;

            behavior._selectedItemsChanged = true;
            listBox.SelectedItems.Clear();
            foreach (var item in list)
            {
                listBox.SelectedItems.Add(item);
            }
            behavior._selectedItemsChanged = false;
        }
    }
}
