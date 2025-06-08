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

    public async Task<IEnumerable<Priority>> GetAllPrioritiesAsync()
    {
        var entities = await _getAllPrioritiesUseCase.Execute();
        return _mapper.Map<IEnumerable<Priority>>(entities);
    }
}
