using System;
using System.Linq;
using System.Linq.Expressions;

namespace ViewModel.Infrastructure
{
    public class MethodHandler : IExpressionHandler
    {
        private Expression expression;

        public bool CanHandle(Expression expression)
        {
            return expression is MethodCallExpression;
        }

        public string Handle(Expression source, string currentPath, string delimiter)
        {
            string path = string.Empty;
            expression = source;

            var methodCall = expression as MethodCallExpression;

            var methodName = methodCall.Method.Name;
            if (methodName == "get_Item") //it's a dictionary
            {
                var firstArgument = methodCall.Arguments.First();
                path = "[" + (firstArgument as ConstantExpression).Value + "]" + currentPath;
            }
            else
            {
                throw new Exception("Method calls are not supported");
            }

            return path;
        }

        public Expression ToNext()
        {
            return (expression as MethodCallExpression).Object;
        }
    }
}