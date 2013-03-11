namespace Supermarket.Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductAvailableIsPositiveCheckMigration : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE ProductAvailabilitiesDetails ADD CONSTRAINT CK_Positive_Product_Amount CHECK (Amount >= 0)");
        }
        
        public override void Down()
        {
            Sql("ALTER TABLE [dbo].[ProductAvailabilitiesDetails] DROP CONSTRAINT [CK_Positive_Product_Amount]");
        }
    }
}
