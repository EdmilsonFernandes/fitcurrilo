using Projeto.DAL.DataSource;
using Projeto.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DAL.Persistence
{
    public class VagaDal : GenericDal<Vaga>
    {

        // Retorna uma lista de objetos Vaga, com todas as vagas cadastradas no BD
        public List<Vaga> ListarVagas()
        {
            using (Conexao con = new Conexao())
            {
                var listaVagas = new List<Vaga>();
                var query = from tbVaga in con.Vaga
                            select tbVaga;
                foreach (var vagas in query)
                {
                    listaVagas.Add(vagas);
                }
                return listaVagas;
            }
        }
    }
}
