using Biblioteca_UTN.Dato;
using Biblioteca_UTN.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_UTN.Controllers
{
    public class EditorialController : Controller
    {
        private readonly BibliotecaDato _BD;

        public EditorialController()
        {
            _BD = new BibliotecaDato();
        }

        public IActionResult Index()
        {
            var listaEditoriales = _BD.ListarEditorial(0);
            return View(listaEditoriales);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Editorial editorial)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _BD.CrearEditorial(editorial);

                    TempData["Mensaje"] = "Editorial agregado correctamente";

                    return RedirectToAction("Index");
                }

                return View(editorial);
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            return View(_BD.ListarEditorial(id).FirstOrDefault());
        }

        public IActionResult Edit(int id)
        {
            var editorial = _BD.ListarEditorial(id).FirstOrDefault();
            if (editorial == null)
            {
                return NotFound();  
            }
            return View(editorial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Editorial editorial)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(editorial);
                }

                var error = _BD.EditarEditorial(editorial);
                if (!string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                TempData["Mensaje"] = "Editorial editado exitosamente.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            var editorial = _BD.ListarEditorial(id).FirstOrDefault();
            if (editorial == null)
            {
                return NotFound();
            }
            return View(editorial);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {

                var editorial = _BD.ListarEditorial(id).FirstOrDefault();
                if (editorial == null)
                {
                    return NotFound();
                }

                var error = _BD.EliminarEditorial(id);
                if (!string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View(editorial);
                }

                TempData["Mensaje"] = "Editorial eliminado exitosamente.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
