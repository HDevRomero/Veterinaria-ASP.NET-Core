using Microsoft.AspNetCore.Mvc;
using Veterinaria.Models;
using Veterinaria.Services.Implementations;

namespace Veterinaria.Controllers
{
    public class AnimalController : Controller
    { 
        public IActionResult ShowAnimals()
        {
            var animalList = UnitOfWork.Animal.GetAll();
            return View(animalList);
        }

        [HttpGet]
        public IActionResult SaveAnimal()
        {
            var model = new AnimalModel
            {
                Owners = UnitOfWork.Animal.GetOwners(),
                AnimalTypes = UnitOfWork.Animal.GetAnimalTypes()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SaveAnimal(AnimalModel model)//
        {
            var modelNew = new AnimalModel//
            {
                Owners = UnitOfWork.Animal.GetOwners(),
                AnimalTypes = UnitOfWork.Animal.GetAnimalTypes()
            };

            if (!ModelState.IsValid) return View(modelNew);
            var addResult = UnitOfWork.Animal.Add(model);
            if (addResult != null) return RedirectToAction("ShowAnimals");
            else
                return View(model);            
        }

        [HttpGet]
        public IActionResult DeleteAnimal(int idDeleteAnimal)
        {
            var animal = UnitOfWork.Animal.GetById(idDeleteAnimal);
            return View(animal);
        }

        [HttpPost]
        public IActionResult DeleteAnimal(AnimalModel model)
        {
            var deleteResult = UnitOfWork.Animal.Delete(model.AniId);
            if (deleteResult != null && deleteResult.AniId == model.AniId)
                return RedirectToAction("ShowAnimals");
            else return View();
        }
    }
}
