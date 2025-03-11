using NexCart.DTOs;

namespace NexCart.Services.Interfaces
{
    public interface ICartService
    {
        CartDto GetCartByUserId(int userId);
        void AddToCart(int userId, CartItemDto cartItemDto);
        void UpdateCartItem(int cartItemId, int Quantity);
        void RemoveCartItem(int cartItemId);
        void ClearCart(int userId);
    }
}
