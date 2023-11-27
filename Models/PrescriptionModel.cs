using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace E_Nompilo_Healthcare_system.Models
{
    public class PrescriptionModel
    {
        [Key]
        public int PrescriptionId { get; set; }

        [Required(ErrorMessage = "Medication name is required.")]
        public string? MedicationName { get; set; }

        [Required(ErrorMessage = "Dosage is required.")]
        [Display(Name ="Dosage Form")]
        public string Dosageamount { get; set; }

        [Required(ErrorMessage = "Frequency is required.")]
        public string Frequency { get; set; }



        [Display(Name = "Date Prescribed")]
        [Required(ErrorMessage = "Date prescribed is required.")]
        [DataType(DataType.Date)]
        public DateTime DatePrescribed { get; set; } 
        [Display(Name = "Prescription Expiration Date")]
        [Required(ErrorMessage = "Prescription Expiration Date is required.")]
        [DataType(DataType.Date)]
        public DateTime PrescriptionExpirationDate { get; set; }

        [Required(ErrorMessage = "Comment name is required.")]
        public string Comment { get; set; }

        public int? PrescripId { get; set; }
        [ForeignKey("PrescripId")]
        public virtual RequestPrescripModel? Prescription_Request { get; set; }

        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? HUser { get; set; }

        public PrescriptionModel()
        {
            DatePrescribed = DateTime.Now;
        }


    }
}
