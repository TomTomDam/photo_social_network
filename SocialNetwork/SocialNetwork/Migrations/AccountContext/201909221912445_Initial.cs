namespace SocialNetwork.Migrations.AccountContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        email = c.String(),
                        password = c.String(),
                        rememberMe = c.Boolean(nullable: false),
                        confirmPassword = c.String(),
                        oldPassword = c.String(),
                        newPassword = c.String(),
                        confirmResetPassword = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}
