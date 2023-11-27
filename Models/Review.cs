using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Review
    {
        [key]
        public int ID { get; set; }


        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]

        public DateTime Date { get; set; }

        public string? PatientID { get; set; }
        [ForeignKey("PatientID")]

        public virtual HealthcareSystemUser? MainUser { get; set; }


        public string Method { get; set; }


        [Required(ErrorMessage = "Content is required.")]
        [StringLength(1000, ErrorMessage = "Content must be between 1 and 1000 characters.", MinimumLength = 1)]
        [DataType(DataType.MultilineText)]
        public string Reviews { get; set; }

        [Display(Name = "Rating")]
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
        public int Rating { get; set; }

        public Review()
        {
            Date = DateTime.Now;

        }
    }
}
