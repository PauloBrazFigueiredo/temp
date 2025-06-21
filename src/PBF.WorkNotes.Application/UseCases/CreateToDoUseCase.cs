namespace PBF.WorkNotes.Application.UseCases;

public class CreateToDoUseCase : ICreateToDoUseCase
{
    private readonly IToDosRepository _toDosRepository;

    public CreateToDoUseCase(IToDosRepository toDosRepository)
    {
        _toDosRepository = toDosRepository;
    }
    public async Task<Guid> Execute(ToDo entity)
    {
        return await _toDosRepository.Create(entity);
    }
}
