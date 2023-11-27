using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace E_Nompilo_Healthcare_system.Areas.Identity.Data;

public class HealthcareDbContext : IdentityDbContext<HealthcareSystemUser>
{
    public HealthcareDbContext(DbContextOptions<HealthcareDbContext> options)
        : base(options)
    {
    }
   

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        // builder.Entity<Claim>()
        //.HasOne(c => c.QueueModel)
        //.WithMany(u => u.Appointments)
        //.HasForeignKey(c => c.BookingId)
        //.OnDelete(DeleteBehavior.Restrict);

             builder.Entity<MedicalFileModel>()
            .HasOne(d => d.HCUser)
            .WithOne(u => u.MedicalFileModel)
            .HasForeignKey<MedicalFileModel>(d => d.Id);


    }
    public DbSet<MedicalFileModel> medFile { get; set; }
    public DbSet<AppointmentModel> Appointments { get; set; }
    
    public DbSet<SelfDiagnosModel> selfDiagnos { get; set; }
    public DbSet<Vaccination_Feedback_commentsModel> Vaccinations_Comments { get; set; }
    public DbSet<Add_Medication_Model> add_Medications { get; set; }
    public DbSet<RequestPrescripModel> requestPrescrips { get; set; }
    public DbSet<RefillrequestModel> refillrequests { get; set; }
    public DbSet<PrescriptionModel> prescriptions { get; set; }
    public DbSet<Referrals> referrals { get; set; }
    
    
    public DbSet<Fam_Screening>? Fam_Screening { get; set; }
    public DbSet<Counselling>? Counselling { get; set; }
    public DbSet<ContraceptivesRefill>? ContraceptivesRefill { get; set; }
 

   
    

    public DbSet<Pre_NatalCareNutritionTracking>? Pre_NatalCareNutritionTracking { get; set; }
    
  
 

    public DbSet<SelfHistoryModel> selfHistories { get; set; }
    public DbSet<IllnessTreatmentModel> illnessTreatments { get; set; }

    public override int SaveChanges()
    {

        foreach (var entry in ChangeTracker.Entries())
        {

            if (entry.State == EntityState.Deleted)
            {
                // Instead of physically deleting, mark as modified and set RecStatus to 'D'
                entry.State = EntityState.Modified;
                entry.CurrentValues["RecStatus"] = 'D';
            }

        }
        return base.SaveChanges();
    }
    public DbSet<VaccineScreening> VaccineScreening { get; set; }
    public DbSet<Alert_V> Alert_v { get; internal set; }
   

    public DbSet<Resourses>? Resourses { get; set; }

    public DbSet<RequestContraceptivesInfo>? RequestContraceptivesInfo { get; set; }

    public DbSet<Review>? Review { get; set; }

    public DbSet<Resourse>? Resourse { get; set; }


    public DbSet<TimeSpanModel> Get_Time { get; set; }

    public DbSet<Get_In_Touch_Contacts> In_Touch_Contacts { get; set; }

    public DbSet<E_Nompilo_Healthcare_system.Models.Fetal_Monitoring>? Fetal_Monitoring { get; set; }


    
    public DbSet<E_Nompilo_Healthcare_system.Models.Ultra_Sound_Image>? Ultra_Sound_Image { get; set; }

    public DbSet<E_Nompilo_Healthcare_system.Models.Pre_Natal_Care_Alert>? Pre_Natal_Care_Alert { get; set; }

    
    public DbSet<E_Nompilo_Healthcare_system.Models.Information>? Information { get; set; }

  
    

   

    

  
    

   
    public DbSet<Vaccine_Information> Vaccine_Information { get; set; }
    public DbSet<Get_VaccinatedModel> Get_Vaccinateds { get; set; }
    public DbSet<Vaccine_reportingModel> vaccine_Reportings { get; set; }
}


