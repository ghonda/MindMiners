using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMiners.Domain.Interfaces;
using MindMiners.Models;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MindMiners.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISynchronizationApplication _synchronizationApplication;

        public HomeController(ISynchronizationApplication synchronizationApplication)
        {
            _synchronizationApplication = synchronizationApplication;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadAndDownloadFile(FileInputModel model)
        {
            if (model.FileToUpload is null || model.FileToUpload.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", model.FileToUpload.FileName);

            //using (var stream = new FileStream(path, FileMode.Create))
            //{
            //    await model.FileToUpload.CopyToAsync(stream);
            //    _synchronizationApplication.SubtitleSync(stream);
            //}

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, ".str", Path.GetFileName(path));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
