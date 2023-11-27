using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{

    public class VaccineScreening
    {

        [Key]
        [Required]
        public int ScreeningID { get; set; } 

        public bool Cough { get; set; }

        public bool Dizzines { get; set; }

        public bool Abdominalpain { get; set; }

        public bool Headache { get; set; }

        public bool legswelling { get; set; }

        public bool snezzing { get; set; }

        public bool ChestPressure { get; set; }

        public bool Allergy { get; set; }

        public string Days { get; set; }

        public string TakenVac { get; set; }

        public DateTime GetDateSurvay { get; set; }

        public VaccineScreening()
        {
            GetDateSurvay = DateTime.Now;

        }

        public bool ShowAlert => (Days == "6-10 days" || Days == "10-15 days")   && (Cough || Dizzines || Abdominalpain || Headache || legswelling || snezzing || ChestPressure || Allergy);

        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? User { get; set; }


    }
}
