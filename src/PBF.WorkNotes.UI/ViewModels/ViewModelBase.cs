namespace PBF.WorkNotes.UI.ViewModels;
/*
public class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    protected MainViewModel _mainViewModel;
    public event PropertyChangedEventHandler PropertyChanged;

    public ViewModelBase(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private bool _disposed;
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            // Dispose managed resources here
        }

        _disposed = true;
    }

    ~ViewModelBase()
    {
        Dispose(false);
    }
}
*/
public abstract class ViewModelBase : INotifyPropertyChanged
{
    protected MainViewModel _mainViewModel;

    protected ViewModelBase(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}