namespace PBF.WorkNotes.UI.Services;

public class PrioritiesService : IPrioritiesService
{
    private readonly IMapper _mapper;
    private readonly IGetAllPrioritiesUseCase _getAllPrioritiesUseCase;

    public PrioritiesService(
        IMapper mapper,
        IGetAllPrioritiesUseCase getAllPrioritiesUseCase)
    {
        _mapper = mapper;
        _getAllPrioritiesUseCase = getAllPrioritiesUseCase;
    }

    public async IAsyncEnumerable<Priority> GetAsync()
    {
        var entities = await _getAllPrioritiesUseCase.Execute();
        foreach (var entity in entities)
        {
            yield return _mapper.Map<Priority>(entity);
        }
    }
}
