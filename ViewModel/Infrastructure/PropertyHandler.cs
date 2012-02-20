using System;
using System.Linq.Expressions;

namespace ViewModel.Infrastructure
{
    public class PropertyHandler : IExpressionHandler
    {
        private Expression expression;

        public bool CanHandle(Expression expression)
        {
            return (expression is MemberExpression || expression is LambdaExpression);
        }

        public string Handle(Expression source, string currentPath, string delimiter)
        {
            if (source is MemberExpression)
            {
                expression = source;
            }
            else
            {
                expression = (source as LambdaExpression).Body;
            }

            string memberName = string.Empty;
            if (expression is MemberExpression)
            {
                memberName = (expression as MemberExpression).Member.Name;
            }

            return delimiter + memberName + currentPath;
        }

        public Expression ToNext()
        {
            return (expression as MemberExpression).Expression;
        }
    }
}