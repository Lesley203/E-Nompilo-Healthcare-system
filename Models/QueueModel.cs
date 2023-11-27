using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

using E_Nompilo_Healthcare_system.Areas.Identity.Data;

namespace E_Nompilo_Healthcare_system.Models
{
    public class QueueModel
    {

        [Key]
        [Required]
        public int QueueId { get; set; }
            
        [Required]
        [DisplayName("status")]
        [DataType(DataType.Text)]
        public string status { get; set; }

      

        public QueueModel()
        {
            status = "OnHold";
            
        }

        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? HCUser { get; set; }

        
    }
}
