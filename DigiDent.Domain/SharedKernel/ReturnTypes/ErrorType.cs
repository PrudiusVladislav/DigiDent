namespace DigiDent.Domain.SharedKernel.ReturnTypes;

/// <summary>
/// Enum that represents the type of an error that can occur in the domain.
/// </summary>
public enum ErrorType
{
    NotFound,
    Conflict,
    Validation,
    Unauthorized
}