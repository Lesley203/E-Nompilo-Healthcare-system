using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{
    public class SelfHistoryModel
    {

        [Key]
        [Required]
        public int SelfDigId { get; set; }

        [Required]
        [DisplayName("Date")]
        [DataType(DataType.Text)]
        public DateTime GetDateTime { get; set; }

        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? HCUser { get; set; }

        public int? DiagnosisId { get; set; }
        [ForeignKey("DiagnosisId")]
        public virtual SelfDiagnosModel? SelfDiagUser { get; set; }

        public SelfHistoryModel()
        {
            GetDateTime = DateTime.Now;
        }

        
    }

}
