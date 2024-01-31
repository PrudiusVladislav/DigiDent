using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.Shared.Kernel.ReturnTypes;
using FluentAssertions;

namespace DigiDent.ClinicManagement.UnitTests.Domain.Visits;

public class CreateMoneyTests
{
    [Theory]
    [MemberData(nameof(CreateValidMoney))]
    public void Create_WithValidMoney_ShouldCreateAndReturnMoney(
        decimal validMoneyValue)
    {
        // Act
        Result<Money> moneyResult = Money.Create(validMoneyValue);

        // Assert
        moneyResult.IsSuccess.Should().BeTrue();
        moneyResult.Value.Should().NotBeNull();
        moneyResult.Value?.Amount.Should().Be(validMoneyValue);
    }
    
    [Theory]
    [MemberData(nameof(CreateInvalidMoney))]
    public void Create_WithInvalidMoney_ShouldReturnFailure(
        decimal invalidMoney)
    {
        // Act
        Result<Money> moneyResult = Money.Create(invalidMoney);

        // Assert
        moneyResult.IsFailure.Should().BeTrue();
        moneyResult.Value.Should().BeNull();
    }
    
    
    public static IEnumerable<object[]> CreateValidMoney()
    {
        yield return [0.0];
        yield return [100.1];
    }
    
    public static IEnumerable<object[]> CreateInvalidMoney()
    {
        yield return [-1.0];
        yield return [-100.1];
    }
}