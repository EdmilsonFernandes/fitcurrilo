using Projeto.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Projeto.DAL.Mappings
{
    public class RecrutadorMap : EntityTypeConfiguration<Recrutador>
    {
        public RecrutadorMap()
        {
            ToTable("Recrutador");

            HasKey(r => r.Id);

            Property(r => r.Id)
                .HasColumnName("Id");

            Property(r => r.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(70)
                .IsRequired();

            Property(r => r.Email)
            .HasColumnName("Email")
            .HasMaxLength(70);

        }
    }
}
