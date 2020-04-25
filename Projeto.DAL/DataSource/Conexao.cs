using Projeto.DAL.Mappings; // mapeamento..
using Projeto.Entities; // entidades
using System.Configuration; //connectionstring..
using System.Data.Entity; //entityframework..


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
            modelBuilder.Configurations.Add(new CandidatoMap());
            modelBuilder.Configurations.Add(new CurriculoMap());
            modelBuilder.Configurations.Add(new PerfilMap());
            modelBuilder.Configurations.Add(new SituacaoMap());
            modelBuilder.Configurations.Add(new RecrutadorMap());
            modelBuilder.Configurations.Add(new TecnologiaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new VagaMap());
        }

        // Declarar um DbSet para cada entidade..
        public DbSet<Candidato> Candidato { get; set; }
        public DbSet<Curriculo> Curriculo { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Situacao> Situacao { get; set; }
        public DbSet<Recrutador> Recrutador { get; set; }
        public DbSet<Tecnologia> Tecnologia { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Vaga> Vaga { get; set; }
    }
}
