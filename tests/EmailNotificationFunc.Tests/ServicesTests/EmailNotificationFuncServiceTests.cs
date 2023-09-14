using EmailNotificationFunc.Services;
using System.Net;

namespace EmailNotificationFunc.Tests.ServicesTests;

public class EmailNotificationFuncServiceTests
{

    [Fact]
    public void GenerateUriWithSasToken_ReturnsValidUriWithSasToken()
    {
        var service = new EmailNotificationFunctionService();
        var uri = new Uri("https://example.blob.core.windows.net/container/blob.txt");
        var name = "blob.txt";

        Environment.SetEnvironmentVariable("BlobContainerName", "uploaded-files");
        Environment.SetEnvironmentVariable("AccountName", "test_account_name");
        Environment.SetEnvironmentVariable("AccountKey", "MkSeJ4pp8xcQfG/8A1S12YdEWZzJ2EI+jQoIBxNcb5UGnzUt3hg/t6PdQi8QO9ommNwZngZah/hS+AStkD5m9Q==");

        var result = service.GenerateUriWithSasToken(uri, name);

        Assert.NotNull(result);
        Assert.Contains("?", result); 
    }

    [Fact]
    public async Task SendEmailNotificationAsync_ResultIsUnuauthorizedWithWrongAccountKey()
    {
        var service = new EmailNotificationFunctionService();
        var email = "test@example.com";
        var uri = new Uri("https://example.blob.core.windows.net/container/blob.txt");
        var name = "blob.txt";

        Environment.SetEnvironmentVariable("BlobContainerName", "uploaded-files");
        Environment.SetEnvironmentVariable("AccountName", "your_account_name");
        Environment.SetEnvironmentVariable("AccountKey", "MkSeJ4pp8xcQfG/8A1S12YdEWZzJ2EI+jQoIBxNcb5UGnzUt3hg/t6PdQi8QO9ommNwZngZah/hS+AStkD5m9Q==");
        Environment.SetEnvironmentVariable("SendGridAPIKey", "TEST.1m3uM4c7Sw6jvshKfHPgaw.wt5RaTSizmuyw5DrMj8JjgdckTcIRouo48vjr14gyFU");
        Environment.SetEnvironmentVariable("TemplateId", "testtemplate_id");


        var res = await service.SendEmailNotificationAsync(email, name, uri);

        Assert.Same(res.ToString(), HttpStatusCode.Unauthorized.ToString());
    }
}
