using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Referrals
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display]
        public string? Patient { get; set; }
        [ForeignKey("Patient")]
        public virtual HealthcareSystemUser? MainUsers { get; set; }
        [Required]
        [Display(Name = "Referred To")]
        public string? ReferredTo { get; set; }
        [Required]
        [Display(Name = "Referral Date")]
        public DateTime ReferralDate { get; set; } = DateTime.Now;
        [Required]
        public string Reasons { get; set; }
        [Required]
        [Display(Name = "Specific Concerns")]
        public string SpecificConcerns { get; set; }
    }
}
