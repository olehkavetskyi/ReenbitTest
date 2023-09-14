using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ReenbitTest.Controllers;
using ReenbitTest.Models;
using ReenbitTest.Services;
using ReenbitTest.Tests.Helpers;

namespace ReenbitTest.Tests.ControllerTests;

public class FileControllerTests
{

    [Fact]
    public async Task AddBlobMetadataAsync_InvalidModelState_ReturnsBadRequestWithErrors()
    {
        var requestData = new RequestData { Email = "123", File = null! };
        var controller = new FileController(null!);

         controller.ModelState.AddModelError("Email", "IsInvalid");

        var result = await controller.AddBlobMetadataAsync(requestData) as BadRequestObjectResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);

        var errors = result.Value as List<string>;

        Assert.NotNull(errors);
        Assert.NotEmpty(errors);
    }
}
