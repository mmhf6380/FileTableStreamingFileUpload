using System;
using System.Collections.Generic;
using System.Text;

namespace TestStreamingFile.DAL.Models
{
    public class FileResult
    {
        public List<string> FileNames { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public List<string> ContentTypes { get; set; }
        public int AddressPath { get; set; }
    }
}
