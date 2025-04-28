using System.ComponentModel.DataAnnotations;

namespace Biblioteca_UTN.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La Ubicación del libbro es obligatoria")]
        [StringLength(150)]
        public string Nombre { get; set; }
    }
}
