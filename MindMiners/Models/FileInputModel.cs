using Microsoft.AspNetCore.Http;

namespace MindMiners.Models
{
    public class FileInputModel
    {
        public IFormFile FileToUpload { get; set; }
        public double Offset { get; set; }
    }
}