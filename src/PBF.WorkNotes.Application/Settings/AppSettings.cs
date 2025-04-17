namespace PBF.WorkNotes.Application.Settings;

public class AppSettings
{
    public AppSettings()
    {
        ConnectionStrings = new List<ConnectionStringSettings>();
    }

    public string Environment { get; set; }
    public List<ConnectionStringSettings> ConnectionStrings { get; set; }
}
