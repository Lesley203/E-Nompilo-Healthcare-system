using System.ComponentModel.DataAnnotations;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Vaccine_Information
    {

        [Key]
        public int InformationID { get; set; }
        [Required]
        public string? VaccineName { get;set;}
        [Required]
        public string? VaccineType { get; set; }
        [Required]
        public string? VaccineDescription { get; set; }
        [Required]
        public int? Dose_Number { get; set; }
       

    }
}
