using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Estoque
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public Embalagem Embalagem { get; set; }
        public int Total { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataEntrada { get; set; }
        public string EditadoPor { get; set; }
        public DateTime DataEdicao { get; set; }
        public DateTime DataSaida { get; set; }

        public Estoque()
        {
            Total = Embalagem.Capacidade * Quantidade;
            DataCadastro = DataEntrada;
        }
    }
}
