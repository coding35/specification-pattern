using NUnit.Framework;
using Specification;
using Specification.Model;

namespace Test;

public class AndSpecificationTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldPassAndSpecificationOperandsTrue()
    {
        var rule = new OrderRule()
        {
            AmountLimit = 100,
            WeightLimit = 100
        };
        
        // TODO Is there a better way to instantiate the specification?
        Specification<Order, OrderRule> spec = Specification<Order, OrderRule>.Init;
        spec = spec.And(new OrderAmountSpecification(rule));
        spec.And(new OrderWeightSpecification(rule));

        var order = new Order()
        {
            TotalWeight = 20M,
            TotalAmount = 20M
        };
        
        var test = spec.IsSatisfiedBy(order);
        
        Assert.True(test);
    }
    
    [Test]
    public void ShouldFailAndSpecificationOperandsFalse()
    {
        var rule = new OrderRule()
        {
            AmountLimit = 100,
            WeightLimit = 100
        };
        
        Specification<Order, OrderRule> spec = Specification<Order, OrderRule>.Init;
        spec = spec.And(new OrderAmountSpecification(rule));
        spec.And(new OrderWeightSpecification(rule));
        
        var order = new Order()
        {
            TotalWeight = 200M,
            TotalAmount = 200M
        };
        
        var test = spec.IsSatisfiedBy(order);
        
        Assert.False(test);
    }
    
    [Test]
    public void ShouldFailAndSpecificationOperandFalse()
    {
        var rule = new OrderRule()
        {
            AmountLimit = 20,
            WeightLimit = 100
        };
        
        Specification<Order, OrderRule> spec = Specification<Order, OrderRule>.Init;
        spec = spec.And(new OrderAmountSpecification(rule));
        spec.And(new OrderWeightSpecification(rule));
        
        var order = new Order()
        {
            TotalWeight = 20M,
            TotalAmount = 200M
        };
        
        var test = spec.IsSatisfiedBy(order);
        
        Assert.False(test);
    }
}