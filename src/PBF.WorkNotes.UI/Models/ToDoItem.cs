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

    private Brush _priorityColor;
    public Brush PriorityColor
    {
        get => _priorityColor;
        set
        {
            _priorityColor = value;
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

    private string _stateName;
    public string StateName
    {
        get => _stateName;
        set
        {
            _stateName = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
