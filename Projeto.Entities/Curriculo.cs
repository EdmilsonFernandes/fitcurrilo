using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Curriculo
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int Tamanho { get; set; }
        public byte[] Conteudo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string CadastradoPor { get; set; }
        public int IdCandidato { get; set; }

        public virtual Candidato Candidato { get; set; }
    }
}
