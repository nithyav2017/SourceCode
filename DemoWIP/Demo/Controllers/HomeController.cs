using System.Diagnostics;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Products()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(IFormFile file )
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension.Rows;
                        var colCount = worksheet.Dimension.Columns;
                        var excelData = new List<Dictionary<string, object>>();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var rowData = new Dictionary<string, object>();
                            for (int col = 1; col <= colCount; col++)
                            {
                                rowData.Add(worksheet.Cells[1, col].Text, worksheet.Cells[row, col].Text);
                            }
                            excelData.Add(rowData);
                        }

                        return Json(excelData);

                    }

                }
            }
            return Json(new { success = false, message = "File upload failed." });
        }

          
            
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
