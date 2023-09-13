using System.ComponentModel.DataAnnotations;

namespace Dinotrack.Shared.Entities
{
    public class Ref
    {
        public int Id { get; set; }

        [Display(Name = "Referencia")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public int BrandId { get; set; }

        public Brand? Brand { get; set; }

    }
}
