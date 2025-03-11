using NexCart.Data;
using NexCart.Models;
using NexCart.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace NexCart.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly NexCartDBContext _context;

        public CartRepository(NexCartDBContext context)
        {
            _context = context;
        }

        public Cart GetCartByUserId(int userId)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserId == userId);
        }

        public void AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            var existingCartItem = _context.CartItems.Find(cartItem.CartItemId);
            if (existingCartItem != null)
            {
                // Update only necessary fields
                existingCartItem.ProductId = cartItem.ProductId;
                existingCartItem.Quantity = cartItem.Quantity;

                _context.SaveChanges();
            }
        }


        public void RemoveCartItem(int cartItemId)
        {
            var cartItem = _context.CartItems.Find(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public void ClearCart(int cartId)
        {
            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.CartId == cartId);
            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.SaveChanges();
            }
        }
    }
}
