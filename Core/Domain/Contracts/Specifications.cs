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
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        protected void AddInclude(Expression<Func<T, object>> expression) 
            => IncludeExpressions.Add(expression);
        protected void AddOrderBy(Expression<Func<T, object>> expression)
            => OrderBy = expression;
        protected void AddOrderByDescending(Expression<Func<T, object>> expression)
            => OrderByDescending = expression;
    }
}
// _dbContext.Set<T>().Where(Expression).Include().Orderby()
// Func<T, bool>
// Where Expression<Func<T, bool>>
// Include List<Expression<Func<T, object>>>
// OrderBy Expression<Func<T, object>>
// OrderByDescending Expression<Func<T, object>>