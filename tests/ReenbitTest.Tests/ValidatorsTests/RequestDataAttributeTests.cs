using Microsoft.AspNetCore.Http;
using ReenbitTest.Models;
using ReenbitTest.Tests.Helpers;
using ReenbitTest.Validators;

namespace ReenbitTest.Tests.ValidatorsTests;

public class RequestDataAttributeTests
{
    [Fact]
    public void IsValid_WithValidFileExtension_ReturnsTrue()
    {
        var attribute = new RequestDataAttribute();

        IFormFile file = TestUtilities.CreateTestFile("test.docx");
        var requestData = new RequestData { Email = "test@example.com", File = file };

        var result = attribute.IsValid(requestData);

        Assert.True(result);
    }

    [Fact]
    public void IsValid_WithInvalidFileExtension_ReturnsFalse()
    {
        var attribute = new RequestDataAttribute();

        IFormFile file = TestUtilities.CreateTestFile("test.pdf");
        var requestData = new RequestData { Email = "test@example.com", File = file };

        var result = attribute.IsValid(requestData);

        Assert.False(result);
    }

    [Fact]
    public void IsValid_WithNullValue_ReturnsFalse()
    {
        var attribute = new RequestDataAttribute();

        var result = attribute.IsValid(null);

        Assert.False(result);
    }
}
