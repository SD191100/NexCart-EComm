using NexCart.Models;

namespace NexCart.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Cart GetCartByUserId(int userId);
        void AddCart(Cart cart);
        void AddCartItem(CartItem cartItem);
        void UpdateCartItem(CartItem cartItem);
        void RemoveCartItem(int cartItemId);
        void ClearCart(int cartId);
    }
}
