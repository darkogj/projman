namespace ProjectManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdditionalUserDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DisplayName", c => c.String());
            AddColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.AspNetUsers", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DateCreated");
            DropColumn("dbo.AspNetUsers", "IsActive");
            DropColumn("dbo.AspNetUsers", "DisplayName");
        }
    }
}
