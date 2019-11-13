using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;

namespace Projeto.DAL.Mappings
{
    public class EmbalagemMAP:EntityTypeConfiguration<Embalagem>
    {
        public EmbalagemMAP()
        {
            ToTable("Embalagem");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("Id");

            Property(e => e.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(70)
                .IsRequired();

            Property(e => e.Capacidade)
                .HasColumnName("Capacidade")
                .IsRequired();

            Property(e => e.UnidadeMedida)
                .HasColumnName("UnidadeMedida")
                .HasMaxLength(20)
                .IsRequired();

            Property(e => e.CadastradoPor)
                .HasColumnName("CadastradoPor")
                .HasMaxLength(50)
                .IsOptional();

            Property(e => e.DataCadastro)
                .HasColumnName("DataCadastro")
                .IsRequired()
                .IsOptional();

            Property(e => e.EditadoPor)
                .HasColumnName("EditadoPor")
                .HasMaxLength(50)
                .IsOptional();

            Property(e => e.DataEdicao)
                .HasColumnName("DataEdicao")
                .IsOptional();

            Property(e => e.Descricao)
                .HasColumnName("Descricao")
                .HasMaxLength(250)
                .IsOptional();
        }
    }
}
