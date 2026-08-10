namespace OrderService.Domain.Specification;

public static class SpecificationExtensions
{
    public static IQueryable<TEntity> Specify<TEntity>(this IQueryable<TEntity> query, Specification<TEntity> specification)
    {
        var queryable = query;

        if (specification.OrderByExpression is not null)
        {
            var orderedQuery = queryable.OrderBy(specification.OrderByExpression);
            if (specification.ThenOrderByExpression is not null) queryable = orderedQuery.ThenBy(specification.ThenOrderByExpression);
            else if (specification.ThenOrderByDescendingExpression is not null) queryable = orderedQuery.ThenByDescending(specification.ThenOrderByDescendingExpression);
            else queryable = orderedQuery;
        }

        if (specification.OrderByDescendingExpression is not null)
        {
            var orderedQuery = queryable.OrderBy(specification.OrderByDescendingExpression);
            if (specification.ThenOrderByExpression is not null) queryable = orderedQuery.ThenBy(specification.ThenOrderByExpression);
            else if (specification.ThenOrderByDescendingExpression is not null) queryable = orderedQuery.ThenByDescending(specification.ThenOrderByDescendingExpression);
            else queryable = orderedQuery;
        }

        if (specification.OrderByDescendingExpression is not null)
        {
            queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
        }

        return queryable;
    }
}
