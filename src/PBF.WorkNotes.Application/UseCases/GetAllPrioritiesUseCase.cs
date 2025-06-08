namespace PBF.WorkNotes.Application.UseCases;

public class GetAllPrioritiesUseCase : IGetAllPrioritiesUseCase
{
    private readonly IPrioritiesRepository _prioritiesRepository;

    public GetAllPrioritiesUseCase(IPrioritiesRepository prioritiesRepository)
    {
        _prioritiesRepository = prioritiesRepository;
    }

    public async Task<IEnumerable<Priority>> Execute()
    {
        return await _prioritiesRepository.GetAll();
    }
}
