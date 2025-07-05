namespace PBF.WorkNotes.UI.Services.Interfaces;

public interface IToDosService
{
    Task<ToDo> GetAsync(Guid id);
    Task<Guid?> CreateAsync(ToDo model);
    Task<bool> UpdateAsync(ToDo model);
}
