namespace PBF.WorkNotes.UI.Services;

public  class ToDoStateService : IToDoStateService
{
    private readonly IToDoStatesRepository _toDoStateRepository;

    public ToDoStateService(IToDoStatesRepository toDoStateRepository)
    {
        _toDoStateRepository = toDoStateRepository;
    }
}
