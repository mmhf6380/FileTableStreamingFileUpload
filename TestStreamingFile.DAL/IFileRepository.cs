using System;
using System.Collections.Generic;
using System.Text;
using TestStreamingFile.DAL.Models;

namespace TestStreamingFile.DAL
{
    public interface IFileRepository
    {
        IEnumerable<FileDescriptionShort> AddFileDescriptions(FileResult fileResult);

        IEnumerable<FileDescriptionShort> GetAllFiles();

        FileDescription GetFileDescription(int id);
    }
}
