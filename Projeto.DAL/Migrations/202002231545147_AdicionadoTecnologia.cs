namespace Projeto.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AdicionadoTecnologia : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TecnologiaMap", newName: "Tecnologia");
        }

        public override void Down()
        {
            RenameTable(name: "dbo.Tecnologia", newName: "TecnologiaMap");
        }
    }
}
