using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Errors;

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