namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025051102, @"Add ""Priorities"" data.")]
public class Migration_2025051102 : Migration
{
    public override void Up()
    {
        Insert.IntoTable("Priorities").Row(new { Id = Guid.NewGuid(), Name = "Critical", Level = "P0", Color = "#e57373", IsDefault = false });
        Insert.IntoTable("Priorities").Row(new { Id = Guid.NewGuid(), Name = "High", Level = "P1", Color = "#fff59d", IsDefault = false });
        Insert.IntoTable("Priorities").Row(new { Id = Guid.NewGuid(), Name = "Medium", Level = "P2", Color = "#e3f2fd", IsDefault = true });
        Insert.IntoTable("Priorities").Row(new { Id = Guid.NewGuid(), Name = "Low", Level = "P3", Color = "#eceff1", IsDefault = false });
    }

    public override void Down()
    {
        Delete.FromTable("Priorities");
    }
}