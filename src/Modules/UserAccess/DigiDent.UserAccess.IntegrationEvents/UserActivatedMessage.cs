namespace DigiDent.UserAccess.IntegrationEvents;

public record UserActivatedMessage
{
    public string FullName { get; init; }
    public string Email { get; init; }
}