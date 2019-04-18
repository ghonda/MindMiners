using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace MindMiners.Models
{
    public class FileInputModel
    {
        public IFormFile FileToUpload { get; set; }
        public string Offset { get; private set; }
    }
}