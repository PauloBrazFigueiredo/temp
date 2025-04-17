namespace PBF.WorkNotes.Domain.Entities;

public class Tag
{
    public Guid Id { get; set; }
    public bool IsPermanent{ get; set; }
    public string Name { get; set; }
}
