using AutoMapper;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.ClinicManagement.Application;

/// <summary>
/// Contains common AutoMapper profiles that are shared among multiple aggregates.
/// </summary>
public class SharedMappingProfiles: Profile
{
    public SharedMappingProfiles()
    {
        CreateMap(typeof(ITypedId<Guid>), typeof(Guid))
            .ConvertUsing(typeof(TypedIdGuidConverter));
        
        CreateMap<FullName, string>()
            .ConvertUsing(fullName => fullName.ToString());
        
        CreateMap<Email, string>()
            .ConvertUsing(email => email.Value);
        
        CreateMap<PhoneNumber, string>()
            .ConvertUsing(phoneNumber => phoneNumber.Value);

        CreateMap<Gender, string>()
            .ConvertUsing(gender => gender.ToString());
        
        CreateMap<Money, decimal>()
            .ConstructUsing(money => money.Amount);
        
        CreateMap<TimeDuration, TimeSpan>()
            .ConvertUsing(duration => duration.Duration);
    }
    
    private class TypedIdGuidConverter: ITypeConverter<ITypedId<Guid>, Guid>
    {
        public Guid Convert(ITypedId<Guid> source, Guid destination, ResolutionContext context)
        {
            return source.Value;
        }
    }
}

