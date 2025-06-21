using System.Collections;

namespace PBF.WorkNotes.UI.ViewModels;

public class ToDoViewModel : ViewModelBase
{
    private readonly IValidator<ToDo> _validator;
    private readonly IToDosService _toDosService;
    private readonly IPrioritiesService _prioritiesService;
    private readonly IToDoStatesService _toDoStatesService;

    public ToDoViewModel(
        MainWindowViewModel mainWindowViewModel,
        IPrioritiesService prioritiesService,
        IToDoStatesService toDoStatesService,
        IToDosService toDosService) : base(mainWindowViewModel)
    {
        _prioritiesService = prioritiesService;
        _toDoStatesService = toDoStatesService;
        _toDosService = toDosService;
        _validator = new ToDoValidator();
        InitializeAsync();

        //AddCommand = new RelayCommand(ExecuteAddCommand);
        //EditCommand = new RelayCommand(ExecuteEditCommand);
        //DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        //NewCommand = new RelayCommand(ExecuteNewCommand);
    }

    private async void InitializeAsync()
    {
        var priorities = await _prioritiesService.GetAllPrioritiesAsync();
        Priorities = new ObservableCollection<Priority>(priorities);
        var states = await _toDoStatesService.GetAllToDoStatesAsync();
        States = new ObservableCollection<ToDoState>(states);

        if (Mode == ViewModelMode.Create)
        {
            ToDo = new ToDo
            {
                Priority = Priorities.Single(p => p.IsDefault),
                State = States.Single(p => p.IsDefault)
            };
            CanSave = true;
        }
    }

    private ToDo _toDo = new ToDo();
    public ToDo ToDo
    {
        get => _toDo;
        set
        {
            SetProperty<ToDo>(ref _toDo, value);
        }
    }

    private ObservableCollection<Priority> _priorities;
    public ObservableCollection<Priority> Priorities
    {
        get => _priorities;
        set => SetProperty<ObservableCollection<Priority>>(ref _priorities, value);
    }

    private ObservableCollection<ToDoState> _states;
    public ObservableCollection<ToDoState> States
    {
        get => _states;
        set => SetProperty<ObservableCollection<ToDoState>>(ref _states, value);
    }

    private bool _canSave = true;
    public bool CanSave
    {
        get => _canSave;
        set
        {
            _canSave = value;
            OnPropertyChanged(nameof(CanSave));
        }
    }

    public ICommand SaveCommand => new RelayCommand(ExecuteSaveCommand);
    private async Task ExecuteSaveCommand()
    {
        if (Validate())
        {
            if (Mode == ViewModelMode.Create)
            {
                var result =  await _toDosService.CreateAsync(ToDo);
                if (result is not null)
                {
                    ToDo = await _toDosService.GetByIdAsync(result.Value);
                    Mode = ViewModelMode.Edit;
                    _mainWindowViewModel.ShowSuccess($"To-Do item created successfully (Id: {ToDo.Id}).");
                }
                else
                {
                    _mainWindowViewModel.ShowError($"Error creating To-Do item.");
                }
            }
            else if (Mode == ViewModelMode.Edit)
            {
                var result = await _toDosService.UpdateAsync(ToDo);
                if (result)
                {
                    _mainWindowViewModel.ShowSuccess($"To-Do item updated successfully (Id: {ToDo.Id}).");
                }
                else
                {
                    _mainWindowViewModel.ShowError($"Error updating To-Do item (Id: {ToDo.Id}).");
                }
            }
        }
    }

    public ICommand CancelCommand => new RelayCommand(ExecuteCancelCommand);
    private void ExecuteCancelCommand()
    {
        var a = string.Empty;
    }

    private bool Validate()
    {
        foreach (var error in _validator.Validate(ToDo).Errors)
        {
            _mainWindowViewModel.ShowError(error.ErrorMessage);
            return false;
        }
        return true;
    }

    //public ICommand AddCommand { get; }
    //public ICommand EditCommand { get; }
    //public ICommand DeleteCommand { get; }
    //public ICommand NewCommand { get; }

    //private void ExecuteNewCommand()
    //{
    //    throw new NotImplementedException();
    //}

    //private void ExecuteDeleteCommand()
    //{
    //    throw new NotImplementedException();
    //}

    //private void ExecuteEditCommand()
    //{
    //    throw new NotImplementedException();
    //}

    //private void ExecuteAddCommand()
    //{
    //    throw new NotImplementedException();
    //}

    #region Notes

    private RichTextBox _rtb;

    private bool _isBold;
    public bool IsBold
    {
        get => _isBold;
        set => SetProperty(ref _isBold, value);
    }

    private bool _isItalic;
    public bool IsItalic
    {
        get => _isItalic;
        set => SetProperty(ref _isItalic, value);
    }

    public void SetRichTextBox(RichTextBox rtb)
    {
        _rtb = rtb;
    }

    private ICommand _selectionChangedCommand;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public ICommand SelectionChangedCommand => _selectionChangedCommand ??= new RelayCommand(UpdateFormatting);
    private void UpdateFormatting()
    {
        if (_rtb?.Selection == null) return;

        System.Windows.Application.Current.Dispatcher.Invoke(() =>
        {
            var fontWeight = _rtb.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            IsBold = fontWeight != DependencyProperty.UnsetValue && fontWeight.Equals(FontWeights.Bold);

            var fontStyle = _rtb.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            IsItalic = fontStyle != DependencyProperty.UnsetValue && fontStyle.Equals(FontStyles.Italic);
        });
    }

    public FlowDocument FormattedDocumentContent
    {
        get => StringToFlowDocument(_toDo.Notes);
        set => _toDo.Notes = FlowDocumentToString(value);
    }

    private FlowDocument StringToFlowDocument(string text)
    {
        if (string.IsNullOrEmpty(text))
            return new FlowDocument(new Paragraph(new Run("")));

        var flowDoc = new FlowDocument();
        var range = new TextRange(flowDoc.ContentStart, flowDoc.ContentEnd);

        try
        {
            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(text)))
            {
                range.Load(stream, DataFormats.Rtf);
            }
        }
        catch
        {
            range.Text = text;
        }

        return flowDoc;
    }

    private string FlowDocumentToString(FlowDocument document)
    {
        if (document == null) return string.Empty;

        var range = new TextRange(document.ContentStart, document.ContentEnd);

        using (var stream = new MemoryStream())
        {
            range.Save(stream, DataFormats.Rtf);
            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }
    }

    public IEnumerable GetErrors(string? propertyName)
    {
        throw new NotImplementedException();
    }

    #endregion Notes
}
