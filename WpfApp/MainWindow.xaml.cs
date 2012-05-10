using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ViewModeler.Infrastructure;
using ViewModeler.Models;
using WpfApp;

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

            ViewModelBase.SetDispatcher(new DefaultDispatcher(Application.Current.Dispatcher));

            DataContext = new FormViewModel()
            {
                Title = "PostSharp testing",
                Text = "Hello"
            };
            //how to develop attached property that turn on/off validation on textboxes
            //var depProperty = TextBox.TextProperty;

            //var binding = titleTb.GetBindingExpression(depProperty).ParentBinding;

            //binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //binding.ValidatesOnDataErrors = true;
        }
    }
}
