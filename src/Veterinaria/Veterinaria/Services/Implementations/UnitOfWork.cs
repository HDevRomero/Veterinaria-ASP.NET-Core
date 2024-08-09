using Veterinaria.Models;

namespace Veterinaria.Services.Implementations
{
    public class UnitOfWork
    {
        public static AnimalTypeService<AnimalTypeModel> AnimalType = new AnimalTypeService<AnimalTypeModel>();
        public static OwnerService<OwnerModel> Owner = new OwnerService<OwnerModel>();
        public static AnimalService<AnimalModel> Animal = new AnimalService<AnimalModel>();
    }
}
