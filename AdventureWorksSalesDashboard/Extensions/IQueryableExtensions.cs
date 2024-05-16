using System.Linq.Expressions;

namespace AdventureWorksSalesDashboard.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> WhereDynamic<T>(this IQueryable<T> query, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? combined = null;

            // Check each property of the type
            foreach (var property in typeof(T).GetProperties().Where(p => p.PropertyType == typeof(string)))
            {
                var propertyAccess = Expression.Property(parameter, property);
                var containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);
                var searchTextExpression = Expression.Constant(searchText, typeof(string));

                if (containsMethod is null)
                {
                    throw new Exception("Contains method not found. This should never happen :)");
                }

                // Create an expression to represent 'property.Contains(searchText)'
                var containsExpression = Expression.Call(propertyAccess, containsMethod, searchTextExpression);

                // Combine the expressions using 'Or' (||)
                combined = combined == null ? containsExpression : Expression.OrElse(combined, containsExpression);
            }

            if (combined == null)
            {
                return query;
            }

            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

            // Apply the lambda expression as a filter to the IQueryable
            return query.Where(lambda);
        }
    }
}
