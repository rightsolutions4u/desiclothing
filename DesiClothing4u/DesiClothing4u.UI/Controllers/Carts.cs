using DesiClothing4u.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesiClothing4u.UI.Controllers
{
    public class Cartsontroller : Controller
    {
        public static class CartExtensions
        {
            public static T GetObjectFromJson<T>(this ISession session, string key)
            {
                var value = session.GetString(key);
                return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }
            public static void SetObjectAsJson(this ISession session, string key, object value)
            {
                session.SetString(key, JsonConvert.SerializeObject(value));
            }

            //Added by SM on Nov 25, 2020 for cart
            public static ActionResult AddCart(int Id, string Name, decimal Price)
            {

                Cart cart = new Cart
                {

                    Id = Id,
                    Name = Name,
                    Price = Price

                };

                lia = CartExtensions.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                //key="cart", key="count"
                //if cart is empty
                if (HttpContext.Session.GetString("cart") == null)
                {
                    List<Cart> li = new List<Cart>();
                    li.Add(cart);
                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
                    ViewBag.cartCount = li.Count();
                    HttpContext.Session.SetInt32("count", 1);
                    ViewBag.cart = cart;
                    //ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
                    //return View("~/Views/Home/Index");
                    return View("MyCart", (List<Cart>)li);
                }
                else //not empty
                {

                    List<Cart> li = new List<Cart>();
                    li.Add(cart);
                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
                    var value = HttpContext.Session.GetString("cart");
                    List<Cart> li1 = JsonConvert.DeserializeObject<List<Cart>>(value);
                    //Cart[] li = JsonConvert.DeserializeObject<Cart[]>(value);
                    //li.Add(cart);//newly entered item
                    //HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(li));
                    ViewBag.cartCount = li.Count();// new count
                    HttpContext.Session.SetInt32("count", li.Count());
                    var a = HttpContext.Session.GetInt32("count");
                    //Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                    return View("MyCart", li1);
                }

            }
        }
    }
}
