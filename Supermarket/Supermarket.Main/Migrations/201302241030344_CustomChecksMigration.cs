namespace Supermarket.Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomChecksMigration : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE CashDesk ADD CONSTRAINT CK_Positive_Money_Amount CHECK (AvailableAmount >= 0)");
            
        }
        
        public override void Down()
        {
            Sql("ALTER TABLE [dbo].[CashDesk] DROP CONSTRAINT [CK_Positive_Money_Amount]");
        }
    }
}
