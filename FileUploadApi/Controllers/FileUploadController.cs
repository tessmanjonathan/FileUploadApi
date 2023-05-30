using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System.IO;

namespace FileUploadApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await _fileUploadService.UploadFile(file);

            if (result == "No file uploaded.")
                return BadRequest(result);

            return Ok(result);
        }
    }
}
