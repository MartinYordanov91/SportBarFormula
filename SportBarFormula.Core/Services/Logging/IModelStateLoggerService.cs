using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportBarFormula.Core.Services.Logging;

public interface IModelStateLoggerService
{
    void LogModelErrors(ModelStateDictionary modelState);
}
