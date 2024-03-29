﻿using DigiDent.ClinicManagement.Domain.Employees.Administrators;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees.Administrators;

public class AdministratorsRepository : EmployeesRepository<Administrator>
{
    public AdministratorsRepository(ClinicManagementDbContext context)
        : base(context)
    {
    }
}