using System.Net;

namespace EmailNotificationFunc.Interfaces;

public interface IEmailNotificationFunctionService
{
    Task<HttpStatusCode> SendEmailNotificationAsync(string email, string name, Uri uri);

    string GenerateUriWithSasToken(Uri uri, string name);
}
