using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MindMiners.CrossCutting.Infrastructure.Utils;
using MindMiners.Domain.Interfaces;
using MindMiners.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMiners.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISynchronizationApplication _synchronizationApplication;
        private readonly AbstractValidator<FileInputModel> _validations;

        public HomeController(ISynchronizationApplication synchronizationApplication, AbstractValidator<FileInputModel> validations)
        {
            _synchronizationApplication = synchronizationApplication;
            _validations = validations;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadAndDownloadFile([Bind("FileToUpload,Offset")]FileInputModel model)
        {
            ViewBag.Error = string.Empty;
            try
            {
                var validationResult = _validations.Validate(model);
                if (!validationResult.IsValid)
                    return ReturnError(validationResult.Errors.FirstOrDefault().ErrorMessage);

                var offSet = Helper.ConvertStringToDouble(model.Offset);
                var newFileBytes = _synchronizationApplication.SubtitleSync(model.FileToUpload.OpenReadStream(),model.FileToUpload.FileName,  offSet);
                return File(newFileBytes, "application/x-msdownload", model.FileToUpload.FileName);
            }
            catch (Exception e)
            {
                return ReturnError(e.Message);
            }
        }

        private IActionResult ReturnError(string errorMessage)
        {
            ViewBag.Error = errorMessage;
            return View("Index");
        }
    }
}
