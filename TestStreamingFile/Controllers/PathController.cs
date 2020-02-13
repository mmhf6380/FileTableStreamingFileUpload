using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestStreamingFile.DAL;
using TestStreamingFile.DAL.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestStreamingFile.Controllers
{
    [Route("api/Files/[controller]")]

    public class PathController : Controller
    {
        private readonly IFileRepository fileRepository;

        public PathController(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        [HttpPost("Add")]
        public IActionResult Post(string path)
        {
            var id = fileRepository.AddAddressPath(path);

            return Content(id.ToString());
        }
        [HttpPut("Active")]
        public IActionResult put(int id)
        {
            var addid = fileRepository.ActiveAddress(id);

            return Content(addid.ToString());
        }
    }
}
