namespace PBF.WorkNotes.Application.Repository.Interfaces;

public interface IToDoStateRepository
{
    Task<IEnumerable<ToDoState>> GetAll();
    Task<ToDoState> GetById(Guid id);
    Task<Guid> Create(ToDoState model);
    Task<bool> Update(ToDoState model);
    Task<bool> Delete(Guid id);
}
