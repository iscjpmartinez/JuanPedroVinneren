using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestVinneren.Negocio.DTOs.Categoria
{
    public class CategoriaDTO
    {
        [Key]
        public int? IdCategoria { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NombreCategoria { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? IdCategoriaPadre { get; set; }
    }
}
