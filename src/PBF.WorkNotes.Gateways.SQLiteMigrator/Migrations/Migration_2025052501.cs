namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025052501, @"Create ""ToDos"" table.")]
public class Migration_2025052501 : Migration
{
    public override void Up()
    {
        Create.Table("ToDos")
            .WithColumn("Id").AsString().PrimaryKey()
            .WithColumn("Title").AsString()
            .WithColumn("Notes").AsString().Nullable()
            .WithColumn("StateId").AsString()
                .ForeignKey("FK_ToDoStates_Id", "ToDoStates", "Id")
            .WithColumn("PriorityId").AsString()
                .ForeignKey("FK_Priorities_Id", "Priorities", "Id")
            .WithColumn("IsPrivate").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("Order").AsInt32().Nullable()
            .WithColumn("WorkDate").AsDateTime().Nullable()
            .WithColumn("DueDate").AsDateTime().Nullable()
            .WithColumn("CreatedDate").AsDateTime().NotNullable()
        ;
    }
    public override void Down()
    {
        Delete.FromTable("ToDos");
    }
}