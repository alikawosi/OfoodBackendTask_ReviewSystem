using System;
namespace OFoodBackendTask.Controller.Helpers
{
	public class AggregatedRating
{
    public double? Rating { get; set; }
    public int? CommentsCount { get; set; }
    public int? RatesCount { get; set; }
    public int? RatingOneCount { get; set; }
    public int? RatingTwoCount { get; set; }
    public int? RatingThreeCount { get; set; }
    public int? RatingFourCount { get; set; }
    public int? RatingFiveCount { get; set; }
}
}
