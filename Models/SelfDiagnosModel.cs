using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace E_Nompilo_Healthcare_system.Models
{
    public class SelfDiagnosModel
    {

        [Key]
        [Required]
        public int DiagnosisId { get; set; }

        [Required]
        [DisplayName("Symptom Name")]
        [DataType(DataType.Text)]
        public string SymptomName { get; set; }

        [Required]
        [DisplayName("Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Treatment")]
        [DataType(DataType.Text)]
        public string Treatment { get; set; }

        

    }
}
