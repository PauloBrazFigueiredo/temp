namespace PBF.WorkNotes.Application.Providers;

public class SettingsProvider : ISettingsProvider
{
    public const string DataDataseName = "WorkNotesData";
    public AppSettings? Settings { get; set; }

    public SettingsProvider()
    {
        string jsonFilePath;

#if DEBUG
        jsonFilePath = "appsettings.Debug.json";  // Use Debug configuration
#elif RELEASE
    jsonFilePath = "appsettings.Release.json";  // Use Release configuration
#else
    jsonFilePath = "appsettings.Debug.json";  // Fallback configuration (for other modes)
#endif

        if (File.Exists(jsonFilePath))
        {
            string jsonString = File.ReadAllText(jsonFilePath);
            Settings = JsonSerializer.Deserialize<AppSettings>(jsonString);    
        }
    }

    public string GetWorkNotesDataDatabaseConnectionString()
    {
        return GetConnectionString(Settings, DataDataseName);
    }

    public static string GetConnectionString(AppSettings settings, string name)
    {
        if (settings.ConnectionStrings.Any(x => x.Name == name))
            return settings.ConnectionStrings.Single(x => x.Name == name).ConnectionString;
        return string.Empty;
    }
}
