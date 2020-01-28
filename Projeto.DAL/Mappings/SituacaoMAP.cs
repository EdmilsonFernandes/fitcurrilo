using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Mappings
{
    public class SituacaoMap : EntityTypeConfiguration<Situacao>
    {
        public SituacaoMap()
        {
            ToTable("Situacao");

            HasKey(s => s.Id);

            Property(s => s.Id)
                .HasColumnName("Id");

            Property(s=> s.Descricao)
                .HasColumnName("Descricao")
                .HasMaxLength(70)
                .IsRequired();
        }
    }
}
