using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Fetal_Monitoring
    {
        [Key]
        public int FetalMonitorID { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public string? PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual HealthcareSystemUser? MainUser { get; set; }
        public string? Status { get; set; }


        //Questions
        [Display(Name = "1.Are you aware of the importance of monitoring your baby's movements during pregnancy?.")]
        [Required]
        public int Question1 { get; set; }
        [Display(Name = "2.Do you feel your baby's movements regularly?.")]
        [Required]
        public int Question2 { get; set; }
        [Display(Name = "3.Have you been provided with guidance on how to count your baby's movements?.")]
        [Required]
        public int Question3 { get; set; }
        [Display(Name = "4.Have you discussed any concerns about decreased fetal movement with your healthcare provider?.")]
        [Required]
        public int Question4 { get; set; }
        [Display(Name = "5.Do you know how to perform kick counts or movements counts?.")]
        [Required]
        public int Question5 { get; set; }
        [Display(Name = "6.Have you noticed any significant changes in your baby's movements recently?.")]
        [Required]
        public int Question6 { get; set; }
        [Display(Name = "7.Are you familiar with the recommended frequency of fetal movement checks?.")]
        [Required]
        public int Question7 { get; set; }
        [Display(Name = "8.Have you received instructions on monitoring your baby's heart rate during pregnancy?.")]
        [Required]
        public int Question8 { get; set; }
        [Display(Name = "9.Are you aware of the different methods for monitoring your baby's heart rate, such as Doppler or electronic fetal monitoring?.")]
        [Required]
        public int Question9 { get; set; }
        [Display(Name = "10.Have you had any scheduled prenatal check-ups or tests that involve monitoring your baby's heart rate and movements?.")]
        [Required]
        public int Question10 { get; set; }
        [Display(Name = "11.Do you have a way to record your baby's movements or heart rate at home if needed?.")]
        [Required]
        public int Question11 { get; set; }
        public int Total { get; set; }

        public Fetal_Monitoring()
        {
            Date = DateTime.Now;
            Status = "New";
        }

    }
}
