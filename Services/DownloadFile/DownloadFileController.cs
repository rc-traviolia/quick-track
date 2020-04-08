using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuickTrackWeb.Services.DownloadFile;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuickTrackWeb.Services.DownloadFile
{
 
    public class DownloadFileController : Controller
    {
        public IHostingEnvironment _hostingEnvironment;
        public DownloadFileService _pdfService;
        // GET: /<controller>/

        public DownloadFileController(IHostingEnvironment hostingEnvironment, DownloadFileService pdfService)
        {
            _hostingEnvironment = hostingEnvironment;
            _pdfService = pdfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public FileResult GenerateAndDownloadZip()
        {
            return null;
        }

        [HttpGet, Route("api/{name}")]
        public ActionResult Get(string name)
        {
            if(name == "tpdf")
            {
                var buffer = _pdfService.BuildSingleStudentReport("notarealusername@not.real");// Encoding.UTF8.GetBytes("Hello! Content is here.");
                var stream = new MemoryStream(buffer);
                //var stream = new FileStream(filename);

                var result = new FileStreamResult(stream, "application/pdf");
                result.FileDownloadName = "test.pdf";
                return result;
            }
            else
            {
                var buffer = Encoding.UTF8.GetBytes("Hello! Content is here.");
                var stream = new MemoryStream(buffer);
                //var stream = new FileStream(filename);

                var result = new FileStreamResult(stream, "text/plain");
                result.FileDownloadName = "test.txt";
                return result;
            }
            
        }
    }
}
