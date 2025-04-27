namespace PBF.WorkNotes.Application.Repository.Interfaces;

public interface IToDoStateRepository
{
    Task<IEnumerable<ToDoState>> GetAll();
    Task<ToDoState> GetById(Guid id);
    Task<Guid> Create(ToDoState model);
    Task<int> Update(ToDoState model);
    Task<int> Delete(Guid id);
}
