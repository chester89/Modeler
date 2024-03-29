using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModeler.Actions;
using ViewModeler.Models;

namespace ViewModeler.Tests
{
    public class TestViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public Double Value { get; set; }
        public ObservableCollection<int> List { get; set; }
        public ICollection<double> Something { get; set; }
        public ICommand Edit { get; set; }
        public DateTime SomeDate { get; set; }
        public ICommand ForProperties 
        { 
            get
            {
                return new ActionBase(
                    par =>
                        {
                            Message += "Hello";
                        }, param => Id > 0);
            } 
        }
    }
}