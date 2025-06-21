namespace PBF.WorkNotes.UI.ViewModels;

public class ToDosViewModel : ViewModelBase
{
    public ToDosViewModel(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
    {
        //YourCommand = new RelayCommand(ExecuteYourCommand);
        //YourCommand1 = new RelayCommand(ExecuteYourCommand1);
        //YourCommand2 = new RelayCommand(ExecuteYourCommand2);
        //YourCommand3 = new RelayCommand<object>(ExecuteYourCommand3);
        //mainViewModel.ShowSuccess("ToDos view initialized successfully.");

        Items = new ObservableCollection<ToDoItem>
        {
            new ToDoItem { Id = Guid.NewGuid(), Title = "Sample Task 1" },
            new ToDoItem { Id = Guid.NewGuid(), Title = "Sample Task 2" },
            new ToDoItem { Id = Guid.NewGuid(), Title = "Sample Task 3" }
        };
    }

    private ObservableCollection<ToDoItem> _items;
    public ObservableCollection<ToDoItem> Items
    {
        get => _items;
        set => SetProperty<ObservableCollection<ToDoItem>>(ref _items, value);
    }

    //    //public ICommand YourCommand { get; }
    //    //public ICommand YourCommand1 { get; }
    //    //public ICommand YourCommand2 { get; }
    //    public ICommand YourCommand3 { get; }


    //    //private void ExecuteYourCommand()
    //    //{
    //    //    _mainViewModel.ShowSuccess("beng");
    //    //}
    //    //private void ExecuteYourCommand1()
    //    //{
    //    //    _mainViewModel.ShowWarning("beng");
    //    //}

    //    //private void ExecuteYourCommand2()
    //    //{
    //    //    _mainViewModel.ShowInfo("beng");
    //    //}

    //    private void ExecuteYourCommand3(object a)
    //    {
    //        _mainViewModel.ExecuteChangeView("ToDo");
    //    }
}