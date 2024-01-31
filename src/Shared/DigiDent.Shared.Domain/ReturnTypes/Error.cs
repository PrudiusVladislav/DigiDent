namespace DigiDent.Shared.Domain.ReturnTypes;

/// <summary>
/// Represents an error. Mainly used in pair with <see cref="Result"/>.
/// </summary>
/// <param name="Type">Type of the error. See <see cref="ErrorType"/>.</param>
/// <param name="Message">Message of the error.</param>
public record Error(ErrorType Type, string Origin, string Message);