using Microsoft.AspNetCore.Mvc;
using st10293982CLD6212POE1.Models;
using st10293982CLD6212POE1.Services;

namespace st10293982CLD6212POE1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BlobService _blobService;
        private readonly TableStorageService _tableStorageService;

        public ProductsController(BlobService blobService, TableStorageService tableStorageService)
        {
            _blobService = blobService;
            _tableStorageService = tableStorageService;
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, IFormFile file)
        {
            if (file != null)
            {
                using var stream = file.OpenReadStream();
                var image = await _blobService.UploadAsync(stream, file.FileName);
                product.Image = image;
            }

            if (ModelState.IsValid)
            {
                product.PartitionKey = "ProductsPartition";
                product.RowKey = Guid.NewGuid().ToString();
                await _tableStorageService.AddProductAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string partitionKey, string rowKey, Product product)
        {

            if (product != null && !string.IsNullOrEmpty(product.Image))
            {
                // Delete the associated blob image
                await _blobService.DeleteBlobAsync(product.Image);
            }
            //Delete Table entity
            await _tableStorageService.DeleteProductAsync(partitionKey, rowKey);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var product = await _tableStorageService.GetAllProductsAsync();
            return View(product);
        }
        
    }
}
