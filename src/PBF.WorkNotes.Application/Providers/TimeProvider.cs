namespace PBF.WorkNotes.Application.Providers;

public class TimeProvider : ITimeProvider
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}
