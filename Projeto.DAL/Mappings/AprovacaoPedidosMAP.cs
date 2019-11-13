using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;

namespace Projeto.DAL.Mappings
{
    public class AprovacaoCurriculosMAP:EntityTypeConfiguration<AprovacaoCurriculos>
    {
        public AprovacaoCurriculosMAP()
        {
            ToTable("AprovacaoCurriculos");
            HasKey(a => a.Id);

            Property(a => a.Id)
                .HasColumnName("Id");

            Property(a => a.NomeCandidato)
                .HasColumnName("NumeroOfProducao")
                .HasMaxLength(50)
                .IsRequired();

            Property(a => a.DataDeCadastro)
                .HasColumnName("DataFabricacao")
                .IsRequired();

            Property(a => a.DataDeVisto)
                .HasColumnName("DataAnalise")
                .IsRequired();

            Property(a => a.NumeroCertificado)
                .HasColumnName("NumeroCertificado")
                .HasMaxLength(50)
                .IsRequired();

            Property(a => a.NumeroOFLaboratorio)
                .HasColumnName("NumeroOFLaboratorio")
                .HasMaxLength(50)
                .IsRequired();

            Property(a => a.NomeProduto)
                .HasColumnName("NomeProduto")
                .HasMaxLength(50)
                .IsRequired();

            Property(a => a.QuantidadeKG)
                .HasColumnName("QuantidadeKG")
                .HasMaxLength(10)
                .IsRequired();

            Property(a => a.Situacao)
                .HasColumnName("Situacao")
                .HasMaxLength(50);

            Property(a => a.AtualizadoPor)
                .HasColumnName("AtualizadoPor")
                .HasMaxLength(50)
                .IsRequired();

            Property(a => a.Observacao)
                .HasColumnName("Observacao")
                .HasMaxLength(100);

            Property(a => a.NomeCandidato)
                .HasColumnName("NomeCandidato")
                .HasMaxLength(500);

            Property(a => a.StatusVisualizacao)
                .HasColumnName("Visto");

            Property(a => a.FileName)
                .HasColumnName("PathCurriculoCandidato")
                .HasMaxLength(900);
        }
    }
}