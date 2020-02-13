using System;
using System.Collections.Generic;
using System.Text;

namespace TestStreamingFile.DAL.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string UNCPath { get; set; }
        public bool IsActive { get; set; }
    }
}
