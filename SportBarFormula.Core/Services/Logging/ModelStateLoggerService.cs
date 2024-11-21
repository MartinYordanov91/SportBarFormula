using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace SportBarFormula.Core.Services.Logging;

public class ModelStateLoggerServiceILogger(ILogger<IModelStateLoggerService> logger) : IModelStateLoggerService
{
    private readonly ILogger<IModelStateLoggerService> _logger = logger;


    public void LogModelErrors(ModelStateDictionary modelState)
    {
        foreach (var key in modelState.Keys)
        {
            var errors = modelState[key]!.Errors;
            foreach (var error in errors)
            {
                _logger.LogError($"Key: {key}, Error: {error.ErrorMessage}");
            }
        }
    }
}
