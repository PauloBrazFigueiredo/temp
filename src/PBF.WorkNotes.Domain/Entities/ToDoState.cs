namespace PBF.WorkNotes.Domain.Entities;

public class ToDoState
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool IsDefault { get; set; }
}
