using Projeto.DAL.DataSource;
using Projeto.Entities;
using System.Linq;

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

        public bool NomeParecido(string nomeCandidato)
        {
            using (Conexao con = new Conexao())
            {
                return con.Candidato
                        .Where(c => c.Nome.Contains(nomeCandidato.Substring(0,8)))
                        .Count() > 0;
            }
        }
    }
}
