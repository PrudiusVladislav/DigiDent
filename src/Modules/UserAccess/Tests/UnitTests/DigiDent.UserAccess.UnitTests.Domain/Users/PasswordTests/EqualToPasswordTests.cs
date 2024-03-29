﻿using DigiDent.UserAccess.Domain.Users.ValueObjects;
using DigiDent.UserAccess.UnitTests.Domain.Users.TestUtils;
using FluentAssertions;

namespace DigiDent.UserAccess.UnitTests.Domain.Users.PasswordTests;

public class EqualToPasswordTests
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
        return PasswordFactory.PasswordsEqualityExpectations
            .Select(expectation => (
                object[])[expectation.Item1, expectation.Item2, expectation.Item3]);
    }
}