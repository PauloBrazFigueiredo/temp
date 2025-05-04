namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025040502, @"Add ""ToDoState"" data.")]
public class Migration_2025040502 : Migration
{
    public override void Up()
    {
        Insert.IntoTable("ToDoState").Row(new { Id = Guid.NewGuid(), Name = "Active", IsDefault = true});
        Insert.IntoTable("ToDoState").Row(new { Id = Guid.NewGuid(), Name = "Done", IsDefault = false });
    }

    public override void Down()
    {
        Delete.FromTable("ToDoState");
    }
}