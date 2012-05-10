using System;
using ViewModeler.Infrastructure;

namespace ViewModeler.Tests
{
    public class MockDispatcher: IDispatcher
    {
        public void Invoke(Delegate method, params object[] args)
        {
            method.Method.Invoke(method.Target, args);
        }

        public bool OnUiThread
        {
            get { return true; }
        }
    }
}
