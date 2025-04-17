namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025040501, @"Create ""ToDoState"" table.")]
public class Migration_2025040501 : Migration
{
    public override void Up()
    {
        Create.Table("ToDoState")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("IsDefault").AsBoolean();
        Insert.IntoTable("ToDoState").Row(new { Id = Guid.NewGuid(), Name = "Active", IsDefault = true});
        Insert.IntoTable("ToDoState").Row(new { Id = Guid.NewGuid(), Name = "Done", IsDefault = false });
    }

    public override void Down()
    {
        Delete.Table("ToDoState");
    }
}