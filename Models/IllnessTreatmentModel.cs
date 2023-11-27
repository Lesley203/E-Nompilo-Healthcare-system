using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{
    public class IllnessTreatmentModel
    {
        [Key]
        [Required]
        public int TreatmentId { get; set; }

        [Required]
        [DisplayName("Treatment")]
        public string Illness_Treatment { get; set; }

        public int DiagnosisId { get; set; }
        [ForeignKey("DiagnosisId")]
        public virtual SelfDiagnosModel? Diagnos { get; set; }
    }
}
