using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PBF.WorkNotes.UI.ViewModels;

public class ToDoViewModel : ViewModelBase
{
    private readonly IToDoService _toDoService;
    private readonly IPrioritiesService _prioritiesService;
    private readonly IToDoStatesService _toDoStatesService;

    public ToDoViewModel(
        MainViewModel mainViewModel,
        IPrioritiesService prioritiesService,
        IToDoStatesService toDoStatesService,
        IToDoService toDoService) : base(mainViewModel)
    {
        _prioritiesService = prioritiesService;
        _toDoStatesService = toDoStatesService;
        _toDoService = toDoService;
        InitializeAsync();

        

        //_getAllPrioritiesUseCase.Execute().ContinueWith(task =>
        //{
        //    if (task.IsCompletedSuccessfully)
        //    {
        //        Priorities = new ObservableCollection<Priority>(task.Result.ToList<Priority>());
        //        SelectedPriority = Priorities.FirstOrDefault();
        //    }
        //});
        //AddCommand = new RelayCommand(ExecuteAddCommand);
        //EditCommand = new RelayCommand(ExecuteEditCommand);
        //DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        //NewCommand = new RelayCommand(ExecuteNewCommand);

        SaveCommand = new RelayCommand(ExecuteSaveCommand);
    }

    private void ExecuteSaveCommand(object obj)
    {
        throw new NotImplementedException();
    }

    private async void InitializeAsync()
    {
        var priorities = await _prioritiesService.GetAllPrioritiesAsync();
        Priorities = new ObservableCollection<Priority>(priorities);
        ToDo.Priority = Priorities.Single(p => p.IsDefault);

        var states = await _toDoStatesService.GetAllToDoStatesAsync();
        States = new ObservableCollection<ToDoState>(states);
        ToDo.State = States.Single(p => p.IsDefault);
    }

    private ToDo _toDo = new ToDo();
    public ToDo ToDo
    {
        get => _toDo;
        set => SetProperty<ToDo>(ref _toDo, value);
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

    public ICommand SaveCommand { get; }
    private void ExecuteSaveCommand()
    {
        var a = string.Empty;
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

    //private ICommand _toggleBoldCommand;
    //public ICommand ToggleBoldCommand => _toggleBoldCommand ??= new RelayCommand(ToggleBold);
    //private void ToggleBold()
    //{
    //    if (_rtb?.Selection == null) return;

    //    var fontWeight = IsBold ? FontWeights.Normal : FontWeights.Bold;
    //    _rtb.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, fontWeight);
    //    IsBold = !IsBold;
    //}

    //private ICommand _toggleItalicCommand;
    //public ICommand ToggleItalicCommand => _toggleItalicCommand ??= new RelayCommand(ToggleItalic);
    //private void ToggleItalic()
    //{
    //    if (_rtb?.Selection == null) return;
    //    var fontStyle = IsItalic ? FontStyles.Normal : FontStyles.Italic;
    //    _rtb.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, fontStyle);
    //    IsItalic = !IsItalic;
    //}

    public void SetRichTextBox(RichTextBox rtb)
    {
        _rtb = rtb;
    }

    private ICommand _selectionChangedCommand;
    public ICommand SelectionChangedCommand => _selectionChangedCommand ??= new RelayCommand(UpdateFormatting);
    private void UpdateFormatting()
    {
        if (_rtb?.Selection == null) return;

        System.Windows.Application.Current.Dispatcher.Invoke(() =>
        {
            // Get current formatting from selected text
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
}
