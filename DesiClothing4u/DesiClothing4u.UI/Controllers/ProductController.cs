using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DesiClothing4u.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DesiClothing4u.UI.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Product>> Create(IFormCollection collection)
        {
            try
            {
                Product product = new Product
                {
                    Name = collection["ProductName"],
                    ProductTypeId = 5,
                    VisibleIndividually = true,
                    ShortDescription= collection["ShortDescription"],
                    FullDescription = collection["FullDescription"],
                    ProductTemplateId = 1,
                    AllowCustomerReviews = true,
                    CreatedOnUtc = DateTime.UtcNow
                };

                var output = JsonConvert.SerializeObject(product);
                var data = new StringContent(output, Encoding.UTF8, "application/json");
                var url = "https://localhost:44356/api/Vendors";
                var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                var Vendor = response.Content.ReadAsStringAsync().Result;
                var a = JsonConvert.DeserializeObject<Vendor>(Vendor);
                ViewBag.Product = a;
                return View(a);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
