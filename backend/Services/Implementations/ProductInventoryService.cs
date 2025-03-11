using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;

namespace NexCart.Services.Implementations
{
    public class ProductInventoryService : IProductInventoryService
    {
        private readonly IProductInventoryRepository _inventoryRepository;

        public ProductInventoryService(IProductInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public ProductInventory GetInventoryByProductId(int productId)
        {
            return _inventoryRepository.GetInventoryByProductId(productId);
        }

        public void UpdateInventory(ProductInventory inventory)
        {
            _inventoryRepository.UpdateInventory(inventory);
        }

        public void ReduceStock(int productId, int quantity)
        {
            var inventory = _inventoryRepository.GetInventoryByProductId(productId);
            if (inventory == null)
                throw new InvalidOperationException("Product inventory not found.");

            if (inventory.QuantityInStock < quantity)
                throw new InvalidOperationException("Insufficient stock.");

            inventory.QuantityInStock -= quantity;
            _inventoryRepository.UpdateInventory(inventory);
        }

        public void IncreaseStock(int productId, int quantity)
        {
            var inventory = _inventoryRepository.GetInventoryByProductId(productId);
            if (inventory == null)
                throw new InvalidOperationException("Product inventory not found.");

            inventory.QuantityInStock += quantity;
            _inventoryRepository.UpdateInventory(inventory);
        }
    }
}
