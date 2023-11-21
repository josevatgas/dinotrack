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

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Modelo")]
        [Range(1900, 9999, ErrorMessage = "El campo {0} debe tener {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Model { get; set; }

        public int BrandId { get; set; }

        public Brand? Brand { get; set; }

        public ICollection<RefImage>? RefImages { get; set; }

        [Display(Name = "Imágenes")]
        public int RefImagesNumber => RefImages == null || RefImages.Count == 0 ? 0 : RefImages.Count;

        [Display(Name = "Imagén")]
        public string MainImage => RefImages == null || RefImages.Count == 0 ? string.Empty : RefImages.FirstOrDefault()!.Image;
    }
}