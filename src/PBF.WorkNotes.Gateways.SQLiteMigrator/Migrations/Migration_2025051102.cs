namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025051102, @"Add ""Priorities"" data.")]
public class Migration_2025051102 : Migration
{
    public override void Up()
    {
        Insert.IntoTable("Priorities").Row(new { Id = Guid.NewGuid(), Name = "Critical", Level = "P0", Color = "0xff0000", IsDefault = false });
        Insert.IntoTable("Priorities").Row(new { Id = Guid.NewGuid(), Name = "High", Level = "P1", Color = "0xff4500", IsDefault = false });
        Insert.IntoTable("Priorities").Row(new { Id = Guid.NewGuid(), Name = "Medium", Level = "P2", Color = "0xffd700", IsDefault = true });
        Insert.IntoTable("Priorities").Row(new { Id = Guid.NewGuid(), Name = "Low", Level = "P3", Color = "0xa9a9a9", IsDefault = false });
    }

    public override void Down()
    {
        Delete.FromTable("Priorities");
    }
}