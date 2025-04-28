using Biblioteca_UTN.Dato;
using Biblioteca_UTN.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_UTN.Controllers
{
    public class LibroController : Controller
    {

        private readonly BibliotecaDato _BD;

        public LibroController()
        {
            _BD = new BibliotecaDato();
        }

        public IActionResult Index()
        {
            var listaLibros = _BD.ListarLibros(0);
            return View(listaLibros);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Libro libro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _BD.CrearLibro(libro);

                    TempData["Mensaje"] = "Libro agregado correctamente";

                    return RedirectToAction("Index");
                }

                return View(libro);
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            return View(_BD.ListarLibros(id).FirstOrDefault());
        }
        public IActionResult Edit(int id)
        {
            var libro = _BD.ListarLibros(id).FirstOrDefault();
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Libro libro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(libro);
                }

                var error = _BD.EditarLibro(libro);
                if (!string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                TempData["Mensaje"] = "Libro editado exitosamente.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            var libro = _BD.ListarLibros(id).FirstOrDefault();
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteLibroConfirmed(int id)
        {
            try
            {

                var libro = _BD.ListarLibros(id).FirstOrDefault();
                if (libro == null)
                {
                    return NotFound();
                }

                var error = _BD.EliminarLibro(id);
                if (!string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View(libro);
                }

                TempData["Mensaje"] = "Libro eliminado exitosamente.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
