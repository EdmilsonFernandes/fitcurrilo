using System;

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
