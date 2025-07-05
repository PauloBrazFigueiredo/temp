namespace PBF.WorkNotes.UI.Services;

public  class ToDoItemsService : IToDoItemsService
{
    private readonly IMapper _mapper;
    private readonly IGetToDoUseCase _getToDoUseCase;
    private readonly IGetPriorityUseCase _getPriorityUseCase;
    private readonly IGetToDoStateUseCase _getToDoStateUseCase;

    public ToDoItemsService(
        IMapper mapper,
        IGetToDoUseCase getToDoUseCase,
        IGetPriorityUseCase getPriorityUseCase,
        IGetToDoStateUseCase getToDoStateUseCase)
    {
        _mapper = mapper;
        _getToDoUseCase = getToDoUseCase;
        _getPriorityUseCase = getPriorityUseCase;
        _getToDoStateUseCase = getToDoStateUseCase;
    }

    public async IAsyncEnumerable<ToDoItem> GetAsync()
    {
        var entities = await _getToDoUseCase.Execute();
        foreach (var entity in entities)
        {
            yield return _mapper.Map<ToDoItem>(entity);
        }
    }
}
