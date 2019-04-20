using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MindMiners.CrossCutting.Infrastructure.Utils;
using MindMiners.Domain.Interfaces;
using MindMiners.Models;
using System;
using System.Linq;

namespace MindMiners.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHistoryApplication _historyApplication;
        private readonly ISynchronizationApplication _synchronizationApplication;
        private readonly AbstractValidator<FileInputModel> _validations;

        public HomeController(IHistoryApplication historyApplication, ISynchronizationApplication synchronizationApplication, AbstractValidator<FileInputModel> validations)
        {
            _historyApplication = historyApplication;
            _synchronizationApplication = synchronizationApplication;
            _validations = validations;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            var historyList = _historyApplication.GetFileHistory();
            return View(historyList);
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
                var newFileBytes = _synchronizationApplication.SubtitleSync(model.FileToUpload.OpenReadStream(), model.FileToUpload.FileName, offSet);
                return File(newFileBytes, "application/x-msdownload", model.FileToUpload.FileName);
            }
            catch (Exception e)
            {
                return ReturnError(e.Message);
            }
        }

        [HttpGet]
        public IActionResult DownloadFile(int id)
        {
            ViewBag.Error = string.Empty;
            try
            {
                var (fileBytes, name) = _historyApplication.GetFile(id);
                return File(fileBytes, "application/x-msdownload", name);
            }
            catch (Exception e)
            {
                return ReturnError(e.Message);
            }
        }

        public IActionResult RemoveFile(int id)
        {
            ViewBag.Error = string.Empty;
            try
            {
                _historyApplication.RemoveFile(id);
                return RedirectToAction("History");
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
