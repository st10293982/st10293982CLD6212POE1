using Microsoft.AspNetCore.Mvc;
using st10293982CLD6212POE1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace st10293982CLD6212POE1.Controllers
{
    public class ShopsController : Controller
    {
        private static List<Shops> shops = new List<Shops>
        {
            new Shops { Id = 1, Name = "T-Shirt", Description = "A plain t-shirt.", Price = 19.99M, ImageURL = "/images/tshirt.jpg" },
            new Shops { Id = 2, Name = "Jeans", Description = "Blue denim jeans.", Price = 49.99M, ImageURL = "/images/jeans.jpg" },
            new Shops { Id = 3, Name = "Sneakers", Description = "Comfortable sneakers.", Price = 69.99M, ImageURL = "/images/sneakers.jpg" }
        };

        // GET: Shops
        public IActionResult Index()
        {
            return View(shops);
        }

        // GET: Shops/Details/5
        public IActionResult Details(int id)
        {
            var shop = shops.FirstOrDefault(p => p.Id == id);
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        // GET: Shops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Shops shop)
        {
            if (ModelState.IsValid)
            {
                shop.Id = shops.Max(p => p.Id) + 1;
                shops.Add(shop);
                return RedirectToAction(nameof(Index));
            }
            return View(shop);
        }

        // GET: Shops/Edit/5
        public IActionResult Edit(int id)
        {
            var shop = shops.FirstOrDefault(p => p.Id == id);
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        // POST: Shops/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Shops shop)
        {
            if (id != shop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingShop = shops.FirstOrDefault(p => p.Id == id);
                if (existingShop == null)
                {
                    return NotFound();
                }

                existingShop.Name = shop.Name;
                existingShop.Description = shop.Description;
                existingShop.Price = shop.Price;
                existingShop.ImageURL = shop.ImageURL;

                return RedirectToAction(nameof(Index));
            }
            return View(shop);
        }

        // GET: Shops/Delete/5
        public IActionResult Delete(int id)
        {
            var shop = shops.FirstOrDefault(p => p.Id == id);
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var shop = shops.FirstOrDefault(p => p.Id == id);
            if (shop == null)
            {
                return NotFound();
            }

            shops.Remove(shop);
            return RedirectToAction(nameof(Index));
        }
    }
}
