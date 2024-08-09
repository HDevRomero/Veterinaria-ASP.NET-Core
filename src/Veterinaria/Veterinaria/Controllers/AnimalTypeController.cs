using Veterinaria.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class AnimalTypeController : Controller
    {
        [HttpGet]
        public IActionResult ShowAnimalsTypes()
        {
            var AnimalTypeList = UnitOfWork.AnimalType.GetAll();
            return View(AnimalTypeList);
        }

        [HttpGet]
        public IActionResult SaveAnimalType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveAnimalType(AnimalTypeModel model)
        {
            if(!ModelState.IsValid) return View();
            var addResult = UnitOfWork.AnimalType.Add(model);           
            if (!string.IsNullOrEmpty(addResult.TipNombre)) return RedirectToAction("ShowAnimalsTypes");
            else return View();
        }

        [HttpGet]
        public IActionResult UpdateAnimalType(int idUpdateType)
        {
            var AnimalType = UnitOfWork.AnimalType.GetById(idUpdateType);
            return View(AnimalType);
        }

        [HttpPost]
        public IActionResult UpdateAnimalType(AnimalTypeModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                var updateResult = UnitOfWork.AnimalType.Update(model);               
                if (!string.IsNullOrEmpty(updateResult.TipNombre)) return RedirectToAction("ShowAnimalsTypes");
                else return View(model);
            }
            catch(Exception)
            {
                ModelState.AddModelError("", "No se pudo actualizar el tipo de animal. Inténtelo nuevamente.");
                return View(model);
            }
            
        }

        [HttpGet]
        public IActionResult DeleteAnimalType(int idDeleteType)
        {
            var animalType = UnitOfWork.AnimalType.GetById(idDeleteType);
            return View(animalType);
        }

        [HttpPost]
        public IActionResult DeleteAnimalType(AnimalTypeModel model)
        {
            var deleteResult = UnitOfWork.AnimalType.Delete(model.TipId);
            if (deleteResult != null && deleteResult.TipId == model.TipId)
                return RedirectToAction("ShowAnimalsTypes");
            else return View();
        }
    }
}
