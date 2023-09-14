using ReenbitTest.Models;
using System.ComponentModel.DataAnnotations;

namespace ReenbitTest.Validators;

public class RequestDataAttribute : ValidationAttribute
{
    public RequestDataAttribute()
    {
        ErrorMessage = "File is invalid";
    }

    public override bool IsValid(object? value)
    {
        RequestData? data = value as RequestData;

        return data != null && Path.GetExtension(data.File.FileName) == ".docx";
    }
}
