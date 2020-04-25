using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class ProcessoSelecao
    {
        public int Id { get; set; }
        public string Etapa { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string CriadoPor { get; set; }
        public string AtualizadoPor { get; set; }
        public string Observacao { get; set; }
    }
}
