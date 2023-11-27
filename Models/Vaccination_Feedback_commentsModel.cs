using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Vaccination_Feedback_commentsModel
    {
        [Key]
        public int VaccinationCommentsId { get; set; }

        [Required]
        public string Comments { get; set; }

        public int? Vaccination_FeddbackId { get; set; }
        [ForeignKey("Vaccination_FeddbackId")]
        public virtual Vaccine_reportingModel? vacc_User { get; set; }

        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? HCUser { get; set; }
    }
}
