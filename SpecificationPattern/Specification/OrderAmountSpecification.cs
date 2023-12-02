using System.Linq.Expressions;
using Specification.Model;

namespace Specification
{
    public class OrderAmountSpecification : Specification<Order, OrderRule>
    {
        private readonly OrderRule _orderRule;

        public OrderAmountSpecification(OrderRule orderRule)
        {
            _orderRule = orderRule;
        }
        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.TotalAmount < _orderRule.AmountLimit; 
        }
    }
}