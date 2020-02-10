using EFDataLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDataLibrary.Models
{
    public class Cart : ICart<CartLine>
    {
        private List<CartLine> cartLines = new List<CartLine>();
        public IEnumerable<CartLine> Lines { get { return cartLines; } }
        private string CartId { get; set; }
        public static  Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            Console.WriteLine("Hello world");
            return new Cart() { CartId = cartId };
        }
        public void AddLine(Product product, int quantity)
        {
            CartLine line = cartLines
             .Where(p => p.Product.Id == product.Id)
             .FirstOrDefault();
            if (line == null)
            {
                cartLines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void Clear()
        {
            cartLines.Clear();
        }

        public decimal ComputeTotalValue()
        {
            return cartLines.Sum(e => e.Product.Price * e.Quantity);
        }

        public void RemoveLine(Product product)
        {
            cartLines.RemoveAll(l => l.Product.Id == product.Id);
        }
    }
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
