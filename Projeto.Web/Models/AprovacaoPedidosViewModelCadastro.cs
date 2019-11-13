using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Web.Models
{
    public class AprovacaoPedidosViewModelCadastro
    {

        [MinLength(3, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o numero da of de produção.")]
        [Display(Name = "Número da OF :")] //label..
        public string NumeroOFProducao { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Informe a data de fabricação do produto.")]
        [Display(Name = "Data de Fabricação:")] //label..
        public DateTime DataFabricacao { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Informe a data de fabricação do produto.")]
        [Display(Name = "Data de Analise:")] //label..
        public DateTime DataAnalise { get; set; }

        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o numero do certificado.")]
        [Display(Name = "Número do Certificado :")] //label..
        public string NumeroCertificado { get; set; }

        [MinLength(3, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o numero da of de laboratório.")]
        [Display(Name = "Número OF Laboratório:")] //label..
        public string NumeroOFLaboratorio { get; set; }

        [MinLength(3, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o nome do produto.")]
        [Display(Name = "Nome do Produto:")] //label..
        public string NomeProduto { get; set; }

        [RegularExpression("^[0-9]$",ErrorMessage = "Informar somente números.")]
        [Required(ErrorMessage = "Informe a quantidade de produtos em kilos.")]
        [Display(Name = "Informe a quantidade de produtos em Kilos:")] //label..
        public string QuantidadeKG { get; set; }

        [Required(ErrorMessage = "Informe o numero da of de laboratório.")]
        [Display(Name = "Situação da Analise:")] //label..
        public String Situacao { get; set; }

        public string AtualizadoPor { get; set; }

        [Display(Name = "Observação:")] //label..
        public string Observacao { get; set; }
    }
}