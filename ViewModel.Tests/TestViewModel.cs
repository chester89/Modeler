using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Shell;
using ViewModel.Actions;
using ViewModel.Models;

namespace ViewModel.Tests
{
    public class TestViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public Double Value { get; set; }
        public ObservableCollection<int> List { get; set; }
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