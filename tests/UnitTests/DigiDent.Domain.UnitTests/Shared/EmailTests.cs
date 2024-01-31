using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UnitTests.Shared.TestUtils;
using FluentAssertions;

namespace DigiDent.Domain.UnitTests.Shared;

public class EmailTests
{
    [Theory]
    [MemberData(nameof(CreateValidEmails))]
    public void Create_WithValidEmail_ShouldCreateAndReturnEmail(string validEmail)
    {
        //Act 
        var emailResult = Email.Create(validEmail);
        
        //Assert
        emailResult.IsSuccess.Should().BeTrue();
        emailResult.Value.Should().NotBeNull();
        emailResult.Value?.Value.Should().NotBeNullOrWhiteSpace();
        emailResult.Value?.Value.Should().Be(validEmail);
    }
    
    [Theory]
    [MemberData(nameof(CreateInvalidEmails))]
    public void Create_WithInvalidEmail_ShouldReturnFailure(string invalidEmail)
    {
        //Act 
        var emailResult = Email.Create(invalidEmail);
        
        //Assert
        emailResult.IsFailure.Should().BeTrue();
        emailResult.Value.Should().BeNull();
    }
    
    public static IEnumerable<object[]> CreateValidEmails()
    {
        return EmailFactory.ValidEmails
            .Select(validEmail => (object[])[validEmail]);
    }
    
    public static IEnumerable<object[]> CreateInvalidEmails()
    {
        return EmailFactory.InvalidEmails
            .Select(invalidEmail => (object[])[invalidEmail]);
    }
}