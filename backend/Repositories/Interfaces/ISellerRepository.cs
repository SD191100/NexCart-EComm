using NexCart.Models;

namespace NexCart.Repositories.Interfaces
{
    public interface ISellerRepository
    {
        Seller GetSellerById(int sellerId);
        //Seller GetSellerByEmail(int sellerId);
        Seller GetSellerByUserId(int userId);
        void AddSeller(Seller seller);
        void UpdateSeller(Seller seller);
    }
}
