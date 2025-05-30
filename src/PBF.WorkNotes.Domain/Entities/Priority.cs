namespace PBF.WorkNotes.Domain.Entities;

public class Priority
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty; // Default value to ensure non-null
    public bool IsDefault { get; set; }
}