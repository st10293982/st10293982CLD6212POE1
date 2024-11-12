using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using st10293982CLD6212POE1.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace st10293982CLD6212POE1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrdersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Display all orders
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://st10293982function.azurewebsites.net/api/table/orders?code=YOUR_API_KEY");

            if (response.IsSuccessStatusCode)
            {
                var ordersJson = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<Order>>(ordersJson);
                return View(orders);
            }

            return View(new List<Order>());
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Order order)
        {
            if (ModelState.IsValid)
            {
                order.PartitionKeys = "OrdersPartition";
                order.RowKeys = Guid.NewGuid().ToString();
                order.Order_Dates = DateTime.SpecifyKind(order.Order_Dates, DateTimeKind.Utc);

                string jsonOrder = JsonConvert.SerializeObject(order);
                StringContent content = new StringContent(jsonOrder, Encoding.UTF8, "application/json");

                // Add the order to table storage
                var response = await _httpClient.PostAsync("https://st10293982function.azurewebsites.net/api/table/add?code=YOUR_ADD_API_KEY", content);

                if (response.IsSuccessStatusCode)
                {
                    // Send a message to the queue
                    string message = $"New order by customer {order.Customer_Id} for product {order.Product_Id} at {order.Order_Location} on {order.Order_Date}.";
                    StringContent queueMessage = new StringContent(message, Encoding.UTF8, "application/json");

                    await _httpClient.PostAsync("https://st10293982function.azurewebsites.net/api/queue/send?code=YOUR_QUEUE_API_KEY", queueMessage);

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Failed to register the order.");
            }

            return View(order);
        }
    }
}
