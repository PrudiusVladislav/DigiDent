namespace DigiDent.EFCorePersistence.UserAccess;

/// <summary>
/// Marks entity configuration class to be applied to <see cref="UserAccessDbContext"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class UserAccessEntityConfigurationAttribute: Attribute
{
}