namespace PBF.WorkNotes.Domain.Entities;

public class ToDo
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public Guid StateId { get; set; }
    public required ToDoState State { get; set; } 
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    public Guid PriorityId { get; set; }
    public required Priority Priority { get; set; }
    public bool IsPrivate { get; set; }
    public int? Order { get; set; }
    public DateTime? WorkDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CreatedDate { get; set; }
}
