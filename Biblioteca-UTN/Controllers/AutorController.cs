

using Biblioteca_UTN.Dato;
using Biblioteca_UTN.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_UTN.Controllers
{
    public class AutorController : Controller
    {
        private readonly BibliotecaDato _BD;

        public AutorController()
        {
            _BD = new BibliotecaDato();
        }

        public IActionResult Index()
        {
            var listaAutores = _BD.ListarAutores(0);
            return View(listaAutores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Autor autor)
        {
            try
            {
                if (autor.Nombre == null)
                {
                    ModelState.AddModelError("Nombre", "El nombre del autor es obligatorio.");
                    return View(autor);
                }
                if (autor.FechaNacimiento > DateTime.Today)
                {
                    ModelState.AddModelError("FechaNacimiento", "La fecha de nacimiento no puede ser posterior a hoy.");
                    return View(autor);
                }

                if (ModelState.IsValid)
                {
                    _BD.CrearAutor(autor);

                    TempData["Mensaje"] = "Autor agregado exitosamente.";

                    return RedirectToAction("Index");
                }

                return View(autor);
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            return View(_BD.ListarAutores(id).FirstOrDefault());
        }

        public IActionResult Edit(int id)
        {
            var autor = _BD.ListarAutores(id).FirstOrDefault();
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Autor autor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(autor);
                }

                var error = _BD.EditarAutor(autor);

                if (error != null)
                {
                    ViewBag.Error = error;
                    return View(autor);
                }
                else
                {
                    TempData["Mensaje"] = "Autor editado exitosamente.";
                    return RedirectToAction("Index");
                }              
            }
            catch
            {
                return View(autor);
            }
        }

        public IActionResult Delete(int id)
        {
            var autor = _BD.ListarAutores(id).FirstOrDefault();
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var error = _BD.EliminarAutor(id);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewBag.Error = error;
                    return View();
                }
                else
                {
                    TempData["Mensaje"] = "Autor eliminado exitosamente.";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
