namespace PBF.WorkNotes.Application.UseCases.Interfaces;

public interface IGetAllPrioritiesUseCase
{
    public Task<IEnumerable<Priority>> Execute();
}
