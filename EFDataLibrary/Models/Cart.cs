using EFDataLibrary.DataAccess;
using EFDataLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDataLibrary.Models
{
    public class Cart : ICart<CartLine>
    {
        public IEnumerable<CartLine> Lines { get { return _db.CartLines.Where(cart => cart.CartId == CartId).Include(p=>p.Product); } }
        public string CartId { get; set; }
        private UndefinedContext _db;
        public Cart(UndefinedContext db)
        {
            _db = db;
        }
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            var context = services.GetService<UndefinedContext>();
            session.SetString("CartId", cartId);
            return new Cart(context) { CartId = cartId };
        }
        public void AddLine(Product product, int quantity)
        {
            CartLine line = _db.CartLines
             .Where(p => p.Product.Id == product.Id && p.CartId == CartId)
             .FirstOrDefault();
            if (line == null)
            {
                _db.CartLines.Add(new CartLine
                {
                    CartId = CartId,
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
            _db.SaveChanges();
        }

        public void Clear()
        {
            var cartList = _db.CartLines.Where(cart => cart.CartId == CartId);
            _db.CartLines.RemoveRange(cartList);
            _db.SaveChangesAsync();
        }
        public decimal ComputeTotalValue()
        {
            return _db.CartLines.Where(c => c.CartId == CartId).Select(c => c.Product.Price * c.Quantity).Sum();
        }

        public void RemoveLine(Product product)
        {
            CartLine cartLine = _db.CartLines.FirstOrDefault(l => l.Product.Id == product.Id && l.CartId == CartId);
            _db.CartLines.Remove(cartLine);
            _db.SaveChanges();
        }
    }
    public class CartLine
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }
    }
}
