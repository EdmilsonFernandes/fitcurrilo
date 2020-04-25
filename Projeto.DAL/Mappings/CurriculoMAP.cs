using Projeto.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Projeto.DAL.Mappings
{
    public class CurriculoMap : EntityTypeConfiguration<Curriculo>
    {
        public CurriculoMap()
        {
            ToTable("Curriculo");

            HasKey(c => c.Id);

            Property(c => c.Id)
                .HasColumnName("Id");

            Property(c => c.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            Property(c => c.Tamanho)
                .HasColumnName("Tamanho")
                .IsRequired();

            Property(c => c.Tipo)
                .HasColumnName("Tipo")
                .IsRequired();

            Property(c => c.Conteudo)
                .HasColumnName("Conteudo")
                .IsRequired();

            Property(c => c.IdCandidato)
                .HasColumnName("IdCandidato")
               .IsRequired();

            // Mapear o relacionamento de 1 para Muitos
            HasRequired(u => u.Candidato) // Currículo tem 1 Candidato
                .WithMany(e => e.Curriculos) // Candidato TEM muitos currículos
                .HasForeignKey(u => u.IdCandidato); // Chave estrangeira
        }
    }
}
