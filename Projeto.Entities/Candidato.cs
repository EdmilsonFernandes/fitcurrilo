using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string GrauInstrucao { get; set; }
        public int QtdeCertificados { get; set; }
        public string Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string CadastradoPor { get; set; }
        public string AtualizadoPor { get; set; }
        public string Observacao { get; set; }
        public virtual ICollection<Curriculo> Curriculos { get; set; }
    }
}
