using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Models
{
    public class AnimalTypeModel
    {
        public int TipId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio..")]
        public string? TipNombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria..")]
        public string? TipDescripcion { get; set; }
    }
}
