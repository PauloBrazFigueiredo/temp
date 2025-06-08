namespace PBF.WorkNotes.Gateways.SQLiteMigrator.Migrations;

[Migration(2025052501, @"Create ""ToDos"" table.")]
public class Migration_2025052501 : Migration
{
    public override void Up()
    {
        Create.Table("ToDos")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Title").AsString()
            .WithColumn("Notes").AsString()
            .WithColumn("StateId").AsGuid()
                .ForeignKey("FK_ToDoStates_Id", "ToDoStates", "Id")
            .WithColumn("PriorityId").AsGuid()
                .ForeignKey("FK_Priorities_Id", "Priorities", "Id")
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