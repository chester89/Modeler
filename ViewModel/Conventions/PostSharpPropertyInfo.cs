﻿using System;
using PostSharp.Aspects;
using ViewModel.Models;

namespace ViewModel.Conventions
{
    class PostSharpPropertyInfo : IPropertyInfo
    {
        private readonly LocationInterceptionArgs args;

        public PostSharpPropertyInfo(LocationInterceptionArgs args)
        {
            this.args = args;
        }

        #region IPropertyInfo Members

        public object PropertyValue
        {
            get { return args.Value; }
            set { args.Value = value; }
        }

        public object CurrentValue
        {
            get { return args.GetCurrentValue(); }
        }

        public Type PropertyType
        {
            get { return args.Location.PropertyInfo.PropertyType; }
            set { throw new ArgumentException(); }
        }

        public string PropertyName
        {
            get { return args.Location.Name; }
        }

        public void ProceedGet()
        {
            args.ProceedGetValue();
        }

        public void ProceedSet()
        {
            args.ProceedSetValue();
        }

        public ViewModelBase Instance
        {
            get { return args.Instance as ViewModelBase; }
        }

        #endregion
    }
}