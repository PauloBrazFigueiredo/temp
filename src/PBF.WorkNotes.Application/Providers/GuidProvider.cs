namespace PBF.WorkNotes.Application.Providers;

public class GuidProvider : IGuidProvider
{
    public Guid GetGuid()
    {
        return Guid.NewGuid();
    }
}
