namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025040501, @"Create ""ToDoStates"" table.")]
public class Migration_2025040501 : Migration
{
    public override void Up()
    {
        Create.Table("ToDoStates")
            .WithColumn("Id").AsString().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("IsDefault").AsBoolean();
    }

    public override void Down()
    {
        Delete.Table("ToDoStates");
    }
}