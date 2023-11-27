using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Resourse
    {
        [Key]

        public int InformationID { get; set; }

        [Required]
        public string? ContraceptiveMethods { get; set; }

        [Required]
        public string? Effectiveness { get; set; }

        [Required]
        public string? HealthBenefits { get; set; }

        [Required]
        public string? SideEffects { get; set; }

        [Required]
        public string? Reversibility { get; set; }

        [Required]
        public string? AvailabilityandAccess { get; set; }

        //[Required]
        //public byte[]? ImageData { get; set; }

        //public string? ImageType { get; set; }

        //public string Status { get; internal set; }
        //public string DateCreated { get; internal set; }
    }

}

