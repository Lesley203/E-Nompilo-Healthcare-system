using System.ComponentModel.DataAnnotations;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Alert_V
    {
        internal string intendedUser;

        [Key]
        public int AlertID { get; set; }
        public string? Message { get; set; }
        public string? Date { get; set; }
        public string? status { get; set; }
        public int LastView { get; set; }
        public  string? Role { get; set; }
        public string? IntendedUser { get; set;}
        public string RoleController { get; internal set; }

        public Alert_V()
        {
            Date = DateTime.Now.ToString("DD/MM/YYYY HH:mm:ss");
            status = "New";
            LastView = -1;
            Role = "ALL";
            IntendedUser = "ALL";
        }

      
    }
}
