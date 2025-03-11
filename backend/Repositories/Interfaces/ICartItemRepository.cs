using NexCart.Models;

namespace NexCart.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        CartItem GetCartItemById(int cartItemId);
        IEnumerable<CartItem> GetCartItemsByCartId(int cartId);
        void AddCartItem(CartItem cartItem);
        void RemoveCartItem(int cartItemId);
    }
}
