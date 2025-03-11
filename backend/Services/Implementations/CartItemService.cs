using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;

namespace NexCart.Services.Implementations
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public CartItem GetCartItemById(int cartItemId)
        {
            return _cartItemRepository.GetCartItemById(cartItemId);
        }

        public IEnumerable<CartItem> GetCartItemsByCartId(int cartId)
        {
            return _cartItemRepository.GetCartItemsByCartId(cartId);
        }

        public void AddCartItem(CartItem cartItem)
        {
            _cartItemRepository.AddCartItem(cartItem);
        }

        public void RemoveCartItem(int cartItemId)
        {
            _cartItemRepository.RemoveCartItem(cartItemId);
        }
    }
}
