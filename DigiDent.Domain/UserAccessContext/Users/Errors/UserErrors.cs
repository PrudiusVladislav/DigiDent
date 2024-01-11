﻿using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users.Errors;

public static class UserErrors
{
    public static Error UserDoesNotExist(UserId userId)
        => new(ErrorType.NotFound, $"User with id '{userId.Value}' does not exist.");
    
    public static Error CannotDeleteLastAdmin
        => new(ErrorType.Validation, "Cannot delete last administrator.");
}