using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfApp
{
    public class TaskbarFunctionality
    {
        public static DependencyProperty TaskBarIconFlushProperty = DependencyProperty.RegisterAttached("TaskBarIconFlush", 
            typeof(bool), typeof(TaskbarFunctionality), new FrameworkPropertyMetadata(false, OnPropertyChanged));

        static void OnPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            if ((bool)args.NewValue)
            {
                (source as Window).FlashWindow();
            }
            else
            {
                (source as Window).StopFlashingWindow();
            }
        }

        public static void SetTaskBarIconFlush(DependencyObject element, Object value)
        {
            element.SetValue(TaskBarIconFlushProperty, value);
        }

        public static bool GetTaskBarIconFlush(DependencyObject element)
        {
            return (bool)element.GetValue(TaskBarIconFlushProperty);
        }
    }
}