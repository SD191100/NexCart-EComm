using NexCart.Models;

namespace NexCart.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrderById(int orderId);
        IEnumerable<Order> GetOrdersByUserId(int userId);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
        Task<Order> SaveOrderAsync(Order order);
        Task<List<OrderDetail>> SaveOrderDetailsAsync(List<OrderDetail> orderDetails);
    }
}
