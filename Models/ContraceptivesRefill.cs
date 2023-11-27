using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;

namespace E_Nompilo_Healthcare_system.Models
{
    public class ContraceptivesRefill
    {

        [Key]

        public int RefillID { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public System.DateTime CollectionDate { get; set; }

        public string? PatientID { get; set; }
        [ForeignKey("PatientID")]

        public virtual HealthcareSystemUser? MainUser { get; set; }

        public string? Status { get; set; }


        [Required(ErrorMessage = "Contraceptive Name is required.")]
        public string ContraceptiveType { get; set; }


        [Required(ErrorMessage = "Please Enter your Last Refill Date.")]
        public DateTime LastRefillDate { get; set; }

        [Required(ErrorMessage = "Contacat Number is required.")]
        public int ContactNumber { get; set; }


        public string AdditionalInformation { get; set; }

        public ContraceptivesRefill()
        {
           
            Status = "New";
        }
    }
}
