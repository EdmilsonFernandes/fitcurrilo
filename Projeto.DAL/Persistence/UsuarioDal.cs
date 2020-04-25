using Projeto.DAL.DataSource;
using Projeto.Entities;
using System.Collections.Generic;
using System.Data.Entity; //entityframework..
using System.Linq;


namespace Projeto.DAL.Persistence
{
    public class UsuarioDal : GenericDal<Usuario>
    {
        //método para verificar se um login ja esta cadastrado..
        public bool HasLogin(string login)
        {
            using (Conexao con = new Conexao())
            {
                //SQL -> select count(*) from Usuario where Login = ?
                return con.Usuario
                        .Where(u => u.Login == login)
                        .Count() > 0;
            }
        }


        //método para retornar um Usuario pelo login e senha..
        public Usuario FindByLoginSenha(string login, string senha)
        {
            using (Conexao con = new Conexao())
            {
                //SQL -> select * from Usuario u
                //       inner join Perfil p
                //       on p.IdPerfil = u.IdPerfil
                //       where u.Login = ? and u.Senha = ?
                return con.Usuario
                        .Include(u => u.Perfil) //INNER JOIN..
                        .Where(u => u.Login.Equals(login)
                                 && u.Senha.Equals(senha))
                        .FirstOrDefault();
            }
        }


        public List<Usuario> ListarUsuarios()
        {
            Conexao con = new Conexao();

            var listaUsuario = new List<Usuario>();

            var query = from tbUsuario in con.Usuario
                        join tbPerfil in con.Perfil on tbUsuario.IdPerfil equals tbPerfil.IdPerfil
                        select new
                        {
                            tbUsuario.IdUsuario,
                            tbUsuario.Nome,
                            tbUsuario.Login,
                            tbUsuario.IdPerfil,
                            tbUsuario.Perfil,
                        };

            foreach (var result in query)
            {
                var usuario = new Usuario();

                usuario.IdUsuario = result.IdUsuario;
                usuario.Nome = result.Nome;
                usuario.Login = result.Login;
                usuario.Perfil = result.Perfil;
                usuario.IdPerfil = result.IdPerfil;
                listaUsuario.Add(usuario);
            }
            return listaUsuario;
        }
    }
}
