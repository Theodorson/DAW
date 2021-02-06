using Shopping_Site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping_Site.Controllers
{
   
    [Authorize]
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

            

        // GET: Cart
        public ActionResult Index()
        {
            if (Session["cart"] == null)
            {

                List<CartItem> cart = new List<CartItem>();
                Session["cart"] = cart;
                if (cart.Count != 0)
                {
                    ViewBag.Message = "";
                }
                else
                {
                    ViewBag.Message = "Your cart is empty!";
                }
            }
            else
            {
                List<CartItem> cart_test = (List<CartItem>)Session["cart"];
                if (cart_test.Count != 0)
                {
                    ViewBag.Message = "";
                }
                else
                {
                    ViewBag.Message = "Your cart is empty!";
                }
            }
            return View();
        }

       
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            Phone phone = db.Phones.Find(id);
            if (Session["cart"] == null)
            {
               
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Product = phone, Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int index = ifExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = phone, Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index", "Phone");
        }

        [HttpGet]
        public ActionResult BuyNow()
        {
            CartOrderCompletionBirthViewModel order = new CartOrderCompletionBirthViewModel();

            List<CartItem> cart = (List<CartItem>)Session["cart"];

            if (cart.Count != 0)
            {
                ViewBag.Message = "";
                return View(order);
            }
            else
            {
                ViewBag.Message = "Your cart is empty!";
                return View("Index"); 
            }
        }

        [HttpPost]
        public ActionResult BuyNow(CartOrderCompletionBirthViewModel orderRequest)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            try
            {   
                if (ModelState.IsValid)
                {

                    Birth birth = new Birth
                    {
                        BirthYear = orderRequest.BirthYear,
                        BirthMonth = orderRequest.BirthMonth,
                        BirthDay = orderRequest.BirthDay
                    };
                    db.Births.Add(birth);

                    CartOrderCompletion order = new CartOrderCompletion
                    {
                        First_name = orderRequest.First_name,
                        Last_name = orderRequest.Last_name,
                        Phone_number = orderRequest.Phone_number,
                        Region = orderRequest.Region,
                        City = orderRequest.City,
                        Address = orderRequest.Address,
                        Birth = birth
                    };

                    order.Data = DateTime.Now.ToString();    
                    foreach (CartItem item in cart)
                    {
                        order.Products += "[" + item.Product.Name + "," + item.Product.Brand + "," + item.Quantity + "] ";
                    }
                    db.CartOrders.Add(order);

                    db.SaveChanges();
                    // se elibereaza cosul
                    Session.Clear();

                    return RedirectToAction("Index", "Phone");
                }
          
                return View(orderRequest);
            }
            catch (Exception e)
            {
           
                return View(orderRequest);
            }
        }

        public ActionResult Remove(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            int index = ifExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult IncreaseQuantity(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            int index = ifExist(id);
            cart[index].Quantity++;
            Session["cart"] = cart;
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult DecreaseQuantity(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            int index = ifExist(id);
            if (cart[index].Quantity > 1) 
            {
                cart[index].Quantity--;
            }
            
            Session["cart"] = cart;
            return RedirectToAction("Index", "Cart");
        }

        private int ifExist(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.PhoneID.Equals(id))
                    return i;
            return -1;
        }

        

    }
}