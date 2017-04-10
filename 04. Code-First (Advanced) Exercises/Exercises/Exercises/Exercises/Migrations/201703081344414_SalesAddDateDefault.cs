namespace Exercises.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesAddDateDefault : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Sales", "Date", d => d.DateTime(defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("Sales", "Date", d => d.DateTime(defaultValueSql: null));
        }
    }
}
