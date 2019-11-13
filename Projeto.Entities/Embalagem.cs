using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Embalagem
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Capacidade { get; set; }
        public string UnidadeMedida { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime DataCadastro { get; set; }
        public string EditadoPor { get; set; }
        public DateTime DataEdicao { get; set; }
        public string Descricao { get; set; }

    }
}
