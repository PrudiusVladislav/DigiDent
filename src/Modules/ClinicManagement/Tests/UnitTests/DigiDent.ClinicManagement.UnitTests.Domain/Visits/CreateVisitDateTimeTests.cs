using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.UnitTests.Domain.Visits.TestUtils.Constants;
using DigiDent.Shared.Kernel.Abstractions;
using FluentAssertions;
using NSubstitute;

namespace DigiDent.ClinicManagement.UnitTests.Domain.Visits;

public class CreateVisitDateTimeTests
{
    [Fact]
    public void Create_WithValueInFuture_ShouldSucceed()
    {
        // Arrange
        DateTime value = VisitDateTimeConstants.AnyDateTime;
        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        dateTimeProvider.Now.Returns(
            value.Subtract(VisitDateTimeConstants.DefaultVisitDateTimeOffset));
        
        // Act
        var result = VisitDateTime.Create(value, dateTimeProvider);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Value.Should().Be(value);
    }
    
    [Fact]
    public void Create_WithValueInPast_ShouldFail()
    {
        // Arrange
        DateTime value = VisitDateTimeConstants.AnyDateTime;
        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        dateTimeProvider.Now.Returns(
            value.Add(VisitDateTimeConstants.DefaultVisitDateTimeOffset));
        
        // Act
        var result = VisitDateTime.Create(value, dateTimeProvider);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
    }
}