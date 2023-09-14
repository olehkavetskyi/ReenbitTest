using Azure.Storage;
using Azure.Storage.Sas;
using EmailNotificationFunc.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace EmailNotificationFunc.Services;

public class EmailNotificationFunctionService : IEmailNotificationFunctionService
{
    public async Task<HttpStatusCode> SendEmailNotificationAsync(string email, string name, Uri uri)
    {
        var key = Environment.GetEnvironmentVariable("SendGridAPIKey");
        var senderEmail = Environment.GetEnvironmentVariable("SenderEmail");
        var sasUrl = GenerateUriWithSasToken(uri, name);
        var dynamicTemplateData = new Dictionary<string, string>
        {
            { "sasUrl", sasUrl }
        };
        var templateId = Environment.GetEnvironmentVariable("TemplateId");
        var from = new EmailAddress(senderEmail);
        var to = new EmailAddress(email);
        var client = new SendGridClient(key);
        var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);
      
        var response = await client.SendEmailAsync(msg);

        return response.StatusCode;
    }

    public string GenerateUriWithSasToken(Uri uri, string name)
    {
        string blobContainerName = Environment.GetEnvironmentVariable("BlobContainerName")!;
        string accountName = Environment.GetEnvironmentVariable("AccountName")!;
        string accountKey = Environment.GetEnvironmentVariable("AccountKey")!;

        BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = blobContainerName,
            BlobName = name,
            ExpiresOn = DateTime.UtcNow.AddHours(1)
        };

        blobSasBuilder.SetPermissions(BlobSasPermissions.Read);

        var storageCredential = new StorageSharedKeyCredential(accountName, accountKey);
        var sasToken = blobSasBuilder.ToSasQueryParameters(storageCredential).ToString();

        return $"{uri}?{sasToken}";
    }
}
