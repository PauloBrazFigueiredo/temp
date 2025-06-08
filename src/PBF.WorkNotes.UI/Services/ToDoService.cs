namespace PBF.WorkNotes.UI.Services;

public  class ToDoService : IToDoService
{
    private readonly IToDosRepository _toDosRepository;

    public ToDoService(IToDosRepository toDosRepository)
    {
        _toDosRepository = toDosRepository;
    }

    //public async Task<IEnumerable<ToDo>> GetAllAsync()
    //{
    //    return await _toDosRepository.GetAll();
    //}
    //public async Task<ToDo> GetByIdAsync(Guid id)
    //{
    //    return await _toDosRepository.GetById(id);
    //}
    public async Task<Guid> CreateAsync(Entities.ToDo model)
    {
        return await _toDosRepository.Create(model);
    }
}
