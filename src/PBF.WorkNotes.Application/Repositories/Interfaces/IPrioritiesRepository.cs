namespace PBF.WorkNotes.Application.Repositories.Interfaces;

public interface IPrioritiesRepository
{
    Task<IEnumerable<Priority>> GetAll();
    Task<Priority> GetById(Guid id);
    Task<Guid> Create(Priority model);
    Task<bool> Update(Priority model);
    Task<bool> Delete(Guid id);
}
