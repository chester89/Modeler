using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using ViewModel.Infrastructure;
using ViewModel.Models;

namespace WpfPostSharpTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new FormViewModel()
                              {
                                  Title = "PostSharp testing",
                                  Text = "Hello"
                              };

            ViewModelBase.SetDispatcher(new DefaultDispatcher(Application.Current.Dispatcher));

            //how to develop attached property that turn on/off validation on textboxes
            //var depProperty = TextBox.TextProperty;

            //var binding = titleTb.GetBindingExpression(depProperty).ParentBinding;

            //binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //binding.ValidatesOnDataErrors = true;
        }
    }
}
