using Biblioteca_UTN.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_UTN.Dato
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
    }
}
