using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.UnitTests.ClinicCore.Visits.TestUtils;
using FluentAssertions;

namespace DigiDent.Domain.UnitTests.ClinicCore.Visits;

public class CreateProvidedServiceDetailsTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnSuccessResult()
    {
        // Arrange
        (string validName, string validDescription) = ProvidedServiceDetailsFactory
            .ValidProvidedServiceDetails;

        // Act
        var result = ProvidedServiceDetails.Create(validName, validDescription);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value?.Name.Should().Be(validName);
        result.Value?.Description.Should().Be(validDescription);
    }

    [Theory]
    [MemberData(nameof(CreateInvalidProvidedServiceDetails))]
    public void Create_WithInvalidData_ShouldReturnFailureResult(
        string invalidName, string invalidDescription)
    {
        // Act
        var result = ProvidedServiceDetails.Create(invalidName, invalidDescription);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
    }
    
    public static IEnumerable<object[]> CreateInvalidProvidedServiceDetails()
    {
        return ProvidedServiceDetailsFactory.InvalidProvidedServiceDetails
            .Select(invalidDetails => 
                (object[])[invalidDetails.Item1, invalidDetails.Item2]);
    }
}