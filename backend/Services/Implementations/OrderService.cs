using NexCart.DTOs.Checkout;
using NexCart.DTOs;
using NexCart.DTOs.Order;
using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;

namespace NexCart.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository, IEmailService emailService, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public OrderDto GetOrderById(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null) return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            };
        }

        public IEnumerable<OrderDto> GetOrdersByUserId(int userId)
        {
            var orders = _orderRepository.GetOrdersByUserId(userId);

            return orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            });
        }

        public void PlaceOrder(CreateOrderDto createOrderDto)
        {

            var UId = createOrderDto.UserId;
            var user = _userRepository.GetUserById(UId);
            if (user == null)
                throw new InvalidOperationException("User not found");
            var order = new Order
            {
                UserId = UId,


                OrderDate = DateTime.UtcNow,
                OrderDetails = createOrderDto.OrderDetails.Select(od => new OrderDetail
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            };

            _orderRepository.AddOrder(order);

            var subject = "Order Confirmation - NexCart";
            var body = $@"
                <h2>Thank you for your order, {user.FirstName}!</h2>
                <p>Your order has been placed successfully.</p>
                <p><strong>Order ID:</strong> {order.OrderId}</p>
                <p><strong>Total Amount:</strong> ${order.TotalAmount}</p>
                <p>We will notify you when your order is shipped.</p>
                <br/>
                <p>Best Regards,</p>
                <p><strong>NexCart Team</strong></p>";

            _emailService.SendEmail(user.Email, subject, body);
        }

        public async Task<List<OrderDTO>> GetUserOrderHistoryAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders.Select(o => new OrderDTO
            {
                Id = o.OrderId,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderItems = o.OrderDetails.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            }).ToList();
        }

        public async Task<OrderResponseDTO> CheckoutAsync(CheckoutRequestDTO checkoutRequest)
        {
            var totalCost = await CalculateTotalCostAsync(checkoutRequest.CartItems);
            // Return a summary for the frontend to display the total and other details.
            return new OrderResponseDTO { Status = "pending", TotalAmount = totalCost, OrderDate = DateTime.Now };
        }

        public async Task<bool> ProcessPaymentAsync(PaymentRequestDTO paymentRequest)
        {
            // Simulate payment processing
            // If payment is successful:

            return true;
        }

        public async Task<OrderResponseDTO> ConfirmOrderAsync(OrderConfirmationDTO confirmationRequest)
        {
            var UId = confirmationRequest.UserId;
            var user = _userRepository.GetUserById(UId);
            if (user == null)
                throw new InvalidOperationException("User not found");

            var order = new Order
            {
                UserId = confirmationRequest.UserId,
                TotalAmount = confirmationRequest.TotalAmount,
                OrderDate = DateTime.UtcNow,
                PaymentID = confirmationRequest.PaymentID
            };

            var savedOrder = await _orderRepository.SaveOrderAsync(order);

            var orderDetails = confirmationRequest.CartItems.Select(ci => new OrderDetail
            {
                OrderId = savedOrder.OrderId,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                Price = _productRepository.GetProductById(ci.ProductId).Price
            }).ToList();

            await _orderRepository.SaveOrderDetailsAsync(orderDetails);

            var subject = "Order Confirmation - NexCart";
            var body = $@"
                <h2>Thank you for your order, {user.FirstName}!</h2>
                <p>Your order has been placed successfully.</p>
                <p><strong>Order ID:</strong> {order.OrderId}</p>
                <p><strong>Total Amount:</strong> ${order.TotalAmount}</p>
                <p>We will notify you when your order is shipped.</p>
                <br/>
                <p>Best Regards,</p>
                <p><strong>NexCart Team</strong></p>";

            _emailService.SendEmail(user.Email, subject, body);


            return new OrderResponseDTO
            {
                OrderId = savedOrder.OrderId,
                OrderDate = savedOrder.OrderDate,
                TotalAmount = savedOrder.TotalAmount.Value,
                OrderDetails = orderDetails.Select(od => new OrderDetailDTO
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            };
        }

        private async Task<decimal> CalculateTotalCostAsync(List<CartItemDto> cartItems)
        {
            decimal totalCost = 0;

            foreach (var item in cartItems)
            {
                var product = _productRepository.GetProductById(item.ProductId);
                totalCost += product.Price * item.Quantity;
            }

            return totalCost;
        }
    }
}
