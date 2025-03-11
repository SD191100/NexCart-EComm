using NexCart.Models;

namespace NexCart.Services.Interfaces
{
    public interface IProductInventoryService
    {
        ProductInventory GetInventoryByProductId(int productId);
        void UpdateInventory(ProductInventory inventory);
        void ReduceStock(int productId, int quantity);
        void IncreaseStock(int productId, int quantity);
    }
}
