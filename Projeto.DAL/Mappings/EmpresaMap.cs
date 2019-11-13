using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;


namespace Projeto.DAL.Mappings
{
    public class EmpresaMap:EntityTypeConfiguration<Empresa>
    {
        public EmpresaMap()
        {
            ToTable("Empresa");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("Id");

            Property(e => e.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(80)
                .IsRequired();

            Property(e => e.CNPJ)
               .HasColumnName("CNPJ")
               .HasMaxLength(14)
               .IsRequired();
        }
    }
}
