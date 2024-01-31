using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.Shared.Domain.ValueObjects;
using DigiDent.Shared.UnitTests.Domain.TestUtils;
using FluentAssertions;

namespace DigiDent.Shared.UnitTests.Domain.Extensions;

public class FullNameTests
{
    [Theory]
    [MemberData(nameof(CreateValidNames))]
    public void Create_WithValidFullName_ShouldCreateAndReturnFullName(
        string validFirstName, string validLastName)
    {
        // Act
        Result<FullName> fullNameResult = FullName.Create(validFirstName, validLastName);

        // Assert
        fullNameResult.IsSuccess.Should().BeTrue();
        fullNameResult.Value.Should().NotBeNull();
        fullNameResult.Value?.FirstName.Should().Be(validFirstName);
        fullNameResult.Value?.LastName.Should().Be(validLastName);
    }
    
    [Theory]
    [MemberData(nameof(CreateInvalidNames))]
    public void Create_WithInvalidFullName_ShouldReturnFailure(
        string invalidFirstName, string invalidLastName)
    {
        // Act
        Result<FullName> fullNameResult = FullName.Create(invalidFirstName, invalidLastName);

        // Assert
        fullNameResult.IsFailure.Should().BeTrue();
        fullNameResult.Value.Should().BeNull();
    }
    
    public static IEnumerable<object[]> CreateValidNames()
    {
        return FullNameFactory.ValidNames
            .Select(validFullName => 
                (object[])[validFullName.Item1, validFullName.Item2]);
    }

    public static IEnumerable<object[]> CreateInvalidNames()
    {
        return FullNameFactory.InvalidNames()
            .Select(invalidFullName =>
                (object[]) [invalidFullName.Item1, invalidFullName.Item2]);
    }
}