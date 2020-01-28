using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; //connectionstring..
using System.Data.Entity; //entityframework..
using Projeto.DAL.Mappings; //mapeamento..
using Projeto.Entities; //entidades


namespace Projeto.DAL.DataSource
{
    //Regra 1) A Classe de conexão deverá HERDAR DbContext
    public class Conexao : DbContext
    {
        //Regra 2) Informar para o DbContext a connectionstring..
        public Conexao()
            : base(ConfigurationManager.ConnectionStrings["fitCurriculos"].ConnectionString)
        {

        }

        // Sobrescrever o método OnModelCreating..
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //definir para o EF cada classe de mapeamento..
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new PerfilMap());
            modelBuilder.Configurations.Add(new CandidatoMap());
            modelBuilder.Configurations.Add(new SituacaoMap());
            modelBuilder.Configurations.Add(new CurriculoMap());
        }

        // Declarar um DbSet para cada entidade..
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Candidato> Candidato { get; set; }
        public DbSet<Curriculo> Curriculo { get; set; }
        public DbSet<Situacao> Situacao { get; set; }

    }
}
