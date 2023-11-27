using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;

namespace E_Nompilo_Healthcare_system.Models
{
    public class RequestContraceptivesInfo
    {
        [key]

        public int Id { get; set; }


        public string? PatientID { get; set; }
        [ForeignKey("PatientID")]

        public virtual HealthcareSystemUser? MainUser { get; set; }


        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]

        public DateTime Date { get; set; }

        [Required]
        public string Contraceptivetype { get; set; }

        [Required]
        public string AdditionalInfo { get; set; }

        public RequestContraceptivesInfo()
        {
            Date = DateTime.Now;
          
        }
    }
}
