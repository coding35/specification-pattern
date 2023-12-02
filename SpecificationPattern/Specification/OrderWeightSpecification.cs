using System.Linq.Expressions;
using Specification.Model;

namespace Specification
{
    public class OrderWeightSpecification : Specification<Order, OrderRule>
    {
        private readonly OrderRule _orderRule;

        public OrderWeightSpecification(OrderRule orderRule)
        {
            _orderRule = orderRule;
        }
        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.TotalWeight < _orderRule.WeightLimit;
        }
    }


}