using System.Globalization;

namespace SportBarFormula.Infrastructure.Constants.DataConstants;

public static class OrderConstants
{
    public const string OrderTotalAmountPrecision = "decimal(18,2)";

    public const string OrderDateStringFormat = "dd-MM-yyyy";
    //[RegularExpression(@"^\d{2}-\d{2}-\d{4}$")]
    //var isValid = DateTime.TryParseExact(model.OrderDate, OrderDateStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

}
