using System.ComponentModel.DataAnnotations;

namespace Biblioteca_UTN.Models
{
    public class Libro
    {      
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(150)]
        public string Nombre { get; set; }
        public int IdAutor {  get; set; }
        public int IdEditorial {  get; set; }
        public int IdGenero {  get; set; }
        public int IdUbicacion {  get; set; }
        public int CantCopias {  get; set; }


        public Autor? Autor { get; set; }
        public Editorial? Editorial { get; set; }
        public Genero? Genero { get; set; }
        public Ubicacion? Ubicacion { get; set; }
    }
}
