using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Vaga
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public decimal Salario { get; set; }
        public int TipoContratacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string CadastradoPor { get; set; }
        public string AtualizadoPor { get; set; }
    }
}
