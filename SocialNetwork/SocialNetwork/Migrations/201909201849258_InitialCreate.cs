namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        commentId = c.Int(nullable: false, identity: true),
                        photoId = c.Int(nullable: false),
                        username = c.String(),
                        text = c.String(),
                    })
                .PrimaryKey(t => t.commentId)
                .ForeignKey("dbo.Photos", t => t.photoId, cascadeDelete: true)
                .Index(t => t.photoId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        photoId = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        photoFile = c.Binary(),
                        imageMimeType = c.String(),
                        description = c.String(),
                        username = c.String(),
                        createdDate = c.DateTime(nullable: false),
                        modifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.photoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "photoId", "dbo.Photos");
            DropIndex("dbo.Comments", new[] { "photoId" });
            DropTable("dbo.Photos");
            DropTable("dbo.Comments");
        }
    }
}
