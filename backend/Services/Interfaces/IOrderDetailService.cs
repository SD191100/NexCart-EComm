using NexCart.Models;

namespace NexCart.Services.Interfaces
{
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int orderId);
        void AddOrderDetail(OrderDetail orderDetail);
        void RemoveOrderDetail(int orderDetailId);
    }
}
