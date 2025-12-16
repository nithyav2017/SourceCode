using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tesseract;
using PdfiumViewer;


namespace PDFToTextConverter.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IFormFile File { get; set; }
        private static string extractedText = string.Empty;
        private readonly ILogger<IndexModel> _logger;

        public IActionResult OnPost()
        {
            if (File != null && File.Length > 0)
            {
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFiles", File.FileName);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    File.CopyTo(stream);
                }
                // Extract text using Tesseract OCR
                ConvertPdfToImages(filepath);
                extractedText = ExtractTextFromPdf(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PDFImage"));

                return new ContentResult { Content = $"<h3>File uploaded successfully!</h3><p>File Name: {File.FileName}</p>", ContentType = "text/html" };
            }
            return new ContentResult { Content = "<h3>Error: Please select a valid PDF file to upload.</h3>", ContentType = "text/html" };
        }
        public IActionResult OnPostDownload()
        {
            if (!string.IsNullOrEmpty(extractedText))
            {

                var byteArray = System.Text.Encoding.UTF8.GetBytes(extractedText);
                return File(byteArray, "text/plain", "ExtractedText.txt" );
                
            }
            return new ContentResult { Content = "No text avaiable to download." };
        }

        private string ExtractTextFromPdf(string imagepath)
        {
            string text = string.Empty;
            string tessPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "tessdata");

            Console.WriteLine("Tesseract path: " + tessPath);

            using (var engine = new TesseractEngine(tessPath, "eng", EngineMode.Default))
            {
                string[] imageFiles = Directory.GetFiles(imagepath);
                foreach (string imagefile in imageFiles)
                {
                    using (var img = Pix.LoadFromFile(imagefile))
                    {
                        using (var page = engine.Process(img))
                        {
                            if (string.IsNullOrEmpty(text))
                                text = page.GetText();
                            else
                                text += Environment.NewLine + page.GetText();
                        }
                    }
                     System.IO.File.Delete(imagefile);
                }
            }
            Directory.Delete(imagepath);
            return text;
        }

        private void ConvertPdfToImages(string pdfPath)
        {
            string imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","PDFImage");
            if(!Directory.Exists(imageDirectory)) 
                Directory.CreateDirectory(imageDirectory);

            using (var document = PdfDocument.Load(pdfPath))
            {
                for (int i = 0; i < document.PageCount; i++)
                {
                    using (var image = document.Render(i, 300, 300, PdfRenderFlags.CorrectFromDpi))
                    {
                        string outputPath = Path.Combine(imageDirectory, $"Page-{i + 1}.png");
                        image.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return null;
        }
    }
}
