using System.ComponentModel.DataAnnotations;

namespace Projeto.Web.Models
{
    public class RecrutadorViewModel
    {
        [Display(Name = "Id:")] //label
        public int Id { get; set; }

        [RegularExpression("^[A-Za-zÀ-Üà-ü\\s]{6,50}$",
            ErrorMessage = "Por favor, informe um nome de usuario válido")]
        [Required(ErrorMessage = "Por favor, informe o nome do recrutador")]
        [Display(Name = "Nome:")] //label..
        public string Nome { get; set; } //campo

        //[RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$",
        //    ErrorMessage = "Por favor, informe um e-mail válido")]
        [Display(Name = "E-mail:")] //label..
        public string Email { get; set; } 

    }
}