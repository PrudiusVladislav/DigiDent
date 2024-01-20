using System.Reflection;
using DigiDent.Domain.ClinicCoreContext.Employees.Assistants;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Visits;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore;

public class ClinicCoreDbContext: DbContext
{
    internal const string ClinicCoreSchema = "Clinic_Core";
    
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Assistant> Assistants { get; set; } = null!;
    
    public DbSet<WorkingDay> WorkingDays { get; set; } = null!;
    public DbSet<SchedulePreference> SchedulePreferences { get; set; } = null!;
    
    public DbSet<Patient> Patients { get; set; } = null!;
    
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<PastVisit> PastVisits { get; set; } = null!;
    
    public DbSet<DentalProcedure> DentalProcedures { get; set; } = null!;
    public DbSet<TreatmentPlan> TreatmentPlans { get; set; } = null!;
    
    public ClinicCoreDbContext(DbContextOptions<ClinicCoreDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(ClinicCoreSchema);
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type
                .GetCustomAttributes(typeof(ClinicCoreEntityConfigurationAttribute), true)
                .Any()
        );
        
        //configuring Employee hierarchy
        modelBuilder.Entity<Employee>()
            .HasDiscriminator<string>("EmployeeType")
            .HasValue<Doctor>("Doctor")
            .HasValue<Assistant>("Assistant");
    }
}
