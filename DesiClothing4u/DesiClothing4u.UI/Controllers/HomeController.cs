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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
        public async Task<ActionResult<Customer>> CreateCustomer(string FirstName, string LastName, string Password, string Email, string StreetAddress1, string StreetAddress2, string Country, string city, string phoneno, string ZipCode)
        {
            try

            {
                Address address = new Address
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Address1 = StreetAddress1,
                    Address2 = StreetAddress2,
                    City = city,
                    Email = Email,
                    CreatedOnUtc = DateTime.UtcNow,
                    ZipPostalCode = ZipCode,
                    PhoneNumber = phoneno
                };
                string output = JsonConvert.SerializeObject(address);
                var data = new StringContent(output, Encoding.UTF8, "application/json");
                var url = "https://localhost:44356/api/Addresses";
                var client = new HttpClient();
                var response = await client.PostAsync(url, data);
                var Address = response.Content.ReadAsStringAsync().Result;
                var BillingAddress1 = JsonConvert.DeserializeObject<Address>(Address);
                var BillingAddressId = BillingAddress1.Id;
                //ViewBag.SiteUsers = a;
                //PostAddress

                Customer customer = new Customer
                {
                    Username = Email,
                    Email = Email,
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
                    Password = Password,
                };
                //CustomerPassword customerPassword = new CustomerPassword();
                //customerPassword.Password = collection["password"];
                //customerPassword.CreatedOnUtc = DateTime.UtcNow;
                //customer.CustomerPasswords.Add(customerPassword);
                //CustomerAddress customerAddress = new CustomerAddress();
                //customerAddress.AddressId = BillingAddress1.Id;
                //customerAddress.CustomerId = customer.Id;
                //customer.CustomerAddresses.Add(customerAddress);



                output = JsonConvert.SerializeObject(customer);
                data = new StringContent(output, Encoding.UTF8, "application/json");
                url = "https://localhost:44356/api/Customers";
                client = new HttpClient();
                response = await client.PostAsync(url, data);
                var Customer = response.Content.ReadAsStringAsync().Result;
                var a = JsonConvert.DeserializeObject<Customer>(Customer);
                ViewBag.Customer = a;
                //return View("~/Views/Home/Index", a);
                //return RedirectToAction(nameof(Index));
                return PartialView("Welcome", a);
                //return View();
            }
            catch
            {
                return View("~/Views/Home/Index");
            }
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
