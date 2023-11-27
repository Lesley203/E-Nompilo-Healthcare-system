using E_Nompilo_Healthcare_system.Areas.Identity.Data;

namespace E_Nompilo_Healthcare_system.Models
{
    public class PhamacistRefillModel
    {
        public HealthcareSystemUser HUser { get; set; }
        public RefillrequestModel RefillrequestModel { get; set; }
    }
}
