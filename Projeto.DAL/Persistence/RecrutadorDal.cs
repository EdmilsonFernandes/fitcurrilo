using Projeto.DAL.DataSource;
using Projeto.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DAL.Persistence
{
    public class RecrutadorDal : GenericDal<Recrutador>
    {
        // Verifica se algum candidato está em uma determinada situacao
    //  public bool SituacaoEmUso(int idSituacao)
    //  {
    //      using (Conexao con = new Conexao())
    //      {
    //          var query =
    //              from tbCandidato in con.Candidato
    //              join tbSituacao in con.Situacao on tbCandidato.Situacao equals tbSituacao.Id.ToString()
    //              where tbSituacao.Id == idSituacao
    //              select tbCandidato;
    //
    //          return (query.Count() > 0);
    //      }
    //  }

        // Retorna uma lista de objetos Situacao, com todas as situações cadastradas no BD
        public List<Recrutador> ListarRecrutadores()
        {
            using (Conexao con = new Conexao())
            {
                var listaRecrutador = new List<Recrutador>();
                var query = from tbRecrutador in con.Recrutador
                            select tbRecrutador;
                foreach (var recrutador in query)
                {
                    listaRecrutador.Add(recrutador);
                }
                return listaRecrutador;
            }
        }

        // Verifica se um recrutador já existe no sistema
        public bool RecrutadorExiste(string nome)
        {
            using (Conexao con = new Conexao())
            {
                return con.Recrutador
                       .Where(r => r.Nome == nome)
                       .Count() > 0;
            }
        }

        // Verifica se um e-mail já está em uso por outro Recrutador
        public bool EmailEmUso(string email)
        {
            using (Conexao con = new Conexao())
            {
                return con.Recrutador
                       .Where(r => r.Email == email)
                       .Count() > 0;
            }
        }

    }
}
