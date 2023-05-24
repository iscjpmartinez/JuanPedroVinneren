using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestVinneren.Negocio.DTOs.Producto
{
    public class ProductoDTO
    {
        [Key]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NombreProducto { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NumMaterial { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CantidadUnidadades { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdCategoria { get; set; }

    }
}
