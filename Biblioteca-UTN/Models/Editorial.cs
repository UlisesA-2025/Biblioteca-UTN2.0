using System.ComponentModel.DataAnnotations;

namespace Biblioteca_UTN.Models
{
    public class Editorial
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(150)]
        public string Nombre { get; set; }
    }
}
