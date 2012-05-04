using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ViewModel.Infrastructure;

namespace ViewModel.TestUtil
{
    public static class SubscriberExtensions
    {
        public static PropertySubscriber<T> CreateSubscriber<T>(T viewModel) where T: INotifyPropertyChanged
        {
            return new PropertySubscriber<T>(viewModel);
        }
    }

    public class PropertySubscriber<T> where T: INotifyPropertyChanged
    {
        private T viewModel;

        public PropertySubscriber(T viewModel)
        {
            this.viewModel = viewModel;
        }

        public void SubscribeTo<TProperty>(Expression<Func<T, TProperty>> vmExpression, Action action)
        {
            string propertyName = new PropertyInspector().PathFor(vmExpression);
            viewModel.PropertyChanged += (e, a) =>
                                             {
                                                 if (a.PropertyName == propertyName)
                                                 {
                                                     action();
                                                 }
                                             };
        }

        public void SubscribeTo<TProperty>(Expression<Func<T, TProperty>> vmExpression, Action<TProperty> action)
        {
            string propertyName = new PropertyInspector().PathFor(vmExpression);
            viewModel.PropertyChanged += (e, a) =>
            {
                if (a.PropertyName == propertyName)
                {
                    var newPropertyValue = vmExpression.Compile()(viewModel);
                    action(newPropertyValue);
                }
            };
        }
    }
}
