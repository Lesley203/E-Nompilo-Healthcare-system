using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Ultra_Sound_Image
    {
        [Key]
        public int NutritionTrackingID { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public string? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual HealthcareSystemUser? MainUser { get; set; }
        public string? Status { get; set; }

        //Questions
        [Display(Name = "1.How many weeks of pregnancy?.")]
        [Required]
        public int Question1 { get; set; }
        [Display(Name = "2.Does the baby make movements?.")]
        [Required]
        public int Question2 { get; set; }
        [Display(Name = "3.What kind of movements do you usually feel?.")]
        [Required]
        public int Question3 { get; set; }
        [Display(Name = "4.Where do you typically feel the movements?.")]
        [Required]
        public int Question4 { get; set; }

        public int Total { get; set; }

        public Ultra_Sound_Image()
        {
            Date = DateTime.Now;
            Status = "New";
        }
    }
}
