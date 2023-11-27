using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{


    public class Get_VaccinatedModel
    {

        [Key]
        [Required]
        public int VaccineId { get; set; }


        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? MainUser { get; set; }

        public char RecStatus { get; set; } = 'A';

        [Required]
        public string VaccineType { get; set; }

        [Required]
        public DateTime Date  { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public Get_VaccinatedModel()
        {
            Date= DateTime.Now;
        }

        [DisplayName("Status")]
        public VaccineStatus Status { get; set; }
        public bool IsCompleted { get; set; }
      
    }
    public enum VaccineStatus
    {
        Waiting,
        first_Vaccine,
        sec_Vaccine,
        third_Vaccine,
        Complete
    }
}
