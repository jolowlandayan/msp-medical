namespace msp_medical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class getStatebutneedstobeupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.State", "UserId", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.State", "UserId");
        }
    }
}
