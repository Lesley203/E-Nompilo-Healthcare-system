using System.ComponentModel.DataAnnotations;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Get_In_Touch_Contacts
    {

        [key]
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string Full_Names { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^1?[0-9]{10}$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumb { get; set; }
    }
}
