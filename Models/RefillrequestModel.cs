using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{
    public class RefillrequestModel
    {
        [Key]
        public int RefillId { get; set; }

        [Required(ErrorMessage = "Medication name is required.")]
        public string MedicationName { get; set; }

       

        [Display(Name = "Request Date")]
        [Required(ErrorMessage = "Request date is required.")]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "Requested quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid quantity greater than zero.")]
        public int RequestedQuantity { get; set; }

        [Required]
        [DisplayName("Notes")]
        [DataType(DataType.Text)]
        public string Notes { get; set; }

        [Required]
        [DisplayName("Dosage Form")]
        [DataType(DataType.Text)]
        public string DosageForm { get; set; }

        //public int? PrescriptionId { get; set; }
        //[ForeignKey("PrescriptionId")]
        //public virtual PrescriptionModel? Requeste_User { get; set; }

        public RefillrequestModel()
        {
            RequestDate = DateTime.Now;
        }




        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? HCUser { get; set; }

        public bool IsCompleted { get; set; }

        [DisplayName("Status")]
        public RefillStatus Statuss { get; set; }

       

    }
    public enum RefillStatus
    {
        Pending,
        Approve,
        Approved
    }
}
