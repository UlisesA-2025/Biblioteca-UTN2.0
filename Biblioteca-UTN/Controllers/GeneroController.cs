using Biblioteca_UTN.Dato;
using Biblioteca_UTN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Biblioteca_UTN.Controllers
{
    public class GeneroController : Controller
    {
        private readonly BibliotecaDato _BD;

        public GeneroController()
        {
            _BD = new BibliotecaDato();
        }

        public IActionResult Index()
        {
            var listaGeneros = _BD.ListarGenero(0);
            return View(listaGeneros);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genero genero)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _BD.CrearGenero(genero);

                    TempData["Mensaje"] = "Género agregado exitosamente.";

                    return RedirectToAction("Index");
                }

               return View(genero);
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            return View(_BD.ListarGenero(id).FirstOrDefault());
        }

        public IActionResult Edit(int id)
        {
            var genero = _BD.ListarGenero(id).FirstOrDefault();
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genero genero)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(genero);
                }
                
                var error = _BD.EditarGenero(genero);
                if (!string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View(genero);
                }

                TempData["Mensaje"] = "Género editado exitosamente.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            var genero = _BD.ListarGenero(id).FirstOrDefault();
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var genero = _BD.ListarGenero(id).FirstOrDefault();
                if (genero == null)
                {
                    return NotFound();
                }

                var error = _BD.EliminarGenero(id);
                if (!string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View(genero);
                }

                TempData["Mensaje"] = "Género eliminado exitosamente.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
