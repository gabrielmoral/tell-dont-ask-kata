using System.Collections.Generic;

namespace TellDontAskKata.Domain
{
    public class Order
    {
        public Order()
        {
            Status = OrderStatus.Created;
            Items = new List<OrderItem>();
            Currency = "EUR";
            Total = 0.0m;
            Tax = 0.0m;
        }
        
        public decimal Total { get; set; }
        public string Currency { get; }
        public List<OrderItem> Items { get; }
        public decimal Tax { get; set; }
        public OrderStatus Status { get; set; }
        public int Id { get; private set; }

        public static Order InitialOrder(OrderStatus orderStatus)
        {
            return new Order { Status = orderStatus, Id = 1 };
        }
    }
}