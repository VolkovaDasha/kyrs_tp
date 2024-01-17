using System.Linq.Expressions;

namespace BookMarketWeb.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> firstExpression, Expression<Func<T, bool>> secondExpression)
    {
        if (firstExpression is null)
        {
            return secondExpression;
        }

        if (secondExpression is null)
        {
            return firstExpression;
        }

        var invokedExpression = Expression.Invoke(secondExpression, firstExpression.Parameters);

        var combinedExpression = Expression.AndAlso(firstExpression.Body, invokedExpression);

        return Expression.Lambda<Func<T, bool>>(combinedExpression, firstExpression.Parameters);
    }
}