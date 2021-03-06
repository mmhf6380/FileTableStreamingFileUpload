﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TestStreamingFile.DAL.Models
{
    public class FileDescription
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string ContentType { get; set; }
        public int AddressPath { get; set; }
    }
}
