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
        public async Task<ActionResult> Details(int id)
        {
            Product product = new Product();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            UriBuilder builder = new UriBuilder("https://localhost:44356/api/Products/GetProductDetail?");
            builder.Query = "Id=" + id;
            HttpResponseMessage Res = await client.GetAsync(builder.Uri);
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var Product1 = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the SiteUser object 
                Product[] a= JsonConvert.DeserializeObject<Product[]>(Product1);
                ViewBag.Product = product;
                ViewBag.Error = null;
                return View("Single", a);
            }
            return View();
        }
        
        //Added by SM on Nov 25, 2020 for cart
        public ActionResult AddCart(int Id, string Name, decimal Price)
        {
           Cart cart = new Cart
            {
               Id = Id,
                Name = Name,
                Price = Price
            };
            List<Cart> li = HttpContext.Session.Get<List<Cart>>("cart");
            Cart li1 = HttpContext.Session.Get<Cart>("cart"); //this runs for null but not for not null
            if (li == null) //list is empty
            {
                List<Cart> a = new List<Cart>();
                a.Add(cart);
                HttpContext.Session.Set<Cart>("cart", cart);
                return View("MyCart", a);
            }
           
            else
            {

                li.Add(cart);
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
                HttpContext.Session.Set<List<Cart>>("cart",li);
                return View("mycart", li);
            }

            //if (HttpContext.Session.GetString("cart") == null)
            //{

            //    List<Cart> li = new List<Cart>();
            //    li.Add(cart);

            //    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
            //    ViewBag.cartCount = li.Count();
            //    HttpContext.Session.SetInt32("count", 1);
            //    ViewBag.cart = cart;
            //    //ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            //    //return View("~/Views/Home/Index");
            //    return View("MyCart", (List<Cart>)li);
            //}
            //else //not empty
            //{

            //    List<Cart> li = new List<Cart>();
            //    li.Add(cart);
            //    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
            //    var value = HttpContext.Session.GetString("cart");
            //    List<Cart> li1 = JsonConvert.DeserializeObject<List<Cart>>(value);
            //    //Cart[] li = JsonConvert.DeserializeObject<Cart[]>(value);
            //    //li.Add(cart);//newly entered item
            //    //HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
            //    ViewBag.cartCount = li.Count();// new count
            //    HttpContext.Session.SetInt32("count", li.Count());
            //    var a = HttpContext.Session.GetInt32("count");
            //    //Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            //    return View("MyCart", li1);
            //}

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

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            
            var value = session.GetString(key);
            return value == null ? default(T) :JsonConvert.DeserializeObject<T>(value);

        }
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
