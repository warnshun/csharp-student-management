using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var stautsCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，你訪問的頁面不存在！";

                    _logger.LogWarning($"發生了一個 404錯誤 {stautsCodeResult.OriginalPath}, 以及查詢語句 = {stautsCodeResult.OriginalQueryString}");

                    //ViewBag.Path = stautsCodeResult.OriginalPath;
                    //ViewBag.QueryString = stautsCodeResult.OriginalQueryString;
                    break;
            }

            return View("NotFound");
        }

        [AllowAnonymousAttribute]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            _logger.LogError($"路徑{exceptionHandlerPathFeature.Path}, 產生了一個錯誤{exceptionHandlerPathFeature.Error}");

            //ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            //ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            //ViewBag.ExceptionStackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            return View("Error");
        }
    }
}
