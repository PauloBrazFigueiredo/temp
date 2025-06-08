namespace PBF.WorkNotes.Application.UseCases;

public class GetAllToDoStatesUseCase  : IGetAllToDoStatesUseCase
{
    private readonly IToDoStatesRepository _toDoStatesRepository;

    public GetAllToDoStatesUseCase(IToDoStatesRepository toDoStatesRepository)
    {
        _toDoStatesRepository = toDoStatesRepository;
    }

    public async Task<IEnumerable<ToDoState>> Execute()
    {
        return await _toDoStatesRepository.GetAll();
    }
}
