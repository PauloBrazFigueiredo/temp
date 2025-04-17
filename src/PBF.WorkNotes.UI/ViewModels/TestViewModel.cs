namespace PBF.WorkNotes.UI.ViewModels;

public class TestViewModel : INotifyPropertyChanged
{
    private readonly IToDoStateService _toDoStateService;

    public TestViewModel(IToDoStateService toDoStateService)
    {
        _toDoStateService = toDoStateService;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
