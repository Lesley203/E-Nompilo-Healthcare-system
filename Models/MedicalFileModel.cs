using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using static E_Nompilo_Healthcare_system.Areas.Identity.Pages.Account.RegisterModel;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Services;

namespace E_Nompilo_Healthcare_system.Models
{
    public class MedicalFileModel
    {


        [Key]
        [Required]
        public int PatientMedicalId { get; set; }



        [Required]
        [DisplayName("Identity No")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "The field must contain exactly 13 digits")]
        public string IdentityNo { get; set; }



        [Required]
        [DisplayName("Residential Address")]
        public string Address1 { get; set; }

        [DisplayName("Residential Address")]
        public string Address2 { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("City")]
        public string City { get; set; }

        [Required]
        [DisplayName("Postal Code")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid postal code format")]
        public string postalCode { get; set; }

        [Required]
        [DisplayName("Country")]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Required]
        [DisplayName("First Name")]
        [DataType(DataType.Text)]
        public string KinFirstName { get; set; }

        [Required]
        [DisplayName("Relationship")]
        [DataType(DataType.Text)]
        public string KinRelationship { get; set; }

        [Required]
        [DisplayName("Kin Contact")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format")]
        public string KinContact { get; set; }

        [Required]
        [DisplayName("Kin Email")]
        [DataType(DataType.EmailAddress)]
        public string KinEmail { get; set; }

        [Required]
        [DisplayName("Residential Address")]
        public string KinAddress1 { get; set; }

        [DisplayName("Residential Address")]
        public string KinAddress2 { get; set; }

        [Required]
        [DisplayName("Kin Postal Code")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid postal code format")]
        public string KinPostalcode { get; set; }

        [Required]
        [DisplayName("Taking Medication")]
        [DataType(DataType.Text)]
        public string TakingMed { get; set; }

        [Required]
        [DisplayName("Allergies")]
        [DataType(DataType.Text)]
        public string allergies { get; set; }

        [DisplayName("Medical Information")]
        [DataType(DataType.Text)]
        public string MedicalInfor { get; set; }

        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? HCUser { get; set; }

       
       
    }
}
