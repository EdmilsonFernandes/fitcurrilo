namespace Projeto.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoRecrutador1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Recrutador", name: "Nome", newName: "Email");
            AddColumn("dbo.Recrutador", "Email1", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recrutador", "Email1");
            RenameColumn(table: "dbo.Recrutador", name: "Email", newName: "Nome");
        }
    }
}
