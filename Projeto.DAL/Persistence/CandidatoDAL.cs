using Projeto.DAL.DataSource;
using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Persistence
{
    public class CandidatoDal : GenericDal<Candidato>
    {
        // Verifica se um candidato já existe no sistema
        public bool CandidatoExiste(string nomeCandidato)
        {
            using (Conexao con = new Conexao())
            {
                return con.Candidato
                        .Where(c => c.Nome == nomeCandidato)
                        .Count() > 0;
            }
        }
    }
}
