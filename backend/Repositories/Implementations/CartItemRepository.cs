using NexCart.Data;
using NexCart.Models;
using NexCart.Repositories.Interfaces;

namespace NexCart.Repositories.Implementations
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly NexCartDBContext _context;

        public CartItemRepository(NexCartDBContext context)
        {
            _context = context;
        }

        public CartItem GetCartItemById(int cartItemId)
        {
            return _context.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
        }

        public IEnumerable<CartItem> GetCartItemsByCartId(int cartId)
        {
            return _context.CartItems.Where(ci => ci.CartId == cartId).ToList();
        }

        public void AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void RemoveCartItem(int cartItemId)
        {
            var cartItem = GetCartItemById(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }
    }
}
