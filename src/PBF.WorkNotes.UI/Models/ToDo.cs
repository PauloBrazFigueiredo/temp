namespace PBF.WorkNotes.UI.Models;

public class ToDo : INotifyPropertyChanged
{
    private Guid _id;
    public Guid Id
    {
        get => _id;
        set         {
            _id = value;
            OnPropertyChanged();
        }
    }

    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    private Priority _Priority;
    public Priority Priority
    {
        get => _Priority;
        set
        {
            _Priority = value;
            OnPropertyChanged();
        }
    }

    private ToDoState _state;
    public ToDoState State
    {
        get => _state;
        set
        {
            _state = value;
            OnPropertyChanged();
        }
    }

    private string _notes;
    public string Notes
    {
        get => _notes;
        set
        {
            _notes = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
