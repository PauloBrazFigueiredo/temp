namespace PBF.WorkNotes.Gateways.SQLiteGateway.Models;

public class ToDoModel
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string StateId { get; set; }
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    public string PriorityId { get; set; }
    public int? Order { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime? WorkDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedDate { get; set; }
}