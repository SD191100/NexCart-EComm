using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;

namespace NexCart.Services.Implementations
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _sellerRepository;

        public SellerService(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }

        public Seller GetSellerById(int sellerId)
        {
            return _sellerRepository.GetSellerById(sellerId);
        }

        public Seller GetSellerByUserId(int userId)
        {
            return _sellerRepository.GetSellerByUserId(userId);
        }

        public void AddSeller(Seller seller)
        {
            _sellerRepository.AddSeller(seller);
        }

        public void UpdateSeller(Seller seller)
        {
            _sellerRepository.UpdateSeller(seller);
        }
    }
}
