using Projeto.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Projeto.DAL.Mappings
{
    public class TecnologiaMap : EntityTypeConfiguration<Tecnologia>
    {
        public TecnologiaMap()
        {
            ToTable("Tecnologia");

            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasColumnName("Id");

            Property(t => t.Nome)
                .HasColumnName("Nome")
                .IsRequired();
        }
    }
}
