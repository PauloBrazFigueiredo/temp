namespace PBF.WorkNotes.UI.Models;

public class Priority : BaseNotifyPropertyChanged, INotifyPropertyChanged
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

    private string _level;
    public string Level
    {
        get => _level;
        set
        {
            _level = value;
            OnPropertyChanged();
        }
    }

    private Brush _color;
    public Brush Color
    {
        get => _color;
        set
        {
            _color = value;
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
