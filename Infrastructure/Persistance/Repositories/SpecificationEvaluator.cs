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

            if (specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);
            else if (specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);
            if (specifications.IsPaginated)
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            return query;
        }
    }
}
