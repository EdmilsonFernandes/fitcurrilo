using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; //entityframework..
using Projeto.Entities;
using Projeto.DAL.DataSource;

namespace Projeto.DAL.Persistence
{
    public class ProdutoDal:GenericDal<Produto>
    {
        //método para verificar se um produto ja esta cadastrado..
        public bool HasLogin(string nome)
        {
            using (Conexao con = new Conexao())
            {
                //SQL -> select count(*) from Produto where Nome = ?
                return con.Produtos
                        .Where(u => u.Nome == nome)
                        .Count() > 0;
            }
        }
    }
}
