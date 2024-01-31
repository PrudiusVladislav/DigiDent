using DigiDent.Shared.Domain.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using FluentAssertions;

namespace DigiDent.Domain.UnitTests.UserAccess.Users.Extensions;

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