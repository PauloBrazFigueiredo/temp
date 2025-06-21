namespace PBF.WorkNotes.UI.ViewModels;

public class RelayCommand : ICommand
{
    private readonly Action _executeSync;
    private readonly Func<Task> _executeAsync;
    private readonly Func<bool> _canExecute;
    private bool _isExecuting;

    // Constructor for SYNC (void) methods
    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _executeSync = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    // Constructor for ASYNC (Task) methods
    public RelayCommand(Func<Task> executeAsync, Func<bool> canExecute = null)
    {
        _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => !_isExecuting && (_canExecute?.Invoke() ?? true);

    public async void Execute(object parameter)
    {
        if (CanExecute(parameter))
        {
            try
            {
                _isExecuting = true;
                RaiseCanExecuteChanged();

                if (_executeAsync != null)
                    await _executeAsync().ConfigureAwait(false); // Handle async
                else
                    _executeSync(); // Handle sync
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }
    }

    public event EventHandler CanExecuteChanged;
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

/*
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<Task> _executeAsync;
    private readonly Func<bool> _canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    //public RelayCommand(Func<Task> execute, Func<bool> canExecute = null)
    //{
    //    _executeAsync = _executeAsync ?? throw new ArgumentNullException(nameof(execute));
    //    _canExecute = canExecute;
    //}

    public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

    public void Execute(object parameter) => _execute();

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
*/

public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _execute;
    private readonly Func<T, bool> _canExecute;

    public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) =>
        _canExecute?.Invoke((T)parameter) ?? true;

    public void Execute(object parameter) => _execute((T)parameter);

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
