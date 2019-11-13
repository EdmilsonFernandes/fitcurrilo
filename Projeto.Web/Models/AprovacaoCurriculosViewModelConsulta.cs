using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Web.Models
{
    public class AprovacaoCurriculosViewModelConsulta
    {
        public int Id { get; set; }
        public string NumeroOFProducao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataAnalize { get; set; }
        public string NumeroCertificado { get; set; }
        public string NumeroOFLaboratorio { get; set; }
        public string NomeProduto { get; set; }
        public string QuantidadeKG { get; set; }
        public String Situacao { get; set; }
        public string AtualizadoPor { get; set; }
        public string Observacao { get; set; }
    }
}