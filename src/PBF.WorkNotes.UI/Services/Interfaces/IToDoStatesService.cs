namespace PBF.WorkNotes.UI.Services.Interfaces;

public interface IToDoStatesService
{
    IAsyncEnumerable<ToDoState> GetAsync();
}
