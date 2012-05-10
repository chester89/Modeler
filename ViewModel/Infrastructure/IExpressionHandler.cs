using System.Linq.Expressions;

namespace ViewModeler.Infrastructure
{
    interface IExpressionHandler
    {
        bool CanHandle(Expression expression);
        string Handle(Expression source, string currentPath, string delimiter);
        Expression ToNext();
    }
}