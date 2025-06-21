namespace PBF.WorkNotes.Application.UseCases;

public class GetToDoStateUseCase  : IGetToDoStateUseCase
{
    private readonly IToDoStatesRepository _toDoStatesRepository;

    public GetToDoStateUseCase(IToDoStatesRepository toDoStatesRepository)
    {
        _toDoStatesRepository = toDoStatesRepository;
    }

    public async Task<ToDoState> Execute(Guid id)
    {
        return await _toDoStatesRepository.GetById(id);
    }
}
