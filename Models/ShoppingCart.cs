using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KidShop.Models
{
    public class ShoppingCart
    {
        public Dictionary<string, CartItem> Items { get; set; }

        public double TotalPrice => Items.Values.Sum(items => items.TotalItemPrice);

        public ShoppingCart()
        {
            Items = new Dictionary<string, CartItem>();
        }

        public void Add(Product product, int quantity, bool isUpdate)
        {
            var cartItem = new CartItem()
            {
                ProductId = product.ProductId,
                ProductPrice = product.Price,
                ProductName = product.ProductName,
                ProductThumbnail = product.Thumbnail,
                Quantity = quantity
            };
            var existKey = Items.ContainsKey(product.ProductId);
            if (!isUpdate && existKey)
            {
                var existingItem = Items[product.ProductId];
                cartItem.Quantity += existingItem.Quantity;
                if(cartItem.Quantity < 1)
                {
                    cartItem.Quantity = 1;
                }
            }
            if (existKey)
            {
                Items[product.ProductId] = cartItem;
            }
            else
            {
                Items.Add(product.ProductId, cartItem);
            }
        }

        public void Add(Product product)
        {
            Add(product, 1, false);
        }

        public void Update(Product product, int quantity)
        {
            Add(product, quantity, true);
        }

        public void Remove(string productId)
        {
            if (Items.ContainsKey(productId))
            {
                Items.Remove(productId);
            }
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
    public class CartItem
    {
        [Key]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductThumbnail { get; set; }
        public double ProductPrice { get; set; }
        public double TotalItemPrice => ProductPrice * Quantity;
        public int Quantity { get; set; }
    }
}