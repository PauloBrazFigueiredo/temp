/*namespace PBF.WorkNotes.UI.ViewModels;

public class MainViewModel : INotifyPropertyChanged, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    //protected MainViewModel _mainViewModel;
    public event PropertyChangedEventHandler PropertyChanged;
    //private ViewModelBase _currentViewModel = null!;
    //private Dictionary<string, ViewModelBase> _viewModels;
    private readonly DispatcherTimer _messageTimer;

    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    private string _statusMessage = "Application ready";
    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    private PackIconKind _statusIcon = PackIconKind.CheckCircle;
    public PackIconKind StatusIcon
    {
        get => _statusIcon;
        set => SetProperty(ref _statusIcon, value);
    }

    private Brush _statusColor = Brushes.Green;
    public Brush StatusColor
    {
        get => _statusColor;
        set => SetProperty(ref _statusColor, value);
    }

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        // Setup clock timer
        _messageTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(30)  // Hide message after 30 seconds
        };
        _messageTimer.Tick += (s, e) => ClearStatusMessage();
        //var a = App.Current.ServiceProvider.GetRequiredService<ToDoViewModel>();
        // Initialize all view models
        //_viewModels = new Dictionary<string, ViewModelBase>
        //{
        //    ["Home"] = new HomeViewModel(null),
        //    ["ToDos"] = new ToDosViewModel(this),
        //    ["ToDo"] = _serviceProvider.GetRequiredService<ToDoViewModel>(),
        //    //["ToDo"] = new ToDoViewModel(this, App.Current.)
        //};

        // Set default view
        //CurrentViewModel = _viewModels["Home"];

        // Initialize commands
        //ChangeViewCommand = new RelayCommand<string>(ExecuteChangeView);
    }
    
    private ICommand _switchViewCommand;
    public ICommand SwitchViewCommand => _switchViewCommand ??= new RelayCommand<string>(SwitchView);
    private void SwitchView(string viewName)
    {
        //if (CurrentDetailViewModel is DetailViewModelA)
        //{
        //    CurrentDetailViewModel = new DetailViewModelB { CurrentItem = SelectedItem };
        //}
        //else
        //{
        //    CurrentDetailViewModel = new DetailViewModelA { CurrentItem = SelectedItem };
        //}
    }

    //public ICommand ChangeViewCommand { get; }
    //public void ExecuteChangeView(string viewName)
    //{
    //    try
    //    {
    //        _viewModels = new Dictionary<string, ViewModelBase>
    //        {
    //            ["Home"] = new HomeViewModel(null),
    //            ["ToDos"] = new ToDosViewModel(this),
    //            ["ToDo"] = _serviceProvider.GetRequiredService<ToDoViewModel>()
    //        };
    //        if (_viewModels.TryGetValue(viewName, out var viewModel))
    //        {
    //            CurrentViewModel = viewModel;
    //        }
    //    }
    //    catch (Exception ex)
    //    { 
    //    }
    //}

    private void ClearStatusMessage()
    {
        StatusIcon = PackIconKind.None;
        StatusMessage = string.Empty;
        _messageTimer.Stop();
    }

    public void SetStatus(PackIconKind icon, Brush color, string message, int timeInterval)
    {
        StatusIcon = icon;
        StatusColor = color;
        StatusMessage = message;

        _messageTimer.Stop();
        if (timeInterval != 0)
        {
            _messageTimer.Interval = TimeSpan.FromSeconds(timeInterval);
            _messageTimer.Start();
        }
    }

    public void ShowSuccess(string message)
    {
        SetStatus(PackIconKind.CheckCircle, Brushes.Green, message, 2);
    }

    public void ShowWarning(string message)
    {
        SetStatus(PackIconKind.Alert, Brushes.Orange, message, 3);
    }

    public void ShowError(string message)
    {
        SetStatus(PackIconKind.Information, Brushes.Blue, message, 3);
        //SetStatus(PackIconKind.CloseCircle, Brushes.Red, message, 5);
    }

    public void ShowInfo(string message)
    {
        SetStatus(PackIconKind.Information, Brushes.Blue, message, 3);
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

    ~MainViewModel()
    {
        Dispose(false);
    }
}
*/