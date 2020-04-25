using Projeto.DAL.DataSource;
using Projeto.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DAL.Persistence
{
    public class TecnologiaDal : GenericDal<Tecnologia>
    {

        // Retorna uma lista de objetos Tecnologia, com todas as tecnologias cadastradas no BD
        public List<Tecnologia> ListarTecnologias()
        {
            using (Conexao con = new Conexao())
            {
                var listaTecnologia = new List<Tecnologia>();
                var query = from tbTecnologia in con.Tecnologia
                            select tbTecnologia;
                foreach (var tecnologia in query)
                {
                    listaTecnologia.Add(tecnologia);
                }
                return listaTecnologia;
            }
        }
        // Verifica se uma tecnologia já existe no banco de dados
        public bool TecnologiaExiste(string nomeTecnologia)
        {
            using (Conexao con = new Conexao())
            {
                return con.Tecnologia
                        .Where(t => t.Nome.Equals(nomeTecnologia))
                        .Count() > 0;
            }
        }

        //public bool IsTecnologiaEmUso(int idSituacao)
        //{
        //    using (Conexao con = new Conexao())
        //    {
        //        var query =
        //            from tbCandidato in con.Candidato
        //            join tbTecnologia in con.Tecnologia on tbCandidato.Tecnologia equals tbSituacao.Id.ToString()
        //            where tbSituacao.Id == idSituacao
        //            select tbCandidato;

        //        return (query.Count() > 0);
        //    }
        //}

    }
}
