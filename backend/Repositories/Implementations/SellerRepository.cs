using NexCart.Data;
using NexCart.Models;
using NexCart.Repositories.Interfaces;

namespace NexCart.Repositories.Implementations
{
    public class SellerRepository : ISellerRepository
    {
        private readonly NexCartDBContext _context;

        public SellerRepository(NexCartDBContext context)
        {
            _context = context;
        }

        public Seller GetSellerById(int sellerId)
        {
            return _context.Sellers.FirstOrDefault(s => s.SellerId == sellerId);
        }

        //Seller GetSellerByEmail(string email)
        //{
        //    return _context.Sellers.FirstOrDefault(s => s.Email == sellerId);
        //}

        public Seller GetSellerByUserId(int userId)
        {
            return _context.Sellers.FirstOrDefault(s => s.UserId == userId);
        }

        public void AddSeller(Seller seller)
        {
            _context.Sellers.Add(seller);
            _context.SaveChanges();
        }

        public void UpdateSeller(Seller seller)
        {
            _context.Sellers.Update(seller);
            _context.SaveChanges();
        }
    }
}
