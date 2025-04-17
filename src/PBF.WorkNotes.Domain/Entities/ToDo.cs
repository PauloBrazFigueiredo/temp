namespace PBF.WorkNotes.Domain.Entities;

public class ToDo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ToDoState State { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
    public ToDoPriority Priority { get; set; }
    public int? Order { get; set; }
    public DateTime? WorkDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
}
