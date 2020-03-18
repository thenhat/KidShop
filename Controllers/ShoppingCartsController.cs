using KidShop.Data;
using KidShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidShop.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        private const string ShoppingCartSessionName = "SHOPPING_CART";

        // GET: ShoppingCart
        public ActionResult AddToCart(string productId, int quantity)
        {
            var existingProduct = db.Products.FirstOrDefault(p => p.ProductId == productId);
            if (existingProduct == null)
            {
                return new HttpNotFoundResult();
            }

            var shoppingCart = GetShoppingCart();
            shoppingCart.Add(existingProduct, quantity, false);
            SetShoppingCart(shoppingCart);
            return RedirectToAction("ShowCart", "ShoppingCarts");
        }

        public ActionResult ShowCart()
        {
            return View("ShowCart", GetShoppingCart());
        }

        public ActionResult UpdateCart(string productId, int quantity)
        {
            var existingProduct = db.Products.FirstOrDefault(p => p.ProductId == productId);

            if (existingProduct == null)
            {
                return new HttpNotFoundResult();
            }
            var shoppingCart = GetShoppingCart();
            shoppingCart.Update(existingProduct, quantity);
            SetShoppingCart(shoppingCart);
            return View("ShowCart");
        }

        public ActionResult RemoveCartItem(string productId)
        {
            var shoppingCart = GetShoppingCart();
            shoppingCart.Remove(productId);
            SetShoppingCart(shoppingCart);
            return View("ShowCart");
        }

        public ActionResult RemoveAll()
        {
            ClearShoppingCart();
            return RedirectToAction("ShowCart", "ShoppingCarts");
        }

        private ShoppingCart GetShoppingCart()
        {
            ShoppingCart shoppingCart = null;

            if (Session[ShoppingCartSessionName] != null)
            {
                try
                {
                    shoppingCart = Session[ShoppingCartSessionName] as ShoppingCart;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
            }
            return shoppingCart;
        }

        private void SetShoppingCart(ShoppingCart shoppingCart)
        {
            Session[ShoppingCartSessionName] = shoppingCart;
        }

        private void ClearShoppingCart()
        {
            Session[ShoppingCartSessionName] = null;
        }
    }
}