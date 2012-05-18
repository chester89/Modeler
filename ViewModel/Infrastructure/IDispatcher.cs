using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace ViewModeler.Infrastructure
{
    /// <summary>
    /// Provides abstraction over <see cref="Dispatcher"/>
    /// </summary>
    public interface IDispatcher
    {
        void Invoke(Delegate method, params object[] args);
        bool OnUiThread { get; }
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

        public bool OnUiThread
        {
            get { return applicationDispatcher.CheckAccess(); }
        }
    }
}
