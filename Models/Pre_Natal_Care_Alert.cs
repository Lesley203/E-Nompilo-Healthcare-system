using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Pre_Natal_Care_Alert
    {

        [Key]
        public int AlertID { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        [Display(Name = "Last View")]
        public int LastView { get; set; }
        [Required]
        public string Role { get; set; }
        public string IntendedUser { get; set; }

        public Pre_Natal_Care_Alert()
        {
            Date = DateTime.Now;
            status = "New";
            LastView = -1;
            Role = "All";
            IntendedUser = "All";
        }
    }
}
