namespace PBF.WorkNotes.Domain.Entities;

public class Priority
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Level { get; set; }
    public string Color { get; set; }
    public bool IsDefault { get; set; }
}