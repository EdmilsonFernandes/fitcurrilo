namespace Projeto.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicaoRecrutador : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recrutador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 70),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recrutador");
        }
    }
}
