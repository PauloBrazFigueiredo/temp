namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025032201, @"Create ""Tags"" table.")]
public class Migration_2025032201 : Migration
{
    public override void Up()
    {
        Create.Table("Tags")
            .WithColumn("Id").AsString().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("IsPermanent").AsBoolean();
    }

    public override void Down()
    {
        Delete.Table("Tags");
    }
}