using Microsoft.EntityFrameworkCore;
using NexCart.DTOs;
using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;

namespace NexCart.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public CartDto GetCartByUserId(int userId)
        {
            var cart = _cartRepository.GetCartByUserId(userId);
            if (cart == null) return null;

            return new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                CartItems = cart.CartItems.Select(ci => new CartItemDto
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    cartItemId = ci.CartItemId
                }).ToList()
            };
        }

        public void AddToCart(int userId, CartItemDto cartItemDto)
        {
            var cart = _cartRepository.GetCartByUserId(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _cartRepository.AddCart(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemDto.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += cartItemDto.Quantity;
                _cartRepository.UpdateCartItem(cartItem);
            }
            else
            {
                _cartRepository.AddCartItem(new CartItem
                {
                    ProductId = cartItemDto.ProductId,
                    Quantity = cartItemDto.Quantity,
                    CartId = cart.CartId
                });
            }
        }

        public void UpdateCartItem(int cartItemId, int Quantity)
        {
            var existingCartItem = _cartItemRepository.GetCartItemById(cartItemId);
            if (existingCartItem == null)
                throw new InvalidOperationException("Cart item not found.");

            //existingCartItem.ProductId = cartItemDto.ProductId;
            existingCartItem.Quantity = Quantity;

            // Keep the CartId intact
            _cartRepository.UpdateCartItem(existingCartItem);
        }


        public void RemoveCartItem(int cartItemId)
        {
            _cartRepository.RemoveCartItem(cartItemId);
        }

        public void ClearCart(int userId)
        {
            var cart = _cartRepository.GetCartByUserId(userId);
            if (cart != null)
            {
                _cartRepository.ClearCart(cart.CartId);
            }
        }
    }
}
