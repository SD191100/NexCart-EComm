using NexCart.Models;

namespace NexCart.Repositories.Interfaces
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int orderId);
        void AddOrderDetail(OrderDetail orderDetail);
        void RemoveOrderDetail(int orderDetailId);
    }
}
