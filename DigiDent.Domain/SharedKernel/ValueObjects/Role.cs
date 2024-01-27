using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.UserAccessContext.Users.Errors;

namespace DigiDent.Domain.SharedKernel.ValueObjects;

public enum Role
{
    Administrator,
    Doctor,
    Assistant,
    Patient
}