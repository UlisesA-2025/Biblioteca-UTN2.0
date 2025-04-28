using Biblioteca_UTN.Dato;
using Biblioteca_UTN.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_UTN.Controllers
{
    public class UbicacionController : Controller
    {
        private readonly BibliotecaDato _BD;

        public UbicacionController()
        {
            _BD = new BibliotecaDato();
        }

        public IActionResult Index()
        {
            var listarUbi = _BD.ListarUbicacion(0);
            return View(listarUbi);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Ubicacion ubicacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _BD.CrearUbicacion(ubicacion);

                    TempData["Mensaje"] = "Ubicación agregado correctamente";

                    return RedirectToAction("Index");
                }

                return View(ubicacion);
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            return View(_BD.ListarUbicacion(id).FirstOrDefault());
        }

        public IActionResult Edit(int id)
        {
            var ubicacion = _BD.ListarUbicacion(id).FirstOrDefault();
            if (ubicacion == null)
            {
                return NotFound();
            }
            return View(ubicacion);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ubicacion ubicacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(ubicacion);
                }

                var error = _BD.EditarUbicacion(ubicacion);
                if (!string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                TempData["Mensaje"] = "Ubicación editado exitosamente.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            var ubicacion = _BD.ListarUbicacion(id).FirstOrDefault();
            if (ubicacion == null)
            {
                return NotFound();
            }
            return View(ubicacion);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {

                var ubicacion = _BD.ListarUbicacion(id).FirstOrDefault();
                if (ubicacion == null)
                {
                    return NotFound();
                }

                var error = _BD.EliminarUbicacion(id);
                if (!string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View(ubicacion);
                }

                TempData["Mensaje"] = "Ubicación eliminado exitosamente.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
