namespace SportBarFormula.Infrastructure.Constants.DataConstants;

public static class ReservationConstants
{
    public const string ReservationDateTimeStringFormat = "dd-MM-yyyy HH:mm";

    //[RegularExpression(@"^\d{2}-\d{2}-\d{4} \d{2}:\d{2}$")]
    //var isValid = DateTime.TryParseExact(model.ReservationDate, ReservationDateTimeStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

}
