namespace DigiDent.Domain.UserAccessContext.Permissions;

public enum Permissions
{
    ViewDoctorSchedule = 1,
    ViewAdministratorSchedule = 2,
    ViewOverallClinicSchedule = 3,
    ViewDoctorsStatistics = 4,
    ViewPatientInformation = 5,
    
    ManageAppointments = 6,
    ManageTreatmentPlans = 7,
    ManageUsers = 8,
    
    SendEmailNotifications = 9,
    
    // Administrator = 1 << 6,
    // Dentist = ViewDoctorSchedule | ManageAppointments | ViewOwnSchedule,
    // Patient = ViewPatientInformation | ManageTreatmentPlans
}