using System.ComponentModel.DataAnnotations;

namespace Dinotrack.Shared.Entities
{
    public class RefImage
    {
        public int Id { get; set; }

        public Ref Ref { get; set; } = null!;

        public int RefId { get; set; }

        [Display(Name = "Imagen")]
        public string Image { get; set; } = null!;
    }
}