using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestStreamingFile.Common;
using TestStreamingFile.DAL;
using TestStreamingFile.DAL.Models;

namespace TestStreamingFile.Controllers
{
    [Route("api/files")]
    public class FileUploadController : Controller
    {
        private readonly IFileRepository _fileRepository;
        private readonly IOptions<ApplicationConfiguration> _optionsApplicationConfiguration;

        public FileUploadController(IFileRepository fileRepository, IOptions<ApplicationConfiguration> o)
        {
            _fileRepository = fileRepository;
            _optionsApplicationConfiguration = o;
        }
        [Route("upload")]
        [HttpPost]
        [ServiceFilter(typeof(ValidateMimeMultipartContentFilter))]
        public async Task<IActionResult> UploadFiles(FileDescriptionShort fileDescriptionShort)
        {
            var names = new List<string>();
            var contentTypes = new List<string>();
            var addresspath = _fileRepository.GetActiveAddress();
            if (ModelState.IsValid)
            {
                foreach (var file in fileDescriptionShort.File)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                        contentTypes.Add(file.ContentType);

                        names.Add(fileName);

                        // Extension method update RC2 has removed this 
                        //await file.SaveAsAsync(Path.Combine(_optionsApplicationConfiguration.Value.ServerUploadFolder, fileName));
                        
                        await file.SaveAsAsync(Path.Combine(addresspath.UNCPath, fileName));

                    }
                }
            }

            var files = new DAL.Models.FileResult
            {
                FileNames = names,
                ContentTypes = contentTypes,
                Description = fileDescriptionShort.Description,
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
                AddressPath=addresspath.Id
            };

            _fileRepository.AddFileDescriptions(files);

            return RedirectToAction("ViewAllFiles", "FileClient");
        }

        [Route("download/{id}")]
        [HttpGet]
        public FileStreamResult Download(int id)
        {
            var fileDescription = _fileRepository.GetFileDescription(id);

            //var path = _optionsApplicationConfiguration.Value.ServerUploadFolder + "\\" + fileDescription.FileName;
            var tablepath = _fileRepository.GetAddressPath(fileDescription.AddressPath);
            var path = tablepath + "\\" + fileDescription.FileName;
            var stream = new FileStream(path, FileMode.Open);
            return File(stream, fileDescription.ContentType);
        }
    }
}
