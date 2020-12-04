using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DesiClothing4u.UI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using DesiClothing4u.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DesiClothing4u.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IHttpContextAccessor Accessor { get; private set; }


        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            Accessor = accessor;
        }
        //By Mohtashim on Nov 29, 2020
        public async Task<ActionResult<Customer>> CheckCustomerLogin(IFormCollection collection)
        {
            Customer cutomer = new Customer();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            UriBuilder builder = new UriBuilder("https://localhost:44356/api/Customers/ValidateCustomer?");
            builder.Query = "email=" + collection["exampleInputEmail1"] + "&UserPassword=" + collection["exampleInputPassword1"];
            HttpResponseMessage Res = await client.GetAsync(builder.Uri);
            var Customer = Res.Content.ReadAsStringAsync().Result;
            var a = JsonConvert.DeserializeObject<Customer>(Customer);
            //Store in cookies
            if (Request.Cookies["UserId"] == null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(50);
                option.IsEssential = true;
                Accessor.HttpContext.Response.Cookies.Append("UserId", a.Id.ToString(), option);
                string Usr = HttpContext.Request.Cookies["UserId"];
            }
            Load load = new Load();
            //Featured--field name MarkAsNew
            var clientF = new HttpClient();
            var urlF = "https://localhost:44356/api/Products/GetFeatuedProducts";
            var responseF = await clientF.GetAsync(urlF);
            var FeaturedProduct = responseF.Content.ReadAsStringAsync().Result;
            load.FeaturedProduct = JsonConvert.DeserializeObject<Product[]>(FeaturedProduct);
            //New Arrivals--field name Recent
            var clientN = new HttpClient();
            var urlN = "https://localhost:44356/api/Products/GetNewProducts";
            var responseN = await clientN.GetAsync(urlN);
            var NewProduct = responseN.Content.ReadAsStringAsync().Result;
            load.NewProduct = JsonConvert.DeserializeObject<Product[]>(NewProduct);
            //Customers
            if (Request.Cookies["UserId"] != null)
            {
                var clientC = new HttpClient();
                UriBuilder builderC = new UriBuilder("https://localhost:44356/api/Customers/LoginID?");
                builderC.Query = "UserId=" + Request.Cookies["UserId"];
                HttpResponseMessage responseC = await clientC.GetAsync(builderC.Uri);
                if (responseC.IsSuccessStatusCode)
                {
                    var Users = responseC.Content.ReadAsStringAsync().Result;
                    load.Customer = JsonConvert.DeserializeObject<Customer>(Users);
                    ViewBag.UserName = load.Customer.Username;
                }
            }
            return View("Index", load);
        }

        //By SM on Nov 12, 2020, remove Index1 action controller
        public async Task<ActionResult> Index()
        {
            try
            {
                Load load = new Load();
                //Featured--field name MarkAsNew
                var clientF = new HttpClient();
                var urlF = "https://localhost:44356/api/Products/GetFeatuedProducts";
                var responseF = await clientF.GetAsync(urlF);
                var FeaturedProduct = responseF.Content.ReadAsStringAsync().Result;
                load.FeaturedProduct = JsonConvert.DeserializeObject<Product[]>(FeaturedProduct);
                //New Arrivals--field name Recent
                var clientN = new HttpClient();
                var urlN = "https://localhost:44356/api/Products/GetNewProducts";
                var responseN = await clientN.GetAsync(urlN);
                var NewProduct = responseN.Content.ReadAsStringAsync().Result;
                load.NewProduct = JsonConvert.DeserializeObject<Product[]>(NewProduct);
                //Customers
                if (Request.Cookies["UserId"] != null)
                {
                    var clientC = new HttpClient();
                    UriBuilder builderC = new UriBuilder("https://localhost:44356/api/Customers/LoginID?");
                    builderC.Query = "UserId=" + Request.Cookies["UserId"];
                    HttpResponseMessage responseC = await clientC.GetAsync(builderC.Uri);
                    if (responseC.IsSuccessStatusCode)
                    {
                        var Users = responseC.Content.ReadAsStringAsync().Result;
                        load.Customer = JsonConvert.DeserializeObject<Customer>(Users);
                        ViewBag.UserName = load.Customer.Username;

                    }
                }
                
                return View("Index", load);
            }
            catch (Exception e)
            {
                Error err = new Error();
                err.ErrorMessage = "Sorry couldn't autoload";
                ViewBag.Error = err;
                return View("Error", err);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View("~/Views/shared/Register.cshtml");
        }

        // POST: AccountController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] dynamic MyCustomer)
        {
            try

            {
                var sMyCustomer = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(MyCustomer.ToString());
                Address address = new Address
                {
                    FirstName = sMyCustomer.FirstName,
                    LastName = sMyCustomer.LastName,
                    Address1 = sMyCustomer.StreetAddress1,
                    Address2 = sMyCustomer.StreetAddress2,
                    City = sMyCustomer.City,
                    Email = sMyCustomer.Email,
                    CreatedOnUtc = DateTime.UtcNow,
                    ZipPostalCode = sMyCustomer.ZipCode,
                    PhoneNumber = sMyCustomer.phoneno
                };
                string output = JsonConvert.SerializeObject(address);
                var data = new StringContent(output, Encoding.UTF8, "application/json");
                var url = "https://localhost:44356/api/Addresses";
                var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                var Address = response.Content.ReadAsStringAsync().Result;
                var BillingAddress1 = JsonConvert.DeserializeObject<Address>(Address);
                var BillingAddressId = BillingAddress1.Id;
                
                Customer customer = new Customer
                {
                    Username = sMyCustomer.Email,
                    Email = sMyCustomer.Email,
                    EmailToRevalidate = "",
                    SystemName = "",
                    BillingAddressId = BillingAddress1.Id,
                    ShippingAddressId = BillingAddress1.Id,
                    AdminComment = "",
                    IsTaxExempt = true,
                    AffiliateId = 0,
                    VendorId = 0,
                    HasShoppingCartItems = false,
                    RequireReLogin = false,
                    FailedLoginAttempts = 0,
                    Active = true,
                    Deleted = false,
                    IsSystemAccount = false,
                    LastIpAddress = "",
                    CreatedOnUtc = DateTime.UtcNow,
                    RegisteredInStoreId = 1,
                    Password = sMyCustomer.Password
                };
                output = JsonConvert.SerializeObject(customer);
                data = new StringContent(output, Encoding.UTF8, "application/json");
                url = "https://localhost:44356/api/Customers";
                client = new HttpClient();
                response = await client.PostAsync(url, data);
                var Customer = response.Content.ReadAsStringAsync().Result;
                var a = JsonConvert.DeserializeObject<Customer>(Customer);
                ViewBag.Customer = a;
                //Store in cookies
                if (Request.Cookies["UserId"] == null)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(50);
                    option.IsEssential = true;
                    
                    Response.Cookies.Append("UserId", a.Id.ToString(), option);
                    string Usr = HttpContext.Request.Cookies["UserId"];
                /* cookie code ends here*/
                }
                return View(a);
                //return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}
