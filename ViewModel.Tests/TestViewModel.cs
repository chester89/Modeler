using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
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
    }
}