using NexCart.Models;

namespace NexCart.Services.Interfaces
{
    public interface ICartItemService
    {
        CartItem GetCartItemById(int cartItemId);
        IEnumerable<CartItem> GetCartItemsByCartId(int cartId);
        void AddCartItem(CartItem cartItem);
        void RemoveCartItem(int cartItemId);
    }
}
