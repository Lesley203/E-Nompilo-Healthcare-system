using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{


    public class Vaccine_reportingModel
    {

        [Key]

        [Required]
        public int Vaccination_FeddbackId { get; set; }

       

        [Required]
        public int VaccineId { get; set; }

        [Required]
        public string VaccineName { get; set; }

        [Required]
        [DisplayName("Date")]
        [DataType(DataType.DateTime)]
        public System.DateTime VaccinationDate { get; set; }
        
        [Required]
        [Display(Name = "Feedback")]
        public string PatientFeedback { get; set; }

        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? HCUser { get; set; }
    }
}
