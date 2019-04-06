using System.Collections.Generic;

namespace TellDontAskKata.Domain
{
    public class Order
    {
        public decimal Total { get; set; }
        public string Currency { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal Tax { get; set; }
        public OrderStatus Status { get; set; }
        public int Id { get; set; }

        public static Order InitialOrder(OrderStatus orderStatus)
        {
            return new Order { Status = orderStatus, Id = 1 };
        }

        public static void DefaultOrder(Order order)
        {
            order.Status = OrderStatus.Created;
            order.Items = new List<OrderItem>();
            order.Currency = "EUR";
            order.Total = 0.0m;
            order.Tax = 0.0m;
        }
    }
}