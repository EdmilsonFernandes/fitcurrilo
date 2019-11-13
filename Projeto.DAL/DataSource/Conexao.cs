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

        //sobrescrever o método OnModelCreating..
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //definir para o EF cada classe de mapeamento..
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new PerfilMap());
            modelBuilder.Configurations.Add(new AprovacaoCurriculosMAP());
            modelBuilder.Configurations.Add(new ProdutoMAP());
            modelBuilder.Configurations.Add(new EmbalagemMAP());
            modelBuilder.Configurations.Add(new ContatoMap());
            modelBuilder.Configurations.Add(new EmpresaMap());
            
        }

        //declarar um DbSet para cada entidade..
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<AprovacaoCurriculos> AprovacaoPedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Embalagem> Embalagem { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Contato> Contato { get; set; }
         
    }
}
