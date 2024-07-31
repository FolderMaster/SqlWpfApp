using Microsoft.Xaml.Behaviors;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace View.Behaviors
{
    public class ExtendedRichTextBoxBehavior : Behavior<RichTextBox>
    {
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register(nameof(Document), typeof(FlowDocument),
                typeof(ExtendedRichTextBoxBehavior), new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, DocumentChanged));

        private static readonly DependencyProperty SelectionProperty =
            DependencyProperty.Register(nameof(Selection), typeof(TextSelection),
                typeof(ExtendedRichTextBoxBehavior));

        public FlowDocument Document
        {
            get => (FlowDocument)GetValue(DocumentProperty);
            set => SetValue(DocumentProperty, value);
        }

        public TextSelection Selection
        {
            get => (TextSelection)GetValue(SelectionProperty);
            set => SetValue(SelectionProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            Document = AssociatedObject.Document;
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }

        private void AssociatedObject_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var richTextBox = (RichTextBox)sender;
            Selection = richTextBox.Selection;
        }

        private static void DocumentChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            var behavior = (ExtendedRichTextBoxBehavior)sender;
            behavior.AssociatedObject.Document = (FlowDocument)e.NewValue;
        }
    }
}
