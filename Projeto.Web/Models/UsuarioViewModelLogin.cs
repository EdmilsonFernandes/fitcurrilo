using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Web.Models
{
    public class UsuarioViewModelLogin
    {
        [RegularExpression("^[a-z0-9]{6,20}$",
            ErrorMessage = "Por favor, informe um login de usuario válido")]
        [Required(ErrorMessage = "Por favor, informe o login do usuário")]
        [Display(Name = "Informe seu Login:")] //label..
        public string Login { get; set; } //campo

        [DataType(DataType.Password)]
        [RegularExpression("^[A-Za-z0-9]{6,20}$",
            ErrorMessage = "Por favor, informe uma senha de usuario válida")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário")]
        [Display(Name = "Informe sua Senha:")] //label..
        public string Senha { get; set; } //campo
    }
}