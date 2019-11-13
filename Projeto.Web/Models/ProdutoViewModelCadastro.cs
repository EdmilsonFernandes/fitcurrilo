using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Web.Models
{
    public class ProdutoViewModelCadastro
    {
        [RegularExpression("^[A-Za-zÀ-Üà-ü\\s]{6,50}$",
            ErrorMessage = "Por favor, informe um nome válido")]
        [Required(ErrorMessage = "Por favor, informe o nome do produto")]
        [Display(Name = "Nome do produto:")] //label..
        public string Nome { get; set; } //campo

        [Display(Name = "Cadastrado Por:")] //label..
        public string CadastradoPor { get; set; }

        [Display(Name = "Data do Cadastro:")] //label.
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Descrição do produto:")] //label.
        public string Descricao { get; set; }


    }
}