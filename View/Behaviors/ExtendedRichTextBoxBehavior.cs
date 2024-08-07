using Microsoft.Xaml.Behaviors;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

using View.Implementations.Document;

namespace View.Behaviors
{
    public class ExtendedRichTextBoxBehavior : Behavior<RichTextBox>
    {
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register(nameof(Document), typeof(Document),
                typeof(ExtendedRichTextBoxBehavior),new FrameworkPropertyMetadata
                (null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, DocumentChanged));

        private static readonly DependencyProperty SelectionProperty =
            DependencyProperty.Register(nameof(Selection), typeof(Selection),
                typeof(ExtendedRichTextBoxBehavior));

        public Document Document
        {
            get => (Document)GetValue(DocumentProperty);
            set => SetValue(DocumentProperty, value);
        }

        public Selection Selection
        {
            get => (Selection)GetValue(SelectionProperty);
            set => SetValue(SelectionProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            Document = new Document(AssociatedObject.Document);
            Selection = new Selection(AssociatedObject.Selection);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }

        private void AssociatedObject_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var richTextBox = (RichTextBox)sender;
            if (!richTextBox.IsReadOnly)
            {
                Selection = new Selection(richTextBox.Selection);
            }
            else
            {
                Selection = null;
            }
        }

        private static void DocumentChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            var behavior = (ExtendedRichTextBoxBehavior)sender;
            var document = (Document?)e.NewValue;
            if (document != null)
            {
                behavior.AssociatedObject.Document = document.FlowDocument;
                behavior.AssociatedObject.IsReadOnly = false;
            }
            else
            {
                behavior.AssociatedObject.Document = new FlowDocument();
                behavior.AssociatedObject.IsReadOnly = true;
            }
        }
    }
}
