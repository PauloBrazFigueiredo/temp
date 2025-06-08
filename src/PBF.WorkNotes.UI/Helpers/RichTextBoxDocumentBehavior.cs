namespace PBF.WorkNotes.UI.Helpers;

public class RichTextBoxDocumentBehavior : Behavior<RichTextBox>
{
    private bool _isUpdating;

    public static readonly DependencyProperty DocumentProperty =
        DependencyProperty.Register(
            "Document",
            typeof(FlowDocument),
            typeof(RichTextBoxDocumentBehavior),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnDocumentChanged));

    public FlowDocument Document
    {
        get => (FlowDocument)GetValue(DocumentProperty);
        set => SetValue(DocumentProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.TextChanged += OnTextChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.TextChanged -= OnTextChanged;
    }

    private static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var behavior = (RichTextBoxDocumentBehavior)d;
        if (behavior._isUpdating) return;

        var richTextBox = behavior.AssociatedObject;
        if (richTextBox != null && richTextBox.Document != e.NewValue)
        {
            try
            {
                behavior._isUpdating = true;
                richTextBox.Document = e.NewValue as FlowDocument ?? new FlowDocument();
            }
            finally
            {
                behavior._isUpdating = false;
            }
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isUpdating) return;

        try
        {
            _isUpdating = true;
            Document = AssociatedObject.Document;
        }
        finally
        {
            _isUpdating = false;
        }
    }
}
