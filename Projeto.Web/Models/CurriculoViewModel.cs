﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Web.Models
{
    public class CurriculoViewModel
    {
        [Display(Name = "Id:")]
        public int Id { get; set; }

        [Display(Name = "Nome:")]
        public string Nome { get; set; }

        [Display(Name = "Tipo:")]
        public string Tipo { get; set; }

        [Display(Name = "Tamanho:")]
        public int Tamanho { get; set; }

        [Display(Name = "Conteúdo:")]
        public byte[] Conteudo { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Cadastro:")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Cadastrado Por:")]
        public string CadastradoPor { get; set; }

        [Display(Name = "Id do Candidato:")]
        public int IdCandidato { get; set; }
    }
}