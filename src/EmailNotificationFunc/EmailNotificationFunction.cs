using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using EmailNotificationFunc.Interfaces;

namespace EmailNotificationFunc;

public class EmailNotificationFunction
{
    private readonly ILogger _logger;
    private readonly IEmailNotificationFunctionService _emailNotificationFuncService;

    public EmailNotificationFunction(ILoggerFactory loggerFactory, IEmailNotificationFunctionService emailNotificationFuncService)
    {
        _logger = loggerFactory.CreateLogger<EmailNotificationFunction>();
        _emailNotificationFuncService = emailNotificationFuncService;
    }

    [Function("EmailNotificationFunc")]
    public async Task RunAsync(
        [BlobTrigger("uploaded-files/{name}", Connection = "BlobStorageConnectionString")] string myBlob,
        IDictionary<string, string> metaData, 
        string name,
        Uri uri)
    { 
        try
        {
            if (metaData.ContainsKey("email"))
            {
                await _emailNotificationFuncService.SendEmailNotificationAsync(metaData["email"], name, uri);
            }
            else
            {
                _logger.LogError($"A file {name} doesn't have metadata");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sending email: {ex.Message}");
        }
    }
}
