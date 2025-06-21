namespace PBF.WorkNotes.UI.Views;

public partial class ToDosView : UserControl
{
    public ToDosView()
    {
        InitializeComponent();
    }

    public ToDosView(ToDosViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
