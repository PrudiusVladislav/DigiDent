using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;

public record GetAllDoctorsQuery: IQuery<IReadOnlyCollection<DoctorDTO>>;