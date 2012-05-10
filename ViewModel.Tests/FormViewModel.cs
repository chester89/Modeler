using System.Windows.Shell;
using System.Windows.Input;
using ViewModeler.Actions;
using ViewModeler.Models;

namespace ViewModeler.Tests
{
    public class FormViewModel: ViewModelBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public double BarValue { get; set; }
        public TaskbarItemProgressState BarState { get; set; }
        public bool TaskbarIconFlushes { get; set; }

        public ICommand Flash
        {
            get
            {
                var command = new Command(par => { TaskbarIconFlushes = !TaskbarIconFlushes; });
                return command;
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
                                BarState = TaskbarItemProgressState.Normal;
                            }
                            BarValue = BarValue + 0.05;
                            if (BarValue >= 0.95)
                            {
                                BarState = TaskbarItemProgressState.Error;
                            }
                        });
            }
        }
    }
}
