using Microsoft.AspNetCore.Mvc;
using ReenbitTest.Interfaces;
using ReenbitTest.Models;

namespace ReenbitTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IBlobService _fileService;

    public FileController(IBlobService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("/api/file")]
    public async Task<ActionResult> AddBlobMetadataAsync([FromForm] RequestData requestData)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage).ToList();
            return BadRequest(errors);
        }

        try
        {
            await _fileService.AddBlobMetadataAsync(requestData);

            return Ok();
        }
        catch
        {
            return StatusCode(500);
        }
    }
}
