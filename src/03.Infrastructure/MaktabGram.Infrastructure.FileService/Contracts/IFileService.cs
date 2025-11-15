using Microsoft.AspNetCore.Http;
namespace MaktabGram.Infrastructure.FileService.Contracts
{
    public interface IFileService
    {
        public string Upload(IFormFile file, string folder);
        public void Delete(string fileName);
    }
}
