namespace PBF.WorkNotes.UI.Models;

public class ToDoItem : INotifyPropertyChanged
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
    /*
    private Priority _priority;
    public Priority Priority
    {
        get => _priority;
        set
        {
            _priority = value;
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

    private DateTime? _workDate;
    public DateTime? WorkDate
    {
        get => _workDate;
        set
        {
            _workDate = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _dueDate;
    public DateTime? DueDate
    {
        get => _dueDate;
        set
        {
            _dueDate = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _createdDate;
    public DateTime? CreatedDate
    {
        get => _createdDate;
        set
        {
            _createdDate = value;
            OnPropertyChanged();
        }
    }
    */
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
