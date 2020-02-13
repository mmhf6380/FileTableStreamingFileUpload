using System;
using System.Collections.Generic;
using System.Text;
using TestStreamingFile.DAL.Models;

namespace TestStreamingFile.DAL
{
    public interface IFileRepository
    {
        int ActiveAddress(int id);
        int AddAddressPath(string path);
        IEnumerable<FileDescriptionShort> AddFileDescriptions(FileResult fileResult);
        Address GetActiveAddress();
        string GetAddressPath(int id);
        IEnumerable<FileDescriptionShort> GetAllFiles();

        FileDescription GetFileDescription(int id);
    }
}
