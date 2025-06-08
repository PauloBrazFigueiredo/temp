namespace PBF.WorkNotes.UI.Models;

public class ToDoState : BaseNotifyPropertyChanged, INotifyPropertyChanged
{
    private Guid _id;
    public Guid Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged();
        }
    }

    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    private bool _isDefault;
    public bool IsDefault
    {
        get => _isDefault;
        set
        {
            _isDefault = value;
            OnPropertyChanged();
        }
    }
}
