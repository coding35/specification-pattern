using System.Linq.Expressions;
using SpecificationPattern.Model;

namespace SpecificationPattern
{
    public class OrderWeightSpecification : Specification<Order>
    {
        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.TotalWeight < 100;
        }
    }


}