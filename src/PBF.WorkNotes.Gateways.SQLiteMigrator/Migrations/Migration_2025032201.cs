namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025032201, @"Create ""Tags"" table.")]
public class Migration_2025032201 : Migration
{
    public override void Up()
    {
        Create.Table("Tags")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("IsPermanent").AsBoolean()
            .WithColumn("Name").AsString();
    }

    public override void Down()
    {
        Delete.Table("Tags");
    }
}