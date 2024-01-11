using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;

public record Feedback
{
    private const int MinRating = 1;
    private const int MaxRating = 5;
    private const int MinRatingWithNoComment = 4;
    private const int MaxCommentLength = 500;
    
    public FeedbackRating Rating { get; private set; }
    public string? Comment { get; private set; }
    
    public static Result<Feedback> Create(int ratingValue, string? comment)
    {
        if (ratingValue is < MinRating or > MaxRating)
        {
            return Result.Fail<Feedback>(FeedbackErrors
                .FeedbackRatingOutOfRange(MinRating, MaxRating));
        }

        if (ratingValue < MinRatingWithNoComment && 
            string.IsNullOrWhiteSpace(comment))
        {
            return Result.Fail<Feedback>(FeedbackErrors
                .NoCommentForLowRating(MinRatingWithNoComment));
        }
        
        if (comment?.Length > MaxCommentLength)
        {
            return Result.Fail<Feedback>(FeedbackErrors
                .CommentTooLong(MaxCommentLength));
        }
        
        return Result.Ok(new Feedback
        {
            Rating = (FeedbackRating)ratingValue,
            Comment = comment
        });
    }
}