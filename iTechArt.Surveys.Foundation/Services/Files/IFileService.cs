using System;
using System.Threading.Tasks;
using iTechArt.Surveys.Foundation.Models;

namespace iTechArt.Surveys.Foundation.Services.Files
{
    public interface IFileService
    {
        Task UploadAsync(File file);

        Task<File> LoadAsync(Guid id);
    }
}