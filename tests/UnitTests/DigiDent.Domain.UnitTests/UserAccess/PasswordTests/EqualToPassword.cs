﻿using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using FluentAssertions;

namespace DigiDent.Domain.UnitTests.UserAccess.PasswordTests;

public class EqualToPassword
{
    [Theory]
    [MemberData(nameof(CreatePasswordsEqualityExpectations))]
    public void IsEqualTo_ShouldCorrectlyIdentifyEquality(
        string validPassword, string passwordToCompare, bool expectedResult)
    {
        // Arrange
        Password password = Password.Create(validPassword).Value!;
        
        // Act
        bool isEqual = password.IsEqualTo(passwordToCompare);
        
        // Assert
        isEqual.Should().Be(expectedResult);
    }

    public static IEnumerable<object[]> CreatePasswordsEqualityExpectations()
    {
        return PasswordUtils.PasswordsEqualityExpectations
            .Select(expectation => (
                object[])[expectation.Item1, expectation.Item2, expectation.Item3]);
    }
}