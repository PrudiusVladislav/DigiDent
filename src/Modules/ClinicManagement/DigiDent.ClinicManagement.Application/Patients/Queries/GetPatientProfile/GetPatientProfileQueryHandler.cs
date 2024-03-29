﻿using AutoMapper;
using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.Patients.Queries.GetPatientProfile;

public sealed class GetPatientProfileQueryHandler
    : IQueryHandler<GetPatientProfileQuery, PatientProfileDTO?>
{
    private readonly IPatientsRepository _patientsRepository;
    private readonly IMapper _mapper;

    public GetPatientProfileQueryHandler(
        IPatientsRepository patientsRepository,
        IMapper mapper)
    {
        _patientsRepository = patientsRepository;
        _mapper = mapper;
    }

    public async Task<PatientProfileDTO?> Handle(
        GetPatientProfileQuery query, CancellationToken cancellationToken)
    {
        PatientId patientId = new(query.Id);
        
        Patient? patient = await _patientsRepository.GetByIdAsync(
            patientId, cancellationToken);

        return _mapper.Map<PatientProfileDTO?>(patient);
    }
}