using System.Diagnostics;

namespace PBF.WorkNotes.UI.ViewModels;

public class ToDosViewModel : ViewModelBase
{
    private readonly IToDoItemsService _toDoItemsService;

    public ToDosViewModel(
        MainWindowViewModel mainWindowViewModel,
        IToDoItemsService toDoItemsService) : base(mainWindowViewModel)
    {
        _toDoItemsService = toDoItemsService;
        Initialize();

        //YourCommand = new RelayCommand(ExecuteYourCommand);
        //YourCommand1 = new RelayCommand(ExecuteYourCommand1);
        //YourCommand2 = new RelayCommand(ExecuteYourCommand2);
        //YourCommand3 = new RelayCommand<object>(ExecuteYourCommand3);
        //mainViewModel.ShowSuccess("ToDos view initialized successfully.");

        //Items = new ObservableCollection<ToDoItem>
        //{
        //    new ToDoItem { Id = Guid.NewGuid(), Title = "Sample Task 1", StateName="Active", PriorityColor = Brushes.Aqua },
        //    new ToDoItem { Id = Guid.NewGuid(), Title = "Sample Task 2", StateName="Active", PriorityColor = Brushes.Beige, WorkDate = new DateTime(2025, 6, 1), DueDate = new DateTime(2025, 6, 1)},
        //    new ToDoItem { Id = Guid.NewGuid(), Title = "Sample Task 3", StateName="Done", PriorityColor = Brushes.Yellow, WorkDate = new DateTime(2025, 6, 1), DueDate = new DateTime(2025, 6, 1)},
        //    new ToDoItem { Id = Guid.NewGuid(), Title = "Sample Task 4", StateName="Active", PriorityColor = Brushes.BlueViolet, WorkDate = new DateTime(2025, 6, 1)},
        //    new ToDoItem { Id = Guid.NewGuid(), Title = "Sample Task 5", StateName="Done", PriorityColor = Brushes.GreenYellow, DueDate = new DateTime(2025, 6, 1)},
        //    new ToDoItem { Id = Guid.NewGuid(), Title = "Sample Task 6", StateName="Active", PriorityColor = Brushes.Honeydew, WorkDate = new DateTime(2025, 6, 1), DueDate = new DateTime(2025, 6, 1)}
        //};
    }

    private ObservableCollection<ToDoItem> _items;
    public ObservableCollection<ToDoItem> Items
    {
        get => _items;
        set => SetProperty<ObservableCollection<ToDoItem>>(ref _items, value);
    }

    public ICommand DoubleClickCommand => new RelayCommand<ToDoItem>(ExecuteDoubleClickCommand);
    private void ExecuteDoubleClickCommand(ToDoItem item)
    {
        Debug.WriteLine($"Double-clicked item: {item?.Title}");
        // Your actual double-click logic here
        
    }

    public ICommand ItemClickCommand => new RelayCommand<ToDoItem>(item =>
    {
        Debug.WriteLine($"Double-clicked item: {item?.Title}");
    });

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
    private void Initialize()
    {
        var items = _toDoItemsService.GetAsync();
        Items = new ObservableCollection<ToDoItem>(items.ToBlockingEnumerable());
    }
}