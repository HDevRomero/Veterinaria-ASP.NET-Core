using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Veterinaria.Models
{
    public class AnimalModel
    {
        public int AniId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? AniNombre { get; set; }

        [Required(ErrorMessage = "Seleccione un propietario valido por favor.")]
        public string? ProIdFk { get; set; }
        public string? ProNombreCompleto { get; set; }

        [Required(ErrorMessage = "Seleccione un tipo de animal valido por favor.")]
        public int? TipIdFk { get; set; }
        public string? TipNombre { get; set; }

        public List<SelectListItem> Owners { get; set; } = new List<SelectListItem>(); // Inicialización        
        public List<SelectListItem> AnimalTypes { get; set; } = new List<SelectListItem>(); // Inicialización
    }
}