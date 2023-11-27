using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{
    public class RequestPrescripModel
    {
        [Key]
        public int PrescripId { get; set; }

        [Required(ErrorMessage = "Medical History is required.")]
        public string MedicalHistory { get; set; }

        [Required(ErrorMessage = "Current Symptoms are required.")]
        public string CurrentSymptoms { get; set; }

        public string ChronicConditions { get; set; }

        public string Allergies { get; set; }

        public string Notes { get; set; }

        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? HCUser { get; set; }
    }
}

