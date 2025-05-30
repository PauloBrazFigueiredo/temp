namespace PBF.WorkNotes.Gateways.SQLiteGateway.Models;

public class PriorityModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
}
