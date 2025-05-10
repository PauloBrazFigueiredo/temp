namespace PBF.WorkNotes.Gateways.SQLiteGateway.Models;

public class TagModel
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsPermanent { get; set; }
}
