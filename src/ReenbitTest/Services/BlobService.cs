using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using ReenbitTest.Interfaces;
using ReenbitTest.Models;

namespace ReenbitTest.Services;

public class BlobService : IBlobService
{
    private readonly IConfiguration _config;

    public BlobService(IConfiguration config)
    {
        _config = config;
    }

    public async Task AddBlobMetadataAsync(RequestData requestData)
    {
        var connectionString = _config.GetConnectionString("BlobStorageConnectionString");
        var containerName = _config.GetValue<string>("BlobStorage:ContainerName");
        var fileName = $"{Guid.NewGuid()}.docx";

        BlobClient blobClient = new BlobClient(connectionString, containerName, fileName);

        BlobUploadOptions options = new BlobUploadOptions
        {
            Metadata = new Dictionary<string, string>
            {
                { "email", requestData.Email }
            }
        };

        await blobClient.UploadAsync(await GetStream(requestData.File), options);
    }

    private async Task<Stream> GetStream(IFormFile formFile)
    {
        var memoryStream = new MemoryStream();

        await formFile.CopyToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}
