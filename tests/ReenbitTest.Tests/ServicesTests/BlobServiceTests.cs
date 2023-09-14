using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using ReenbitTest.Services;

namespace ReenbitTest.Tests.ServicesTests;

public class BlobServiceTests
{

    [Fact]
    public async Task GetStream_WhenCalled_ReturnsStream()
    {
        var fileService = new BlobService(Mock.Of<IConfiguration>());

        var mockFormFile = new Mock<IFormFile>();
        var fileContent = new MemoryStream();
        mockFormFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default))
                    .Callback<Stream, CancellationToken>((stream, token) =>
                    {
                        fileContent.CopyTo(stream);
                    })
                    .Returns(Task.CompletedTask);
        mockFormFile.Setup(f => f.OpenReadStream()).Returns(() => fileContent);
        mockFormFile.Setup(f => f.Length).Returns(() => fileContent.Length);

        var stream = await GetStream(mockFormFile.Object);

        Assert.NotNull(stream);
        Assert.Equal(fileContent.Length, stream.Length);
    }

    private async Task<Stream> GetStream(IFormFile formFile)
    {
        var memoryStream = new MemoryStream();

        await formFile.CopyToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}
