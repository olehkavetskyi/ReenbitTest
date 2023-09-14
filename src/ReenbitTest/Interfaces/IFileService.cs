using ReenbitTest.Models;

namespace ReenbitTest.Interfaces;

public interface IBlobService
{
    Task AddBlobMetadataAsync(RequestData requestData);
}
