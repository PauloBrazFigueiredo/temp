namespace PBF.WorkNotes.UI.Services.Interfaces;

public interface IPrioritiesService
{
    IAsyncEnumerable<Priority> GetAsync();
}
