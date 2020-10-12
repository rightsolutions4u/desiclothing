using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesiClothing4u.Common.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace DesiClothing4u.UI.Controllers
{
    public class VendorController : Controller
    {
        // GET: VendorController
        public ActionResult Index()
        {
            return View("~/Views/VendorRegister.cshtml");
            //return View("VendorView");
        }

        // GET: VendorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VendorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Vendor>> Create(IFormCollection collection)
        {
            try
            {
                Address address = new Address
                {
                    FirstName = collection["Name"],
                    LastName = collection["Name"],
                    Address1 = collection["StreetAddress1"],
                    Address2 = collection["StreetAddress2"],
                    City = collection["city"],
                    Email = collection["email"],
                    CreatedOnUtc = DateTime.UtcNow,
                    ZipPostalCode = collection["ZipCode"],
                    PhoneNumber = collection["phoneno"]
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

                Vendor vendor = new Vendor
                {
                    Name = collection["name"],
                    Email = collection["Email"],
                    AddressId = BillingAddress1.Id,
                    Active = true,
                    Deleted = false,
                    password = collection["password"]
                    //,                    PictureId = PictureId1.Id
                };

                output = JsonConvert.SerializeObject(vendor);
                data = new StringContent(output, Encoding.UTF8, "application/json");
                url = "https://localhost:44356/api/Vendors";
                client = new HttpClient();
                response = await client.PostAsync(url, data);
                var Vendor = response.Content.ReadAsStringAsync().Result;
                var a = JsonConvert.DeserializeObject<Vendor>(Vendor);
                ViewBag.Vendor = a;
                return View("VendorView", a);
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult<Vendor>> CheckVendorLogin(IFormCollection collection)
            //string email,string pwd)
        {
            //ValidateVendor
            //Vendor vendor = new Vendor
            //{
            //    Email = email,
            //    password = pwd
            //};


            Vendor vendor = new Vendor();
            var client = new HttpClient();
            //client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            //Sending request to find web api REST service resource PostSiteUsers using HttpClient  
            UriBuilder builder = new UriBuilder("https://localhost:44356/api/Vendors/ValidateVendor?");
            builder.Query = "email=" + collection["exampleInputEmail1"] + "&UserPassword=" + collection["exampleInputPassword1"];
            HttpResponseMessage Res = await client.GetAsync(builder.Uri);


            //var output = JsonConvert.SerializeObject(vendor);
            //var data = new StringContent(output, Encoding.UTF8, "application/json");
            //var url = "https://localhost:44356/api/Vendors/ValidateVendor";
            //var client = new HttpClient();
            //var response = await client.PostAsync(url, data);
            var Vendor = Res.Content.ReadAsStringAsync().Result;
            var a = JsonConvert.DeserializeObject<Vendor>(Vendor);
            ViewBag.Vendor = a;
            return View("VendorView", a);
        }

        // GET: VendorController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: VendorController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: VendorController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: VendorController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
