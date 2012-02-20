using System;
using System.Collections.Generic;
using System.Linq;
using PostSharp.Aspects;

namespace ViewModel.Aspects
{
    [Serializable]
    public sealed class BackgroundThreadAspect: MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            new Action(args.Proceed).BeginInvoke(null, null);
        }
    }
}
