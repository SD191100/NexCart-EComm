using NexCart.DTOs.Order;
using System;
using System.Collections.Generic;

namespace NexCart.DTOs.Order
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
