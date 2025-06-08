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

    public async Task<IEnumerable<ToDoState>> GetAllToDoStatesAsync()
    {
        var entities = await _getAllToDoStatesUseCase.Execute();
        return _mapper.Map<IEnumerable<ToDoState>>(entities);
    }
}
