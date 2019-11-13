using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class AprovacaoCurriculos
    {
        public int Id { get; set; }
        public string NomeCandidato { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public DateTime DataDeVisto { get; set; }
        public string NumeroCertificado { get; set; }
        public string NumeroOFLaboratorio { get; set; }
        public string NomeProduto { get; set; }
        public string QuantidadeKG { get; set; }
        public String Situacao { get; set; }
        public string AtualizadoPor { get; set; }
        public string Observacao { get; set; }

        public bool StatusVisualizacao { get; set; }

        public string FileName  { get; set; }
        public long Size { get; set; }
        public int Count { get; set; }
    }
}
