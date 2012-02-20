using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace ViewModel.Infrastructure
{
    public interface IDispatcher
    {
        void Invoke(Delegate method, params object[] args);
    }

    public class DefaultDispatcher: IDispatcher
    {
        private readonly Dispatcher applicationDispatcher;

        public DefaultDispatcher(Dispatcher applicationDispatcher)
        {
            this.applicationDispatcher = applicationDispatcher;
        }

        public void Invoke(Delegate method, params object[] args)
        {
            applicationDispatcher.Invoke(method, args);
        }
    }
}
