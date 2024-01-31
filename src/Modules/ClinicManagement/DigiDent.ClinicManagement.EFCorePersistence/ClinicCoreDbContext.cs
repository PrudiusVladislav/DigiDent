using System.Reflection;
using DigiDent.ClinicManagement.Domain.Employees.Administrators;
using DigiDent.ClinicManagement.Domain.Employees.Assistants;
using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.ClinicManagement.Domain.Visits;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.ClinicManagement.EFCorePersistence;

public class ClinicCoreDbContext: DbContext
{
    internal const string ClinicCoreSchema = "Clinic_Core";
    
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Assistant> Assistants { get; set; } = null!;
    public DbSet<Administrator> Administrators { get; set; } = null!;
    
    public DbSet<WorkingDay> WorkingDays { get; set; } = null!;
    public DbSet<SchedulePreference> SchedulePreferences { get; set; } = null!;
    
    public DbSet<Patient> Patients { get; set; } = null!;
    
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<PastVisit> PastVisits { get; set; } = null!;
    
    public DbSet<ProvidedService> ProvidedServices { get; set; } = null!;
    public DbSet<TreatmentPlan> TreatmentPlans { get; set; } = null!;
    
    public ClinicCoreDbContext(DbContextOptions<ClinicCoreDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(ClinicCoreSchema);
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ClinicCoreDbContext).Assembly);
        
        //configuring Employee hierarchy
        modelBuilder.Entity<Employee>()
            .HasDiscriminator<string>("EmployeeType")
            .HasValue<Doctor>("Doctor")
            .HasValue<Assistant>("Assistant")
            .HasValue<Administrator>("Administrator");
    }
}
