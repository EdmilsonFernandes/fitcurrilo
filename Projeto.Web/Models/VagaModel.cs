using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Projeto.Web.Models
{
    public class VagaModel
    {
        [Display(Name = "Id:")]
        public int Id { get; set; }

        [RegularExpression("^[A-Za-zÀ-Üà-ü\\s]{6,50}$",
           ErrorMessage = "Por favor, informe uma descrição válida")]
        [Required(ErrorMessage = "Por favor, informe a descrição da vaga")]
        [Display(Name = "Descrição da Vaga:")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informe a situação da vaga")]
        [Display(Name = "Situação:")]
        public string Situacao { get; set; }

        [Required(ErrorMessage = "Por favor, informe salário da vaga")]
        [Display(Name = "Salário R$:")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Salario { get; set; }

        [Display(Name = "Tipo de Contratação:")]
        public int TipoContratacao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Cadastro:")]
        public DateTime DataCadastro { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Atualizacao:")]
        public DateTime DataAtualizacao { get; set; }

        [Display(Name = "Cadastrado Por:")]
        public string CadastradoPor { get; set; }

        [Display(Name = "Atualizado Por:")]
        public string AtualizadoPor { get; set; }
    }
}
