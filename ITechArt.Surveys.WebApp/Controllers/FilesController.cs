using System;
using System.IO;
using System.Threading.Tasks;
using iTechArt.Surveys.Foundation.Services.Files;
using iTechArt.Surveys.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using File = iTechArt.Surveys.Foundation.Models.File;
using FileInfo = iTechArt.Surveys.DomainModel.FileInfo;

namespace iTechArt.Surveys.WebApp.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        private readonly IFileService _fileService;
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        
        public FilesController(IFileService fileService, FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileService = fileService;
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }

        
        public async Task<IActionResult> Upload(IFormFile fileModel)
        {
            var fileStream = fileModel.OpenReadStream();

            var file = new File()
            {
                Data = fileStream,
                Info = new FileInfo()
                {
                    Id = Guid.NewGuid(),
                    Name = fileModel.FileName
                }
            };
            
            var getContentResult = _fileExtensionContentTypeProvider
                .TryGetContentType(fileModel.FileName, out var mime);

            if (!getContentResult)
            {
                return BadRequest(new {message = "Content type not found"});
            }
            
            file.Info.ContentType = mime;
            
            await _fileService.UploadAsync(file);
            

            return Json(file.Info);
        }

        public async Task<IActionResult> Load(Guid id)
        {
            var file = await _fileService.LoadAsync(id);

            if (file == null)
            {
                return NotFound();
            }
            
            return File(file.Data, file.Info.ContentType, file.Info.Name);
        }
    }
}