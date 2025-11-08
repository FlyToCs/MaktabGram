using Microsoft.AspNetCore.Http;
namespace MaktabGram.Domain.Core.FileAgg
{
    public interface IFileService
    {
        public string Upload(IFormFile file, string folder);
        public void Delete(string fileName);
    }
}
