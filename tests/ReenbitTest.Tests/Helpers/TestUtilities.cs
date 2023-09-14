using Microsoft.AspNetCore.Http;

namespace ReenbitTest.Tests.Helpers;

internal static class TestUtilities
{
    public static IFormFile CreateTestFile(string name)
    {
        var content = "Hello World from a Fake File";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        return new FormFile(stream, 0, stream.Length, "file", name);
    }
}
