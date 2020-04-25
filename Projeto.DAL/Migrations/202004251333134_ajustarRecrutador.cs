namespace Projeto.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajustarRecrutador : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Recrutador", name: "Email", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Recrutador", name: "Email1", newName: "Email");
            RenameColumn(table: "dbo.Recrutador", name: "__mig_tmp__0", newName: "Nome");
            AlterColumn("dbo.Recrutador", "Email", c => c.String(maxLength: 70));
            AlterColumn("dbo.Recrutador", "Email", c => c.String(maxLength: 70));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recrutador", "Email", c => c.String());
            AlterColumn("dbo.Recrutador", "Email", c => c.String(nullable: false, maxLength: 70));
            RenameColumn(table: "dbo.Recrutador", name: "Nome", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Recrutador", name: "Email", newName: "Email1");
            RenameColumn(table: "dbo.Recrutador", name: "__mig_tmp__0", newName: "Email");
        }
    }
}
