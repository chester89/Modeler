using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModeler.Actions;

namespace ViewModeler.Models
{
    public class FormViewModel: ViewModelBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public double BarValue { get; set; }
        //public TaskbarItemProgressState BarState { get; set; }
        public bool TaskbarIconFlushes { get; set; }
        public ObservableCollection<int> Collection { get; set; }

        public ICommand Flash
        {
            get
            {
                return new Command(par => { TaskbarIconFlushes = !TaskbarIconFlushes; });
            }
        }

        public ICommand Edit
        {
            get
            {
                return new ValidationCommand(
                    par =>
                        {
                            if (BarValue.Equals(0))
                            {
                                //BarState = TaskbarItemProgressState.Normal;
                            }
                            BarValue = BarValue + 0.05;
                            if (BarValue >= 0.95)
                            {
                                //BarState = TaskbarItemProgressState.Error;
                            }
                        });
            }
        }
    }
}
