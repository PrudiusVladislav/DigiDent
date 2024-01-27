namespace DigiDent.EFCorePersistence.ClinicCore;

/// <summary>
/// Marks entity configuration class to be applied to <see cref="ClinicCoreDbContext"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ClinicCoreEntityConfigurationAttribute: Attribute
{
}