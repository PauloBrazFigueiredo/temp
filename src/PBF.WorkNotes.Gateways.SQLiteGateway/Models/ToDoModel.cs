namespace PBF.WorkNotes.Gateways.SQLiteGateway.Models;

public class ToDoModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid StateId { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
    public Guid PriorityId { get; set; }
    public int? Order { get; set; }
    public DateTime? WorkDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
}