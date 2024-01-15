using System.Reflection;
using DigiDent.Domain.ClinicCoreContext.Employees.Assistants;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Visits;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore;

public class ClinicCoreDbContext: DbContext
{
    private const string Schema = "Clinic_Core";
    
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Assistant> Assistants { get; set; } = null!;
    
    public DbSet<WorkingDay> WorkingDays { get; set; } = null!;
    public DbSet<SchedulePreference> SchedulePreferences { get; set; } = null!;
    
    public DbSet<Patient> Patients { get; set; } = null!;
    
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<Visit> Visits { get; set; } = null!;
    
    public DbSet<DentalProcedure> DentalProcedures { get; set; } = null!;
    public DbSet<TreatmentPlan> TreatmentPlans { get; set; } = null!;
    
    
    
    public ClinicCoreDbContext(DbContextOptions<ClinicCoreDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: resolve the hierarchy mappings here
        modelBuilder.HasDefaultSchema(Schema);
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type
                .GetCustomAttributes(typeof(ClinicCoreEntityConfigurationAttribute), true)
                .Any()
        );
    }
}