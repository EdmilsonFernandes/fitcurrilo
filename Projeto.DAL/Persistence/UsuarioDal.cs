using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.DAL.DataSource;
using System.Data.Entity; //entityframework..

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

    }
}
