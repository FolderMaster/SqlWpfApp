using Microsoft.Xaml.Behaviors;

using System.Windows;
using System.Windows.Controls;

using View.Implementations.Document;

using ViewModel.Interfaces.Services.Document;

namespace View.Behaviors
{
    public class ExtendedRichTextBoxBehavior : Behavior<RichTextBox>
    {
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register(nameof(Document), typeof(Document),
                typeof(ExtendedRichTextBoxBehavior),
                new FrameworkPropertyMetadata(DocumentChanged));

        private static readonly DependencyProperty SelectionProperty =
            DependencyProperty.Register(nameof(Selection), typeof(Selection),
                typeof(ExtendedRichTextBoxBehavior));

        private static readonly DependencyProperty DocumentServiceProperty =
            DependencyProperty.Register(nameof(DocumentService), typeof(IDocumentService),
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

        public IDocumentService DocumentService
        {
            get => (IDocumentService)GetValue(DocumentServiceProperty);
            set => SetValue(DocumentServiceProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            Document = new Document(AssociatedObject.Document);
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
            Selection = new Selection(richTextBox.Selection);
        }

        private static void DocumentChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            var behavior = (ExtendedRichTextBoxBehavior)sender;
            //behavior.AssociatedObject.Document = (FlowDocument)e.NewValue;
        }
    }
}
