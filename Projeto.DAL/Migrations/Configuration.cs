namespace Projeto.DAL.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Projeto.DAL.DataSource.Conexao>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(Projeto.DAL.DataSource.Conexao context)
        {            
                context.Perfil.AddOrUpdate(                  
                  new Perfil { IdPerfil = 1, Nome = "DEFAULT" },
                  new Perfil { IdPerfil = 2, Nome = "ADMIN" }
                );            
        }
    }
}
