using NUnit.Framework;
using Specification;
using Specification.Model;

namespace Test;

public class OrSpecificationTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldPassOrSpecificationOperandsTrue()
    {
        var rule = new OrderRule()
        {
            AmountLimit = 100,
            WeightLimit = 100
        };
        
        // TODO Is there a better way to instantiate the specification?
        Specification<Order, OrderRule> spec = Specification<Order, OrderRule>.Init;
        spec.And(new OrderAmountSpecification(rule).Or(new OrderWeightSpecification(rule)));
        
        var order = new Order()
        {
            TotalWeight = 2M,
            TotalAmount = 2M
        };
        
        var test = spec.IsSatisfiedBy(order);
        
        Assert.True(test);
    }
    
    [Test]
    public void ShouldFailOrSpecificationOperandsFalse()
    {
        var rule = new OrderRule()
        {
            AmountLimit = 100,
            WeightLimit = 100
        };
        
        Specification<Order, OrderRule> spec = Specification<Order, OrderRule>.Init;
        spec = spec.Or(new OrderAmountSpecification(rule));
        spec.Or(new OrderWeightSpecification(rule));
        
        var order = new Order()
        {
            TotalWeight = 200M,
            TotalAmount = 200M
        };
        
        var test = spec.IsSatisfiedBy(order);
        
        Assert.True(test);
    }
    
    [Test]
    public void ShouldOrAndSpecificationOperandFalse()
    {
        var rule = new OrderRule()
        {
            AmountLimit = 20,
            WeightLimit = 100
        };
        
        Specification<Order, OrderRule> spec = Specification<Order, OrderRule>.Init;
        spec = spec.Or(new OrderAmountSpecification(rule));
        spec.Or(new OrderWeightSpecification(rule));
        
        var order = new Order()
        {
            TotalWeight = 200M,
            TotalAmount = 200M
        };
        
        var test = spec.IsSatisfiedBy(order);
        
        Assert.True(test);
    }
}