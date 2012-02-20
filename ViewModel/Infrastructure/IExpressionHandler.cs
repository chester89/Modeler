using System.Linq.Expressions;

namespace ViewModel.Infrastructure
{
    interface IExpressionHandler
    {
        bool CanHandle(Expression expression);
        string Handle(Expression source, string currentPath, string delimiter);
        Expression ToNext();
    }
}