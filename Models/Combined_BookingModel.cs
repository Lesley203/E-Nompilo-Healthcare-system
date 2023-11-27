using E_Nompilo_Healthcare_system.Areas.Identity.Data;

namespace E_Nompilo_Healthcare_system.Models
{
    public class Combined_BookingModel
    {
        public HealthcareSystemUser HUser { get; set; }
        public AppointmentModel AppointmentModel { get; set; }
    }
}
