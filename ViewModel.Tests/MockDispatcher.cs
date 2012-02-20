using System;
using ViewModel.Infrastructure;

namespace ViewModel.Tests
{
    public class MockDispatcher: IDispatcher
    {
        public void Invoke(Delegate method, params object[] args)
        {
            method.Method.Invoke(method.Target, args);
        }
    }
}
