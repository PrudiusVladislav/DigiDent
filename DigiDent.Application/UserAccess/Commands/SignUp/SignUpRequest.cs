﻿namespace DigiDent.Application.UserAccess.Commands.SignUp;

public sealed record SignUpRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string Role);