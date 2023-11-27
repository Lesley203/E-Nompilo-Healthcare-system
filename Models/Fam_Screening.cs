using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;

namespace E_Nompilo_Healthcare_system.Models
{ 
    public class Fam_Screening
    {
        [Key]

        public int ScreeningID { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]

        public DateTime Date { get; set; }

        public string? PatientID { get; set; }
        [ForeignKey("PatientID")]

        public virtual HealthcareSystemUser? MainUser { get; set; }

        public string?Status { get; set; }

        //Questions

        [Display(Name = "1. Are you Drinking?")]
        [Required]

        public int Question1 { get; set; }

        [Display(Name = "2. Are you currently using any form of contraception?")]
        [Required]

        public int Question2 { get; set; }


        [Display(Name = "3.Are you currently pregnant or trying to become pregnant?")]
        [Required]

        public int Question3 { get; set; }


        [Display(Name = "4. Are your menstrual periods regular, irregular, heavy, or painful?")]
        [Required]

        public int Question4 { get; set; }


        [Display(Name = "5. Do you have any chronic medical conditions or take any medications?")]
        [Required]

        public int Question5 { get; set; }


        [Display(Name = "6. Are you sexually Active")]
        [Required]

        public int Question6 { get; set; }


        [Display(Name = "7. Are you Smoking?")]
        [Required]

        public int Question7 { get; set; }


        [Display(Name = "8. Would you like your partner to be involved in the family planning discussion or counseling?")]
        [Required]

        public int Question8 { get; set; }


        [Display(Name = "9. Would you like information on sexual health education, including safe sex practices, STI prevention, and consent?")]
        [Required]

        public int Question9 { get; set; }


        [Display(Name = "10. Do you have any cultural or religious beliefs that may influence your family planning decisions?")]
        [Required]

        public int Question10{ get; set; }

        //[Display(Name = "11. Would you want to get pregnant in the future?")]
        //[Required]

        //public int Question11 { get; set; }
        [Column("Total")]
        public int Total { get; internal set; }

        public Fam_Screening()
        {
            Date = DateTime.Now;
            Status = "New";
        }
    }
}
