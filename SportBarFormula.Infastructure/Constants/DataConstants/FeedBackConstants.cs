namespace SportBarFormula.Infastructure.Constants.DataConstants;

public static class FeedBackConstants
{
    public const string FeedBackRatingMin = "1";
    public const string FeedBackRatingMax = "5";
    //[Range(typeof(int), FeedBackRatingMin, FeedBackRatingMax)]

    public const string FeedBackCreatedDateStringFormat = "dd-MM-yyyy HH:mm";

    //[RegularExpression(@"^\d{2}-\d{2}-\d{4} \d{2}:\d{2}$")]
    //var isValid = DateTime.TryParseExact(model.CreatedDate, FeedBackCreatedDateStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);
}
