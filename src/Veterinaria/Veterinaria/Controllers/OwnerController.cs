using Microsoft.AspNetCore.Mvc;
using Veterinaria.Models;
using Veterinaria.Services.Implementations;

namespace Veterinaria.Controllers
{
    public class OwnerController : Controller
    {
        [HttpGet]
        public IActionResult ShowOwners()
        {
            var ownerList = UnitOfWork.Owner.GetAll();
            return View(ownerList);
        }

        [HttpGet]
        public IActionResult SaveOwner()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveOwner(OwnerModel model)
        {
            if (!ModelState.IsValid) return View();
            var addResult = UnitOfWork.Owner.Add(model);
            if (!string.IsNullOrEmpty(addResult.ProNombre)) return RedirectToAction("ShowOwners");
            else return View();
        }

        [HttpGet]
        public IActionResult UpdateOwner(string idUpdateOwner)
        {
            var owner = UnitOfWork.Owner.GetById(idUpdateOwner);
            return View(owner);
        }

        [HttpPost]
        public IActionResult UpdateOwner(OwnerModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                var updateResult = UnitOfWork.Owner.Update(model);
                if (!string.IsNullOrEmpty(updateResult.ProNombre)) return RedirectToAction("ShowOwners");
                else return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "No se pudo actualizar el propietario. Inténtelo nuevamente.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult DeleteOwner(string idDeleteOwner)
        {
            var owner = UnitOfWork.Owner.GetById(idDeleteOwner);
            return View(owner);
        }

        [HttpPost]
        public IActionResult DeleteOwner(OwnerModel model)
        {
            if (model.ProDocumento == null)
                throw new Exception("El documento del propietario no puede ser nulo.");

            var deleteResult = UnitOfWork.Owner.Delete(model.ProDocumento);
            if (deleteResult != null && deleteResult.ProDocumento == model.ProDocumento)
                return RedirectToAction("ShowOwners");
            else return View();
        }
    }
}
