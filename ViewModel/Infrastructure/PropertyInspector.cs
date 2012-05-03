using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ViewModel.Infrastructure
{
    public class PropertyInspector
    {
        private readonly ICollection<IExpressionHandler> handlers = new List<IExpressionHandler>();

        private const string delimiter = ".";

        public PropertyInspector()
        {
            ScanForImplementationsOf<IExpressionHandler>().ToList().ForEach(type =>
                                                                            {
                                                                                dynamic instance = Activator.CreateInstance(type);
                                                                                handlers.Add(instance);
                                                                            });
        }

        /// <summary>
        /// Scans current assembly for implementations of requested interface
        /// </summary>
        /// <typeparam name="T">interface</typeparam>
        private IEnumerable<Type> ScanForImplementationsOf<T>()
        {
            return typeof(T).Assembly.GetTypes().Where(
                t => t.FindInterfaces((type, obj) => typeof(T).IsAssignableFrom(type), typeof(T).FullName).Any());
        }

        public string PathFor<T, TR>(Expression<Func<T, TR>> expression)
        {
            string path = string.Empty;

            Expression currentExpression = expression;

            while (currentExpression.NodeType != ExpressionType.Parameter && currentExpression.NodeType != ExpressionType.Default)
            {
                var handler = handlers.SingleOrDefault(h => h.CanHandle(currentExpression));
                if (handler != null)
                {
                    path = handler.Handle(currentExpression, path, delimiter);
                    currentExpression = handler.ToNext();
                }
                else
                {
                    throw new ArgumentException(string.Format("Can't handle expression {0}", expression));
                }
            }

            return path.Any() ? path.Substring(1): path;
        }
    }
}
