using System.ComponentModel.DataAnnotations;

namespace E_Nompilo_Healthcare_system.Models
{
    public class TimeSpanModel
    {

        [key]
        [Required]
        public int ID { get; set; }


        [Required]
        [DataType(DataType.Text)]
        public string Get_Time { get; set; }
    }
}
