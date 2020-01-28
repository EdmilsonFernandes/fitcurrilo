using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Projeto.Web.Models
{
    public class UsuarioAlterarSenha
    {
      [Display(Name = "Id:")] //label
      public int Id { get; set; }

     public string Login { get; set; } //campo
                                          //
                                          //[RegularExpression("^[A-Za-zÀ-Üà-ü\\s]{6,50}$", 
                                          //    ErrorMessage = "Por favor, informe um nome de usuario válido")]
                                          //[Required(ErrorMessage = "Por favor, informe o nome do usuário")]
                                          //[Display(Name = "Nome do Usuário:")] //label..
                                          //public string Nome { get; set; } //campo
                                          //
                                          //[RegularExpression("^[a-z0-9]{6,20}$", 
                                          //    ErrorMessage = "Por favor, informe um login de usuario válido")]
                                          //[Required(ErrorMessage = "Por favor, informe o login do usuário")]
                                          //[Display(Name = "Login de Acesso:")] //label..
                                          //public string Login { get; set; } //campo
                                          //
                                          //
                                          //public string Perfil { get; set; } //campo
                                          //public IEnumerable<SelectListItem> Perfis { get; set; }
                                          //
                                          //[DataType(DataType.Password)]
                                          //[RegularExpression("^[A-Za-z0-9]{6,20}$",
                                          //    ErrorMessage = "Por favor, informe uma senha de usuario válida")]
                                          //[Required(ErrorMessage = "Por favor, informe a senha do usuário")]
                                          //[Display(Name = "Senha de Acesso:")] //label..
                                          //public string Senha { get; set; } //campo
                                          //
                                          //[DataType(DataType.Password)]
                                          //[System.ComponentModel.DataAnnotations.Compare("Senha", ErrorMessage = "Senhas não conferem")]
                                          //[Required(ErrorMessage = "Por favor, confirme a senha do usuário")]
                                          //[Display(Name = "Confirme a Senha:")] //label..
                                          //public string ConfirmaSenha { get; set; } //campo

        [DataType(DataType.Password)]
        [RegularExpression("^[A-Za-z0-9]{6,20}$",
        ErrorMessage = "Por favor, informe uma senha de usuario válida")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário")]
        [Display(Name = "Senha Antiga:")] //label..
        public string SenhaAntiga { get; set; } //campo

        [DataType(DataType.Password)]
        [RegularExpression("^[A-Za-z0-9]{6,20}$",
        ErrorMessage = "Por favor, informe uma senha de usuario válida")]
        [Required(ErrorMessage = "Por favor, informe a nova senha do usuário")]
        [Display(Name = "Senha Nova:")] //label..
        public string SenhaNova { get; set; } //campo

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("SenhaNova", ErrorMessage = "Senhas não conferem")]
        [Required(ErrorMessage = "Por favor, confirme a senha do usuário")]
        [Display(Name = "Confirme a Senha:")] //label..
        public string ConfirmaSenhaNova { get; set; } //campo
    }
}