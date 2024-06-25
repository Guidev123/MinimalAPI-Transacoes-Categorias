using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Shared.Requests.Categories
{
    public class CreateCategoryCommand : Command
    {
        [Required(ErrorMessage = "Título em formato inválido")]
        [MaxLength(100, ErrorMessage = "O título deve conter no máximo 100 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição em formato inválido")]
        public string Description { get; set; } = string.Empty;
    }
}
