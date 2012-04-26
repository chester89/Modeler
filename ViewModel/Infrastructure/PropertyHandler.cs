using System;
using System.Linq.Expressions;

namespace ViewModel.Infrastructure
{
    public class PropertyHandler : IExpressionHandler
    {
        private Expression expr;

        public bool CanHandle(Expression expression)
        {
            return ((expression is MemberExpression || expression is LambdaExpression) && !(expression is MethodCallExpression));
        }

        public string Handle(Expression source, string currentPath = "", string delimiter = ".")
        {
            if (source is MemberExpression)
            {
                expr = source;
            }
            else
            {
                expr = (source as LambdaExpression).Body;
            }

            string memberName = string.Empty;
            if (expr is MemberExpression)
            {
                memberName = (expr as MemberExpression).Member.Name;
            }

            return delimiter + memberName + currentPath;
        }

        public Expression ToNext()
        {
            if (expr != null && expr is MemberExpression)
            {
                return (expr as MemberExpression).Expression;
            }
            return Expression.Empty();
        }
    }
}