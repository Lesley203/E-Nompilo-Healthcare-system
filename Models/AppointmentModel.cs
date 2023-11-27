using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Nompilo_Healthcare_system.Models
{

    public class AppointmentModel
    {


        [Key]
        [Required]
        public int BookingId { get; set; }

        public char RecStatus { get; set; } = 'A';

        [Required]
        [DisplayName("Date Of Appointment")]
        [DataType(DataType.DateTime)]
        public System.DateTime DateofAppointment { get; set; }

        [Required]
        [DisplayName("Time")]
        public string Time { get; set; }


        [DataType(DataType.Text)]
        [DisplayName("Consultant")]
        public string ConsultingPerson { get; set; }

        [Required]
        [DisplayName("Type Of Appointment")]
        [DataType(DataType.Text)]
        public string TypeOfAppointment { get; set; }

        [Required]
        [DisplayName("Notes")]
        [DataType(DataType.Text)]
        public string Notes { get; set; }

        [Required]
        [DisplayName("F irst Time Visit")]
        [DataType(DataType.Text)]
        public string FirstTimeVisit { get; set; }

        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual HealthcareSystemUser? HCUser { get; set; }

        public bool IsCompleted { get; set; }

        [DisplayName("Status")]
        public BookingStatus Status { get; set; }

       

    }
    public enum BookingStatus
    { 
        OnHold,
        Queued,
        YourTurn,
        InProgress,
        Complete
    }
}
