using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Information
    {

        [key]

        public int Id { get; set; }


        public string Contraceptives { get; set; }

        public string? Info { get; set; }
 
    }
}
