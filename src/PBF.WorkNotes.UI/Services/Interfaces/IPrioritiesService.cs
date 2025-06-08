namespace PBF.WorkNotes.UI.Services.Interfaces;

public interface IPrioritiesService
{
    Task<IEnumerable<Priority>> GetAllPrioritiesAsync();
}
