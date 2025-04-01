using System.Linq.Expressions;

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T : class
    {
        protected Specifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>>? Criteria { get; }
        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();
        protected void AddInclude(Expression<Func<T, object>> includeExpression) 
            => IncludeExpressions.Add(includeExpression);
    }
}
// _dbContext.Set<T>().Where(Expression).Include().Orderby()
// Func<T, bool>
// Where Expression<Func<T, bool>>
// Include List<Expression<Func<T, object>>>