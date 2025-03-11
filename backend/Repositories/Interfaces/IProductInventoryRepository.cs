using NexCart.Models;

namespace NexCart.Repositories.Interfaces
{
    public interface IProductInventoryRepository
    {
        ProductInventory GetInventoryByProductId(int productId);
        void UpdateInventory(ProductInventory inventory);
    }
}
