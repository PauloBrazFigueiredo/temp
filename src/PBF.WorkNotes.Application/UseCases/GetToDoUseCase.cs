namespace PBF.WorkNotes.Application.UseCases;

public class GetToDoUseCase : IGetToDoUseCase
{
    private readonly IToDosRepository _toDosRepository;

    public GetToDoUseCase(IToDosRepository toDosRepository)
    {
        _toDosRepository = toDosRepository;
    }
    public async Task<ToDo> Execute(Guid id)
    {
        return await _toDosRepository.GetById(id);
    }

    public async Task<IEnumerable<ToDo>> Execute()
    {
        return await _toDosRepository.GetAll();
    }
}
