using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Visits.Errors;

public static class FeedbackErrors
{
    public static Error FeedbackRatingOutOfRange(int minRating, int maxRating) =>
        new(ErrorType.Validation,
            nameof(Feedback),
            $"Feedback rating must be between {minRating} and {maxRating}");
    
    public static Error NoCommentForLowRating(int minNoCommentRating) =>
        new(ErrorType.Validation,
            nameof(Feedback),
            $"Feedback comment is required for rating less than {minNoCommentRating}");
    
    public static Error CommentTooLong(int maxCommentLength) =>
        new(ErrorType.Validation,
            nameof(Feedback),
            $"Feedback comment cannot be longer than {maxCommentLength} characters");
}