//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Documents;
//using st10293982CLD6212POE1.Models;
//using st10293982CLD6212POE1.Services;

//namespace st10293982CLD6212POE1.Controllers
//{
//    public class OrdersController : Controller
//    {
//        private readonly TableStorageService _tableStorageService;
//        private readonly QueueService _queueService;

//        public OrdersController(TableStorageService tableStorageService, QueueService queueService)
//        {
//            _tableStorageService = tableStorageService;
//            _queueService = queueService;
//        }

//        // Action to display all Orders (optional first)
//        public async Task<IActionResult> Index()
//        {
//            var orders = await _tableStorageService.GetAllOrdersAsync();
//            return View(orders);
//        }
//        public async Task<IActionResult> Register()
//        {
//            var customers = await _tableStorageService.GetAllCustomersAsync();
//            var products = await _tableStorageService.GetAllProductsAsync();

//            // Check for null or empty lists
//            if (customers == null || customers.Count == 0)
//            {
//                // Handle the case where no customers are found
//                ModelState.AddModelError("", "No customers found. Please add customers first.");
//                return View(); // Or redirect to another action
//            }

//            if (products == null || products.Count == 0)
//            {
//                // Handle the case where no products are found
//                ModelState.AddModelError("", "No products found. Please add products first.");
//                return View(); // Or redirect to another action
//            }

//            ViewData["Customers"] = customers;
//            ViewData["Products"] = products;

//            return View();
//        }



//        // Action to handle the form submission and register the order
//        [HttpPost]
//        public async Task<IActionResult> Register(Order order)
//        {
//            if (ModelState.IsValid)
//            {//TableService
//                order.Order_Date = DateTime.SpecifyKind(order.Order_Date, DateTimeKind.Utc);
//                order.PartitionKey = "OrdersPartition";
//                order.RowKey = Guid.NewGuid().ToString();
//                await _tableStorageService.AddOrderAsync(order);
//                //MessageQueue
//                string message = $"New order by customer {order.Customer_Id} of Product {order.Product_Id} at " +
//                    $"{order.Order_Location} on {order.Order_Date}";
//                await _queueService.SendMessageAsync(message);

//                return RedirectToAction("Index");
//            }
//            else
//            {
//                // Log model state errors
//                foreach (var error in ModelState)
//                {
//                    Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
//                }
//            }

//            // Reload Customer and Product lists if validation fails
//            var customers = await _tableStorageService.GetAllCustomersAsync();
//            var products = await _tableStorageService.GetAllProductsAsync();
//            ViewData["Customers"] = customers;
//            ViewData["Products"] = products;

//            return View(order);
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using st10293982CLD6212POE1.Models;
using st10293982CLD6212POE1.Services;

namespace st10293982CLD6212POE1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly TableStorageService _tableStorageService;
        private readonly QueueService _queueService;

        public OrdersController(TableStorageService tableStorageService, QueueService queueService)
        {
            _tableStorageService = tableStorageService;
            _queueService = queueService;
        }

        // Action to display all Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _tableStorageService.GetAllOrdersAsync();
            return View(orders);
        }

        public async Task<IActionResult> Register()
        {
            var customers = await _tableStorageService.GetAllCustomersAsync();
            var products = await _tableStorageService.GetAllProductsAsync();

            // Check for null or empty lists
            if (customers == null || customers.Count == 0)
            {
                ModelState.AddModelError("", "No customers found. Please add customers first.");
                return View();
            }

            if (products == null || products.Count == 0)
            {
                ModelState.AddModelError("", "No products found. Please add products first.");
                return View();
            }

            ViewData["Customers"] = customers;
            ViewData["Products"] = products;

            return View();
        }

        // Action to handle the form submission and register the order
        [HttpPost]
        public async Task<IActionResult> Register(Order order)
        {
            if (ModelState.IsValid)
            {
                // Generate a random Customer_Id and Product_Id if not provided
                if (order.Customer_Id == 0)
                {
                    var customers = await _tableStorageService.GetAllCustomersAsync();
                    order.Customer_Id = customers[new Random().Next(customers.Count)].CustomerId;
                }

                if (order.Product_Id == 0)
                {
                    var products = await _tableStorageService.GetAllProductsAsync();
                    order.Product_Id = products[new Random().Next(products.Count)].Product_Id;
                }

                // Set Order Date and keys
                order.Order_Date = DateTime.SpecifyKind(order.Order_Date, DateTimeKind.Utc);
                order.PartitionKey = "OrdersPartition";
                order.RowKey = Guid.NewGuid().ToString();

                // Save order to Table Storage
                await _tableStorageService.AddOrderAsync(order);

                // Send message to the Queue
                //string message = $"New order by customer {order.Customer_Id} for product {order.Product_Id} at {order.Order_Location} on {order.Order_Date}.";
                //await _queueService.SendMessageAsync(message);

                // Redirect to Index (Orders page)
                return RedirectToAction("Index");
            }

            // Reload Customer and Product lists if validation fails
            var customersList = await _tableStorageService.GetAllCustomersAsync();
            var productsList = await _tableStorageService.GetAllProductsAsync();
            ViewData["Customers"] = customersList;
            ViewData["Products"] = productsList;

            return View(order);
        }
    }
}
