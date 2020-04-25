namespace Projeto.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoVaga : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vaga",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descriacao = c.String(nullable: false),
                        Situacao = c.String(nullable: false),
                        Salario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoContratacao = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(nullable: false),
                        CadastradoPor = c.String(nullable: false),
                        AtualizadoPor = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vaga");
        }
    }
}
