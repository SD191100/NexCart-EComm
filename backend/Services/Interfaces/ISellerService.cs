using NexCart.Models;

namespace NexCart.Services.Interfaces
{
    public interface ISellerService
    {
        Seller GetSellerById(int sellerId);
        Seller GetSellerByUserId(int userId);
        void AddSeller(Seller seller);
        void UpdateSeller(Seller seller);
    }
}
