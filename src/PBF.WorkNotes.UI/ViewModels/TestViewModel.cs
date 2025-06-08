namespace PBF.WorkNotes.UI.ViewModels;

public class TestViewModel : INotifyPropertyChanged
{
    private readonly IToDoStatesService _toDoStateService;

    public TestViewModel(IToDoStatesService toDoStateService)
    {
        _toDoStateService = toDoStateService;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
