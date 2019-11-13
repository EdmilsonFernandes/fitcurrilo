using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Web.Models
{
    public class UsuarioViewModelCadastro
    {
        [RegularExpression("^[A-Za-zÀ-Üà-ü\\s]{6,50}$", 
            ErrorMessage = "Por favor, informe um nome de usuario válido")]
        [Required(ErrorMessage = "Por favor, informe o nome do usuário")]
        [Display(Name = "Nome do Usuário:")] //label..
        public string Nome { get; set; } //campo

        [RegularExpression("^[a-z0-9]{6,20}$", 
            ErrorMessage = "Por favor, informe um login de usuario válido")]
        [Required(ErrorMessage = "Por favor, informe o login do usuário")]
        [Display(Name = "Login de Acesso:")] //label..
        public string Login { get; set; } //campo

        [DataType(DataType.Password)]
        [RegularExpression("^[A-Za-z0-9]{6,20}$",
            ErrorMessage = "Por favor, informe uma senha de usuario válida")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário")]
        [Display(Name = "Senha de Acesso:")] //label..
        public string Senha { get; set; } //campo

        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senhas não conferem")]
        [Required(ErrorMessage = "Por favor, confirme a senha do usuário")]
        [Display(Name = "Confirme sua Senha:")] //label..
        public string SenhaConfirm { get; set; } //campo
    }
}