namespace PBF.WorkNotes.UI.Services;

public  class ToDosService : IToDosService
{
    private readonly IMapper _mapper;
    private readonly IGetToDoUseCase _getToDoUseCase;
    private readonly ICreateToDoUseCase _createToDoUseCase;
    private readonly IUpdateToDoUseCase _updateToDoUseCase;
    private readonly IGetPriorityUseCase _getPriorityUseCase;
    private readonly IGetToDoStateUseCase _getToDoStateUseCase;

    public ToDosService(
        IMapper mapper,
        IGetToDoUseCase getToDoUseCase,
        ICreateToDoUseCase createToDoUseCase,
        IUpdateToDoUseCase updateToDoUseCase,
        IGetPriorityUseCase getPriorityUseCase,
        IGetToDoStateUseCase getToDoStateUseCase)
    {
        _mapper = mapper;
        _getToDoUseCase = getToDoUseCase;
        _createToDoUseCase = createToDoUseCase;
        _updateToDoUseCase = updateToDoUseCase;
        _getPriorityUseCase = getPriorityUseCase;
        _getToDoStateUseCase = getToDoStateUseCase;
    }

    public async Task<ToDo> GetByIdAsync(Guid id)
    {
        var entity = await _getToDoUseCase.Execute(id);
        var model = _mapper.Map<ToDo>(entity);
        var priority = await _getPriorityUseCase.Execute(entity.PriorityId);
        model.Priority = _mapper.Map<Priority>(priority);
        var state = await _getToDoStateUseCase.Execute(entity.StateId);
        model.State = _mapper.Map<ToDoState>(state);
        return model;
    }

    public async Task<Guid?> CreateAsync(ToDo model)
    {
        var entity = _mapper.Map<Entities.ToDo>(model);
        return await _createToDoUseCase.Execute(entity);
    }

    public async Task<bool> UpdateAsync(ToDo model)
    {
        var entity = _mapper.Map<Entities.ToDo>(model);
        return await _updateToDoUseCase.Execute(entity);
    }
}
