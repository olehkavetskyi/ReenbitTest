using ReenbitTest.Validators;
using System.ComponentModel.DataAnnotations;

namespace ReenbitTest.Models;

[RequestData]
public class RequestData
{
    public IFormFile File { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
}