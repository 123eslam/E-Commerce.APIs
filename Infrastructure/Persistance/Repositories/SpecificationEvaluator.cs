using Domain.Contracts;

namespace Persistance.Repositories
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T> 
            (IQueryable<T> inputQuery ,Specifications<T> specifications)
            where T : class
        {
            var query = inputQuery;
            if (specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);
            //foreach(var item in specifications.IncludeExpressions)
            //{
            //    query = query.Include(item);
            //}
            query = specifications.IncludeExpressions.Aggregate(
                query,
                (currentQuery, includeExpresion) => currentQuery.Include(includeExpresion));
            return query;
        }
    }
}
