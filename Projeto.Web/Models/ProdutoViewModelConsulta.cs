using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Web.Models
{
    public class ProdutoViewModelConsulta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime DataCadastro { get; set; }
        public string EditadoPor { get; set; }
        public DateTime DataEdicao { get; set; }
        public string Descricao { get; set; }
    }
}