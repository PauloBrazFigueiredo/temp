namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025051101, @"Create ""Priorities"" table.")]
public class Migration_2025051101 : Migration
{
    public override void Up()
    {
        Create.Table("Priorities")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("Level").AsString()
            .WithColumn("Color").AsString()
            .WithColumn("IsDefault").AsBoolean();
    }

    public override void Down()
    {
        Delete.Table("Priorities");
    }
}