using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Errors;

public static class FeedbackErrors
{
    public static Error FeedbackRatingOutOfRange(int minRating, int maxRating) =>
        new(ErrorType.Validation,
            $"Feedback rating must be between {minRating} and {maxRating}");
    
    public static Error NoCommentForLowRating(int minNoCommentRating) =>
        new(ErrorType.Validation,
            $"Feedback comment is required for rating less than {minNoCommentRating}");
    
    public static Error CommentTooLong(int maxCommentLength) =>
        new(ErrorType.Validation,
            $"Feedback comment cannot be longer than {maxCommentLength} characters");
}