using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Mappings
{
    public class CandidatoMap : EntityTypeConfiguration<Candidato>
    {
        public CandidatoMap()
        {
            ToTable("Candidato");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("Id");

            Property(e => e.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(70)
                .IsRequired();

            Property(e => e.GrauInstrucao)
                .HasColumnName("GrauInstrucao")
                .HasMaxLength(50)
                .IsOptional();

            Property(e => e.Situacao)
                .HasColumnName("Situacao")
                .HasMaxLength(50)
                .IsOptional();

            Property(e => e.CadastradoPor)
                .HasColumnName("CadastradoPor")
                .HasMaxLength(50)
                .IsOptional();

            Property(e => e.DataCadastro)
                .HasColumnName("DataCadastro");

            Property(e => e.DataAtualizacao)
                .HasColumnName("DataAtualizacao");

            Property(e => e.CadastradoPor)
                .HasColumnName("CadastradoPor")
                .HasMaxLength(50)
                .IsOptional();

            Property(e => e.AtualizadoPor)
               .HasColumnName("AtualizadoPor")
               .HasMaxLength(50)
               .IsOptional();

            Property(e => e.Observacao)
               .HasColumnName("Observacao")
               .HasColumnType("nvarchar(max)")
               .IsOptional();
        }
    }
}
