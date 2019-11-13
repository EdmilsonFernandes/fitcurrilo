using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;

namespace Projeto.DAL.Mappings
{
    public class ContatoMap : EntityTypeConfiguration<Contato>
    {
        public ContatoMap()
        {
            ToTable("Contato");

            HasKey(c => c.Id);

            Property(c => c.Id)
                .HasColumnName("Id");

            Property(c => c.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(80)
                .IsRequired();

            Property(c => c.Email)
               .HasColumnName("Email")
               .HasMaxLength(50)
               .IsRequired();

            Property(c => c.IdEmpresa)
                .HasColumnName("IdEmpresa")
               .IsRequired();

            //Mapear o relacionamento de 1 para Muitos..
            HasRequired(u => u.Empresa) //Contato TEM 1 Empresa
                .WithMany(e => e.Contatos) //Empresa TEM Muitos Contatos
                .HasForeignKey(u => u.IdEmpresa); //Chave estrangeira..


        }
    }
}
