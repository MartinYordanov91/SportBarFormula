using System.Globalization;

namespace SportBarFormula.Infastructure.Constants.DataConstants;

public class OrdersConstants
{
    public const string OrderTotalAmountPrecision = "decimal(18,2)";

    public const string OrdesTotalAmountStringFormat = "dd-MM-yyyy";
    //[RegularExpression(@"^\d{2}-\d{2}-\d{4}$")]
    //var isValid = DateTime.TryParseExact(model.TotalAmount, OrdesTotalAmountStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

}
