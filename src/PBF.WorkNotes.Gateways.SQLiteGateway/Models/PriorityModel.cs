namespace PBF.WorkNotes.Gateways.SQLiteGateway.Models;

public class PriorityModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Level { get; set; }
    public string Color { get; set; }
    public bool IsDefault { get; set; }
}
