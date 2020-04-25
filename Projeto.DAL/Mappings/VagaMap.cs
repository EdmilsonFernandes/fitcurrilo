using Projeto.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Projeto.DAL.Mappings
{
    public class VagaMap : EntityTypeConfiguration<Vaga>
    {
        public VagaMap()
        {
            ToTable("Vaga");

            HasKey(v => v.Id);

            Property(v => v.Id)
                .HasColumnName("Id");

            Property(v => v.Descricao)
                .HasColumnName("Descriacao")
                .IsRequired();

            Property(v => v.Situacao)
                .HasColumnName("Situacao")
                .IsRequired();

            Property(v => v.Salario)
                .HasColumnName("Salario")
                .IsRequired();

            Property(v => v.TipoContratacao)
                .HasColumnName("TipoContratacao")
                .IsRequired();

            Property(v => v.DataCadastro)
                .HasColumnName("DataCadastro")
                .IsRequired();

            Property(v => v.CadastradoPor)
                .HasColumnName("CadastradoPor")
                .IsRequired();

            Property(v => v.DataAtualizacao)
                .HasColumnName("DataAtualizacao")
                .IsRequired();

            Property(v => v.AtualizadoPor)
                .HasColumnName("AtualizadoPor")
                .IsRequired();
        }
    }
}
