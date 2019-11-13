using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using Projeto.DAL.DataSource;

namespace Projeto.DAL.Persistence
{
    public class EmpresaDal:GenericDal<Empresa>
    {
        public List<Empresa> FindAllByName(string nome)
        {
            using (Conexao Con = new Conexao())
            {
                return Con.Empresa
                    .Where(e => e.Nome.Contains(nome))
                    .OrderBy(e => e.Nome)
                    .ToList();
            }
            
        }
    }
}