//using Microsoft.AspNetCore.Mvc;
//using st10293982CLD6212POE1.Models;
//using st10293982CLD6212POE1.Services;

//using Microsoft.AspNetCore.Mvc;
//using st10293982CLD6212POE1.Models;
//using st10293982CLD6212POE1.Services;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.IO;

//namespace st10293982CLD6212POE1.Controllers
//{
//    public class FilesController : Controller
//    {
//        private readonly FileShareService _fileShareService;

//        public FilesController(FileShareService fileShareService)
//        {
//            _fileShareService = fileShareService;
//        }

//        public async Task<IActionResult> Index()
//        {
//            List<FileModel> files;
//            try
//            {
//                files = await _fileShareService.ListFilesAsync("uploads");
//            }
//            catch (Exception ex)
//            {
//                ViewBag.Message = $"Failed to load files: {ex.Message}";
//                files = new List<FileModel>();
//            }

//            return View(files);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Upload(IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//            {
//                ModelState.AddModelError("file", "Please select a file to upload.");
//                return await Index();
//            }

//            try
//            {
//                using (var stream = file.OpenReadStream())
//                {
//                    string directoryName = "uploads";
//                    string fileName = file.FileName;

//                    await _fileShareService.UploadFileAsync(directoryName, fileName, stream);
//                }

//                TempData["Message"] = $"File '{file.FileName}' uploaded successfully!";
//            }
//            catch (Exception ex)
//            {
//                TempData["Message"] = $"File upload failed: {ex.Message}";
//            }

//            return RedirectToAction("Index");
//        }

//        // Handle file download
//        [HttpGet]
//        public async Task<IActionResult> DownloadFile(string fileName)
//        {
//            if (string.IsNullOrEmpty(fileName))
//            {
//                return BadRequest("File name cannot be null or empty.");
//            }

//            try
//            {
//                var fileStream = await _fileShareService.DownloadFileAsync("uploads", fileName);

//                if (fileStream == null)
//                {
//                    return NotFound($"File '{fileName}' not found.");
//                }

//                return File(fileStream, "application/octet-stream", fileName);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error downloading file: {ex.Message}");
//            }
//        }
//    }
//}




//namespace st10293982CLD6212POE1.Controllers
//{
//    public class FilesController : Controller
//    {
//        private readonly HttpClient _httpClient;

//        public FilesController(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<IActionResult> Index()
//        {
//            // List files logic can be added here if needed
//            return View(); // Assuming you have a view for listing files
//        }

//        [HttpPost]
//        public async Task<IActionResult> Upload(IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//            {
//                ModelState.AddModelError("file", "Please select a file to upload.");
//                return await Index();
//            }

//            try
//            {
//                using (var stream = file.OpenReadStream())
//                {
//                    string url = "https://st10293982function.azurewebsites.net/api/fileshare/upload?code=dbtfFzu-sKtCNJM9YtP-ix1MYr--7IoaPIfKhjBTPf0ZAzFuvTGVtw%3D%3D";
//                    var content = new MultipartFormDataContent();
//                    content.Add(new StreamContent(stream), "file", file.FileName);

//                    var response = await _httpClient.PostAsync(url, content);

//                    if (response.IsSuccessStatusCode)
//                    {
//                        TempData["Message"] = $"File '{file.FileName}' uploaded successfully!";
//                    }
//                    else
//                    {
//                        TempData["Message"] = $"File upload failed: {response.ReasonPhrase}";
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                TempData["Message"] = $"File upload failed: {ex.Message}";
//            }

//            return RedirectToAction("Index");
//        }

//        // Handle file download
//        [HttpGet]
//        public async Task<IActionResult> DownloadFile(string fileName)
//        {
//            if (string.IsNullOrEmpty(fileName))
//            {
//                return BadRequest("File name cannot be null or empty.");
//            }

//            // Logic for downloading files from the file share can be added here

//            return NotFound(); // Placeholder
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace st10293982CLD6212POE1.Controllers
{
    public class FilesController : Controller
    {
        private readonly HttpClient _httpClient;

        public FilesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Display the list of files (if needed)
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // Upload a file to Blob storage
        [HttpPost]
        public async Task<IActionResult> UploadToBlob(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "Please select a file to upload.");
                return View("Index");
            }

            var formData = new MultipartFormDataContent();
            using (var stream = file.OpenReadStream())
            {
                var streamContent = new StreamContent(stream);
                formData.Add(streamContent, "file", file.FileName);

                var response = await _httpClient.PostAsync("https://st10293982function.azurewebsites.net/api/upload?code=inbUL0X0HMz6QjDurBWDAiuiIMUEZbWxicsMIc1e5YRnAzFu0TwZ_A%3D%3D", formData);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = $"File '{file.FileName}' uploaded to blob storage.";
                }
                else
                {
                    TempData["Message"] = $"File upload failed: {response.ReasonPhrase}";
                }
            }

            return RedirectToAction("Index");
        }

        // Upload a file to File Share
        [HttpPost]
        public async Task<IActionResult> UploadToFileShare(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "Please select a file to upload.");
                return View("Index");
            }

            var formData = new MultipartFormDataContent();
            using (var stream = file.OpenReadStream())
            {
                var streamContent = new StreamContent(stream);
                formData.Add(streamContent, "file", file.FileName);

                var response = await _httpClient.PostAsync("https://st10293982function.azurewebsites.net/api/fileshare/upload?code=dbtfFzu-sKtCNJM9YtP-ix1MYr--7IoaPIfKhjBTPf0ZAzFuvTGVtw%3D%3D", formData);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = $"File '{file.FileName}' uploaded to file share.";
                }
                else
                {
                    TempData["Message"] = $"File upload failed: {response.ReasonPhrase}";
                }
            }

            return RedirectToAction("Index");
        }

        // Download a file from Blob storage
        [HttpGet]
        public async Task<IActionResult> DownloadFromBlob(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("File name cannot be null or empty.");
            }

            var response = await _httpClient.GetAsync($"https://st10293982function.azurewebsites.net/api/download/{fileName}?code=YIMa8Y7cg_RO8HSWEhRl2PsHELpyZdZKyiFzeAsheVLqAzFuo_xOEw%3D%3D");

            if (response.IsSuccessStatusCode)
            {
                var fileStream = await response.Content.ReadAsStreamAsync();
                return File(fileStream, "application/octet-stream", fileName);
            }

            return NotFound($"File '{fileName}' not found.");
        }
    }
}

