using System.Linq.Expressions;

namespace Specification
{
    internal sealed class IdentitySpecification<T, TRule> : Specification<T, TRule>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }

    public abstract class Specification<T, TRule>
    {
        public static readonly Specification<T, TRule> Init = new IdentitySpecification<T, TRule>();
        public static readonly Specification<T, TRule> Init2;
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T, TRule> And(Specification<T, TRule> specification)
        {
            if (this == Init)
                return specification;
            if (specification == Init)
                return this;

            return new AndSpecification<T, TRule>(this, specification);
        }

        public Specification<T, TRule> Or(Specification<T, TRule> specification)
        {
            if (this == Init || specification == Init) // any of them is true, ie. one operand is true
                return Init;

            return new OrSpecification<T, TRule>(this, specification);
        }

        public Specification<T,TRule> Not()
        {
            return new NotSpecification<T, TRule>(this);
        }
    }


    internal sealed class AndSpecification<T, TRule> : Specification<T, TRule>
    {
        private readonly Specification<T, TRule> _left;
        private readonly Specification<T, TRule> _right;

        public AndSpecification(Specification<T, TRule> left, Specification<T, TRule> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }


    internal sealed class OrSpecification<T, TRule> : Specification<T, TRule>
    {
        private readonly Specification<T, TRule> _left;
        private readonly Specification<T, TRule> _right;

        public OrSpecification(Specification<T, TRule> left, Specification<T, TRule> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            BinaryExpression orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
        }
    }


    internal sealed class NotSpecification<T, TRule> : Specification<T, TRule>
    {
        private readonly Specification<T, TRule> _specification;

        public NotSpecification(Specification<T, TRule> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = _specification.ToExpression();
            UnaryExpression notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}
