using Microsoft.AspNetCore.Http;
namespace MaktabGram.Domain.FileAgg
{
    public interface IFileService
    {
        public string Upload(IFormFile file, string folder);
    }
}
