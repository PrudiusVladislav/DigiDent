using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;

/// <summary>
/// Represents feedback value object. Specifies the rating and comment for the visit.
/// </summary>
public record Feedback
{
    public FeedbackRating Rating { get; init; }
    public string? Comment { get; init; }
    
    // for EF Core
    internal Feedback() { }

    internal Feedback(int rating, string? comment)
    {
        Rating = (FeedbackRating)rating;
        Comment = comment;
    }
    
    public static Result<Feedback> Create(int ratingValue, string? comment)
    { 
        const int minRating = 1;
        const int maxRating = 5;
        const int minRatingWithNoComment = 4;
        const int maxCommentLength = 500;
        
        if (ratingValue is < minRating or > maxRating)
        {
            return Result.Fail<Feedback>(FeedbackErrors
                .FeedbackRatingOutOfRange(minRating, maxRating));
        }

        if (ratingValue < minRatingWithNoComment && 
            string.IsNullOrWhiteSpace(comment))
        {
            return Result.Fail<Feedback>(FeedbackErrors
                .NoCommentForLowRating(minRatingWithNoComment));
        }
        
        if (comment?.Length > maxCommentLength)
        {
            return Result.Fail<Feedback>(FeedbackErrors
                .CommentTooLong(maxCommentLength));
        }
        
        return Result.Ok(new Feedback(ratingValue, comment));
    }
}