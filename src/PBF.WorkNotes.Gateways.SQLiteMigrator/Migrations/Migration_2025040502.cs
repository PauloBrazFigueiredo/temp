namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025040502, @"Add ""ToDoStates"" data.")]
public class Migration_2025040502 : Migration
{
    public override void Up()
    {
        Insert.IntoTable("ToDoStates").Row(new { Id = Guid.NewGuid().ToString(), Name = "Active", IsDefault = true});
        Insert.IntoTable("ToDoStates").Row(new { Id = Guid.NewGuid().ToString(), Name = "Done", IsDefault = false });
    }

    public override void Down()
    {
        Delete.FromTable("ToDoStates");
    }
}