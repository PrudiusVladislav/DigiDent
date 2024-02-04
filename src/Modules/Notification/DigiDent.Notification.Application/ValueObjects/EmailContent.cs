namespace DigiDent.Notification.Application.ValueObjects;

public record EmailContent(
    string Subject,
    string HtmlBody);