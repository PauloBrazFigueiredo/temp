namespace PBF.WorkNotes.Application.UseCases;

public class UpdateToDoUseCase : IUpdateToDoUseCase
{
    private readonly IToDosRepository _toDosRepository;

    public UpdateToDoUseCase(IToDosRepository toDosRepository)
    {
        _toDosRepository = toDosRepository;
    }
    public async Task<bool> Execute(ToDo entity)
    {
        return await _toDosRepository.Update(entity);
    }
}
