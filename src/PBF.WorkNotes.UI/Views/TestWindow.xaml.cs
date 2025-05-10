using System.Collections.ObjectModel;

namespace PBF.WorkNotes.UI;

public partial class TestWindow : Window
{
    //private ObservableCollection<Product> _products;
    private readonly IToDoStatesRepository _toDoStateRepository;

    public TestWindow(TestViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
    }
}