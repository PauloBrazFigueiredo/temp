namespace PBF.WorkNotes.UI.Services.Interfaces;

public interface IToDoStatesService
{
    Task<IEnumerable<ToDoState>> GetAllToDoStatesAsync();
}
