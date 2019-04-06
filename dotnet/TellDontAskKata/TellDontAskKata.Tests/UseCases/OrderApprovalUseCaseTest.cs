using System;

using TellDontAskKata.Domain;
using TellDontAskKata.Tests.Doubles;
using TellDontAskKata.UseCase;

using Xunit;

namespace TellDontAskKata.Tests.UseCases
{
    public class OrderApprovalUseCaseTest
    {
        private TestOrderRepository orderRepository;

        private OrderApprovalUseCase useCase;

        public OrderApprovalUseCaseTest()
        {
            orderRepository = new TestOrderRepository();
            useCase = new OrderApprovalUseCase(orderRepository);
        }

        [Fact]
        public void ApprovedExistingOrder()
        {
            Order initialOrder = Order.InitialOrder(OrderStatus.Created);
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = true };

            useCase.Run(request);

            Assert.Equal(orderRepository.SavedOrder.Status, OrderStatus.Approved);
        }

        [Fact]
        public void RejectExistingOrder()
        {
            Order initialOrder = Order.InitialOrder(OrderStatus.Created);
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = false };

            useCase.Run(request);

            Assert.Equal(orderRepository.SavedOrder.Status, OrderStatus.Rejected);
        }

        [Fact]
        public void CannotApproveRejectedOrderBy()
        {
            Order initialOrder = Order.InitialOrder(OrderStatus.Rejected);
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = true };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<RejectedOrderCannotBeApprovedException>(runAction);
        }

        [Fact]
        public void CannotRejectApprovedOrder()
        {
            Order initialOrder = Order.InitialOrder(OrderStatus.Approved);
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = false };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<ApprovedOrderCannotBeRejectedException>(runAction);
        }

        [Fact]
        public void ShippedOrdersCannotBeApproved()
        {
            Order initialOrder = Order.InitialOrder(OrderStatus.Shipped);
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = true };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<ShippedOrdersCannotBeChangedException>(runAction);
        }

        [Fact]
        public void ShippedOrdersCannotBeRejected()
        {
            Order initialOrder = Order.InitialOrder(OrderStatus.Shipped);
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = false };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<ShippedOrdersCannotBeChangedException>(runAction);
        }
    }
}