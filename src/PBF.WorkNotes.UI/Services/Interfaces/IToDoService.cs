namespace PBF.WorkNotes.UI.Services.Interfaces;

public interface IToDoService
{
    Task<Guid> CreateAsync(Entities.ToDo model);
}
