namespace PBF.WorkNotes.Gateways.SQLiteGateway.Models;

public class ToDoStateModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool IsDefault { get; set; }
}
