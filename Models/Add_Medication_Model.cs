using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Add_Medication_Model
    {
        [Key]
        public int MedicationId { get; set; }

        [Required(ErrorMessage = "Medication name is required.")]
        public string MedicationName { get; set; }

        [Required(ErrorMessage = "Manufacturer name is required.")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Active ingredient is required.")]
        public string ActiveIngredient { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid dosage value.")]
        public int Dosage { get; set; }

        [Display(Name = "Dosage Form")]
        public string DosageForm { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public Add_Medication_Model()
        {
            DateCreated = DateTime.Now;
        }

        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? HCUser { get; set; }



    }
}
