namespace PBF.WorkNotes.Application.Repositories.Interfaces;

public interface IToDosRepository
{
    Task<IEnumerable<ToDo>> GetAll();
    Task<ToDo> GetById(Guid id);
    Task<Guid> Create(ToDo model);
    Task<bool> Update(ToDo model);
    Task<bool> Delete(Guid id);
}
