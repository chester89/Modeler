using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Actions
{
    public class ExitContext
    {
        private List<Exception> exceptionsHappened;

        public ExitContext()
        {
            exceptionsHappened = new List<Exception>();
        }

        public void AddException(Exception exception)
        {
            exceptionsHappened.Add(exception);
        }

        public bool ExceptionsOccured 
        { 
            get { return exceptionsHappened.Any(); } 
        }
    }
}
