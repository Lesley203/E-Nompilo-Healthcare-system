using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using E_Nompilo_Healthcare_system.Models;

namespace E_Nompilo_Healthcare_system.Areas.Identity.Data;

// Add profile data for application users by adding properties to the HealthcareSystemUser class
public class HealthcareSystemUser : IdentityUser
{
    

    [Required]
    [StringLength(50)]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    [DataType(DataType.Text)]
    public string LastName { get; set; }

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

    [Required]
    [Display(Name = "Creation Date of Account")]
    [DataType(DataType.DateTime)]
    public System.DateTime Date_AccountCreated { get; set; }

    public ICollection<MedicalFileModel> MedicalFiles { get; set; }
    public MedicalFileModel MedicalFileModel { get; set; }

    public HealthcareSystemUser()
    {
        Date_AccountCreated = DateTime.Now;
    }


}

