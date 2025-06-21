namespace PBF.WorkNotes.Application.UseCases;

public class GetPriorityUseCase : IGetPriorityUseCase
{
    private readonly IPrioritiesRepository _prioritiesRepository;

    public GetPriorityUseCase(IPrioritiesRepository prioritiesRepository)
    {
        _prioritiesRepository = prioritiesRepository;
    }

    public async Task<Priority> Execute(Guid id)
    {
        return await _prioritiesRepository.GetById(id);
    }
}
