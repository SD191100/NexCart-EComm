using NexCart.Data;
using NexCart.Models;
using NexCart.Repositories.Interfaces;

namespace NexCart.Repositories.Implementations
{
    public class ProductInventoryRepository : IProductInventoryRepository
    {
        private readonly NexCartDBContext _context;

        public ProductInventoryRepository(NexCartDBContext context)
        {
            _context = context;
        }

        public ProductInventory GetInventoryByProductId(int productId)
        {
            return _context.ProductInventories.FirstOrDefault(pi => pi.ProductId == productId);
        }

        public void UpdateInventory(ProductInventory inventory)
        {
            _context.ProductInventories.Update(inventory);
            _context.SaveChanges();
        }
    }
}
