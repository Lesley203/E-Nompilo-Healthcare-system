using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Pre_NatalCareNutritionTracking
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
        [Display(Name = "1.How often do you consume a variety of fruits and vegetables each day?.")]
        [Required]
        public int Question1 { get; set; }
        [Display(Name = "2.Are you taking a pre-natal vitamin or supplement?.")]
        [Required]
        public int Question2 { get; set; }
        [Display(Name = "3.How often do you include diary or diary alternatives in your diet for calcium?.")]
        [Required]
        public int Question3 { get; set; }
        [Display(Name = "4.Do you consume foods rich in Iron(e.g lean meats, beans, fortified cereals) regulary?.")]
        [Required]
        public int Question4 { get; set; }
        [Display(Name = "5.How often do you eat fatty fidh like salmon or trout for omega-3 fatty acids?.")]
        [Required]
        public int Question5 { get; set; }
        [Display(Name = "6.Have you experienced any significant food aversions or cravings during your pregnancy?.")]
        [Required]
        public int Question6 { get; set; }
        [Display(Name = "7.Are you aware of the recommended daily intake of folic acid during pregnancy?.")]
        [Required]
        public int Question7 { get; set; }
        [Display(Name = "8.How many glasses of water do you typically drink in a day?.")]
        [Required]
        public int Question8 { get; set; }
        [Display(Name = "9.Have you discussed your dietary choices and Pre-natal nuttrion with your healthcare Provider?.")]
        [Required]
        public int Question9 { get; set; }
        [Display(Name = "10.Do you consume any alcoholic beverages or sugary drinks during your pregnancy?.")]
        [Required]
        public int Question10 { get; set; }
        [Display(Name = "11.How would rate your adherance to a balanced and healthy pre-natal care diet?.")]
        [Required]
        public int Question11 { get; set; }
        [Display(Name = "12.Do you take any other dietary supplements, herbs, or medication not prescibed by your healthcare provider?.")]
        [Required]
        public int Question12 { get; set; }
        public int Total { get; set; }

        public Pre_NatalCareNutritionTracking()
        {
            Date = DateTime.Now;
            Status = "New";
        }
    }
}
