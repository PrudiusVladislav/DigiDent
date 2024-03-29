﻿using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.Shared.UnitTests.Domain.TestUtils;
using FluentAssertions;

namespace DigiDent.Shared.UnitTests.Domain.ValueObjectsTests;

public class PhoneNumberTests
{
    [Theory]
    [MemberData(nameof(CreateValidPhoneNumbers))]
    public void Create_WithValidPhoneNumber_ShouldCreateAndReturnPhoneNumber(
        string validPhoneNumber)
    {
        // Act
        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(validPhoneNumber);

        // Assert
        phoneNumberResult.IsSuccess.Should().BeTrue();
        phoneNumberResult.Value.Should().NotBeNull();
        phoneNumberResult.Value?.Value.Should().NotBeNullOrWhiteSpace();
        phoneNumberResult.Value?.Value.Should().Be(validPhoneNumber);
    }
    
    [Theory]
    [MemberData(nameof(CreateInvalidPhoneNumbers))]
    public void Create_WithInvalidPhoneNumber_ShouldReturnFailure(
        string invalidPhoneNumber)
    {
        // Act
        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(invalidPhoneNumber);

        // Assert
        phoneNumberResult.IsFailure.Should().BeTrue();
        phoneNumberResult.Value.Should().BeNull();
    }
    
    public static IEnumerable<object[]> CreateValidPhoneNumbers()
    {
        return PhoneNumberFactory.ValidPhoneNumbers
            .Select(validPhoneNumber => (object[])[validPhoneNumber]);
    }
    
    public static IEnumerable<object[]> CreateInvalidPhoneNumbers()
    {
        return PhoneNumberFactory.InvalidPhoneNumbers
            .Select(invalidPhoneNumber => (object[])[invalidPhoneNumber]);
    }
}