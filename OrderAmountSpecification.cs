using System.Linq.Expressions;
using SpecificationPattern;
using SpecificationPattern.Model;

namespace OrderAmountSpecification
{
    public class OrderAmountSpecification : Specification<Order>
    {
        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.TotalAmount < 100; 
        }
    }
}