using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Mappings
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuario");

            HasKey(u => u.IdUsuario);

            Property(u => u.IdUsuario)
                .HasColumnName("IdUsuario");

            Property(u => u.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(50)
                .IsRequired();

            Property(u => u.Login)
                .HasColumnName("Login")
                .HasMaxLength(25)
                .IsRequired();

            Property(u => u.Senha)
                .HasColumnName("Senha")
                .HasMaxLength(50)
                .IsRequired();

            Property(u => u.IdPerfil)
                .HasColumnName("IdPerfil")
                .IsRequired();

            //Mapear o relacionamento de 1 para Muitos..
            HasRequired(u => u.Perfil) //Usuario TEM 1 Perfil
                .WithMany(p => p.Usuarios) //Perfil TEM Muitos Usuario
                .HasForeignKey(u => u.IdPerfil); //Chave estrangeira..
        }
    }
}
