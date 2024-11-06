namespace SportBarFormula.Infastructure.Constants.DataConstants;

public static class PaymentConstants
{
    public const string PaymentDateStringFormat = "dd-MM-yyyy";
    //[RegularExpression(@"^\d{2}-\d{2}-\d{4}$")]
    //var isValid = DateTime.TryParseExact(model.PaymentDate, PaymentDateStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

    
    public const string PaymantAmountPrecision = "decimal(18,2)";

    public const int PaymentMethodMinLength = 3; 
    public const int PaymentMethodMaxLength = 50; 

    public const int PaymentStatusMinLength = 3; 
    public const int PaymentStatusMaxLength = 50; 
}
