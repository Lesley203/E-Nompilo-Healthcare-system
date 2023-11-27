using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace E_Nompilo_Healthcare_system.Models
{
    public class EditUserVieModel
    {

        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }



        [Required]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^1?[0-9]{10}$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumb { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public System.DateTime DateofBirth { get; set; }
    }
}
