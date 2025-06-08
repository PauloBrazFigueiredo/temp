namespace PBF.WorkNotes.UI.Views;

public partial class ToDoView : UserControl
{
    public ToDoView()
    {
        Loaded += MainWindow_Loaded;
        InitializeComponent();
    }

    public ToDoView(ToDoViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is ToDoViewModel vm)
        {
            (DataContext as ToDoViewModel).SetRichTextBox(richTextBox);
        }
    }

    private void RichTextBoxSelectionChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            (DataContext as ToDoViewModel).SelectionChangedCommand.Execute(null);
        }
    }
}
