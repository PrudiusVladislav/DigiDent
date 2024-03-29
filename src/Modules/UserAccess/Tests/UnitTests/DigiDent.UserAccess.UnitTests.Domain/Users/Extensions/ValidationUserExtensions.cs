﻿using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.ValueObjects;
using FluentAssertions;

namespace DigiDent.UserAccess.UnitTests.Domain.Users.Extensions;

public static class ValidationUserExtensions
{
    public static void ShouldBeCreatedFrom(
        this User? createdUser,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber,
        Password password,
        Role role)
    {
        createdUser.Should().NotBeNull();
        createdUser!.FullName.Should().Be(fullName);
        createdUser.Email.Should().Be(email);
        createdUser.PhoneNumber.Should().Be(phoneNumber);
        createdUser.Password.Should().Be(password);
        createdUser.Role.Should().Be(role);
    }
}