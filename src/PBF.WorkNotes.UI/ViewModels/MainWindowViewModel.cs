namespace PBF.WorkNotes.UI.ViewModels;

public class MainWindowViewModel : BaseNotifyPropertyChanged, INotifyPropertyChanged, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private Dictionary<string, ViewModelBase> _viewModels = new Dictionary<string, ViewModelBase>();

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        SwitchView("Home");
    }

    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    public ICommand SwitchViewCommand => new RelayCommand<string>(SwitchView);
    private void SwitchView(string viewName)
    {
        ViewModelBase viewModel = null;
        if (_viewModels.ContainsKey(viewName))
        {
            viewModel = _viewModels[viewName];
        }
        else
        {
            viewModel = viewName switch
            {
                "Home" => _serviceProvider.GetRequiredService<HomeViewModel>(),
                "ToDo" => _serviceProvider.GetRequiredService<ToDoViewModel>()
            };
            _viewModels[viewName] = viewModel;
        }
        CurrentViewModel = _viewModels[viewName];
    }

    #region Message Timer

    private DispatcherTimer _messageTimer;

    private void InitializeMessageTimer()
    {
        _messageTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(30)  // Hide message after 30 seconds
        };
        _messageTimer.Tick += (s, e) => ClearStatusMessage();
    }

    #endregion Message Timer

    #region Status Message

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
        SetStatus(PackIconKind.CloseCircle, Brushes.Red, message, 0);
    }

    public void ShowInfo(string message)
    {
        SetStatus(PackIconKind.Information, Brushes.Blue, message, 3);
    }

    #endregion Status Message

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

    ~MainWindowViewModel()
    {
        Dispose(false);
    }
}
