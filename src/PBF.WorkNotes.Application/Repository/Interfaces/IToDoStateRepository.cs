namespace PBF.WorkNotes.Application.Repository.Interfaces;

public interface IToDoStateRepository
{
    Task<IEnumerable<ToDoState>> GetAll();
    Task<ToDoState> GetById(Guid id);
    Task Create(ToDoState model);
    Task Update(ToDoState model);
    Task Delete(int id);
}
