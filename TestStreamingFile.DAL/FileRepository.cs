using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStreamingFile.DAL.Models;

namespace TestStreamingFile.DAL
{
    public class FileRepository : IFileRepository
    {

        private readonly FileContext _context;
        private readonly ILogger _logger;

        public FileRepository(FileContext context, ILoggerFactory loggerFactory )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("FileRepository");
        }

        public IEnumerable<FileDescriptionShort> AddFileDescriptions(FileResult fileResult)
        {
            List<string> filenames = new List<string>();
            for (int i = 0; i < fileResult.FileNames.Count(); i++)
            {


                int index = fileResult.FileNames[i].LastIndexOf("\\");
                var shortName = fileResult.FileNames[i].Substring(index + 1);

                var fileDescription = new FileDescription
                {
                    ContentType = fileResult.ContentTypes[i],
                    FileName = shortName,
                    CreatedTimestamp = fileResult.CreatedTimestamp,
                    UpdatedTimestamp = fileResult.UpdatedTimestamp,
                    Description = fileResult.Description,
                    AddressPath=fileResult.AddressPath
                };

                filenames.Add(fileResult.FileNames[i]);
                _context.FileDescriptions.Add(fileDescription);
            }

            _context.SaveChanges();
            return GetNewFiles(filenames);
        }

        private IEnumerable<FileDescriptionShort> GetNewFiles(List<string> filenames)
        {
            IEnumerable<FileDescription> x = _context.FileDescriptions.Where(r => filenames.Contains(r.FileName));
            return x.Select(t => new FileDescriptionShort { Id = t.Id, Description = t.Description });
        }

        public IEnumerable<FileDescriptionShort> GetAllFiles()
        {
            return _context.FileDescriptions.Select(
                    t => new FileDescriptionShort { Name = t.FileName, Id = t.Id, Description = t.Description });
        }

        public FileDescription GetFileDescription(int id)
        {
            return _context.FileDescriptions.Single(t => t.Id == id);
        }

        public string GetAddressPath(int id)
        {
            return _context.Addresses.Find(id).UNCPath.ToString();
        }
        public Address GetActiveAddress()
        {
            return _context.Addresses.Where(x => x.IsActive == true).FirstOrDefault();
        }
        public int AddAddressPath(string path)
        {
            Address address = new Address
            {
                UNCPath = path,
                IsActive = false
            };
            _context.Addresses.Add(address);
            _context.SaveChanges();
            return address.Id;
        }
        public int ActiveAddress(int id)
        {
            var address = _context.Addresses.Find(id);
            address.IsActive = true;
            DeActiveOtherAddress(id);
            _context.SaveChanges();
            return address.Id;
        }
        private void DeActiveOtherAddress(int id)
        {
            var addresses = _context.Addresses.ToList();
            for (int i =0; i < addresses.Count; i++)
            {
                if (addresses[i].Id==id)
                {
                    continue;
                }
                addresses[i].IsActive = false;
            }
        }
    }
}
