namespace PBF.WorkNotes.UI.Services;

public class ToDoStatesService : IToDoStatesService
{
    private readonly IMapper _mapper;
    private readonly IGetAllToDoStatesUseCase _getAllToDoStatesUseCase;

    public ToDoStatesService(
        IMapper mapper,
        IGetAllToDoStatesUseCase getAllToDoStatesUseCase)
    {
        _mapper = mapper;
        _getAllToDoStatesUseCase = getAllToDoStatesUseCase;
    }

    public async IAsyncEnumerable<ToDoState> GetAsync()
    {
        var entities = await _getAllToDoStatesUseCase.Execute();
        foreach (var entity in entities)
        {
            yield return _mapper.Map<ToDoState>(entity);
        }
    }
}
