namespace PBF.WorkNotes.UI;

public partial class MainWindow : Window
{
    private readonly IToDoStatesRepository _toDoStateRepository;

    public MainWindow()
    {
        InitializeComponent();

        //DataContext = new MainWindowViewModel();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var t = _toDoStateRepository.GetAll();
    }
}