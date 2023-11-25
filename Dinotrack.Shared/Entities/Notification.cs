using Dinotrack.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dinotrack.Shared.Entities
{
    public class Notification
    {
        public int Id { get; set; }

        public User? User { get; set; }

        public string? UserId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Tipo Notificación")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public NotificationTypeEnum NotificationType { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [Display(Name = "Detalle")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string? Remarks { get; set; }

        [Display(Name = "Estado")]
        [Required]
        public NotificationStateEnum NotificationState { get; set; }
    }
}