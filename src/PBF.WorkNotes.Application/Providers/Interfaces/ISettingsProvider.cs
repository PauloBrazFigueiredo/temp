namespace PBF.WorkNotes.Application.Providers.Interfaces;

public interface ISettingsProvider
{
    AppSettings? Settings { get; set; }
    string GetWorkNotesDataDatabaseConnectionString();
}
