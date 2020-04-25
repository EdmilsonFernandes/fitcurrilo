using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Projeto.Web.Models
{
    public class CandidatoViewModel
    {
        [Display(Name = "Id:")] //label
        public int Id { get; set; }

        [RegularExpression("^[A-Za-zÀ-Üà-ü\\s]{6,50}$",
           ErrorMessage = "Por favor, informe um nome válido")]
        [Required(ErrorMessage = "Por favor, informe o nome do candidato")]
        [Display(Name = "Nome do Candidato:")] //label
        public string Nome { get; set; } //campo

        [Display(Name = "Grau de Instrução:")] //label
        public string GrauInstrucao { get; set; } //campo

        [Required(ErrorMessage = "..")]
        [Display(Name = "Qtde de Certificados:")] //label
        public int QtdeCertificados { get; set; } //campo

        [Display(Name = "Situação:")] //label
        public string Situacao { get; set; } //campo

        [Display(Name = "Recrutador:")] //label
        public string Recrutador { get; set; } //campo

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Cadastro:")] //label
        public DateTime DataCadastro { get; set; }//campo

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Atualizacao:")] //label
        public DateTime DataAtualizacao { get; set; }//campo

        [Display(Name = "Cadastrado Por:")] //label
        public string CadastradoPor { get; set; } //campo

        [Display(Name = "Atualizado Por:")] //label
        public string AtualizadoPor { get; set; } //campo

        [Display(Name = "Observação:")] //label
        public string Observacao { get; set; } //campo

        [Display(Name = "Currículo:")] //label

        public IEnumerable<SelectListItem> Situacoes { get; set; }

        public IEnumerable<SelectListItem> Recrutadores { get; set; }

        public List<Curriculo> Curriculos { get; set; }
    }
}
