namespace PBF.WorkNotes.UI.Services.Interfaces;

public interface IToDoItemsService
{
    IAsyncEnumerable<ToDoItem> GetAsync();
}
