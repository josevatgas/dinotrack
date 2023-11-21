using System.ComponentModel.DataAnnotations;

namespace Dinotrack.Shared.DTOs
{
    public class ImageDTO
    {
        [Required]
        public int RefId { get; set; }

        [Required]
        public List<string> Images { get; set; } = null!;
    }
}