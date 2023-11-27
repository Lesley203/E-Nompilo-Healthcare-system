using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;


namespace E_Nompilo_Healthcare_system.Models
{
    public class Counselling
    {
        [key]
        public int Id { get; set; }


        public string? PatientID { get; set; }
        [ForeignKey("PatientID")]

        public virtual HealthcareSystemUser? MainUser { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }

    }
}
