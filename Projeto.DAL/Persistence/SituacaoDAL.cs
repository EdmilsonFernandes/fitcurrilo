using Projeto.DAL.DataSource;
using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Persistence
{
    public class SituacaoDal : GenericDal<Situacao>
    {
        // Verifica se algum candidato está em uma determinada situacao
        public bool SituacaoEmUso(int idSituacao)
        {
            using (Conexao con = new Conexao())
            {
                var query =
                    from tbCandidato in con.Candidato
                    join tbSituacao in con.Situacao on tbCandidato.Situacao equals tbSituacao.Id.ToString()
                    where tbSituacao.Id == idSituacao
                    select tbCandidato;

                return (query.Count() > 0);
            }
        }

        // Retorna uma lista de objetos Situacao, com todas as situações cadastradas no BD
        public List<Situacao> ListarSituacoes()
        {
            using (Conexao con = new Conexao())
            {
                var listaSituacao = new List<Situacao>();
                var query = from tbSituacao in con.Situacao
                            select tbSituacao;
                foreach (var situacao in query)
                {
                    listaSituacao.Add(situacao);
                }
                return listaSituacao;
            }
        }

        // Verifica se uma situacao já existe no sistema
        public bool SituacaoExiste(string situacao)
        {
            using (Conexao con = new Conexao())
            {
                return con.Situacao
                        .Where(s => s.Descricao == situacao)
                        .Count() > 0;
            }
        }

    }
}
