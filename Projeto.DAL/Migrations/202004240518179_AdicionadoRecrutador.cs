namespace Projeto.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoRecrutador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidato", "Recrutador", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidato", "Recrutador");
        }
    }
}
