using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Shell;
using System.Windows.Input;
using ViewModel.Actions;

namespace ViewModel.Models
{
    public class FormViewModel: ViewModelBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public double BarValue { get; set; }
        public TaskbarItemProgressState BarState { get; set; }
        public bool TaskbarIconFlushes { get; set; }
        public ObservableCollection<int> Collection { get; set; }

        public ICommand Flash
        {
            get
            {
                return new Command(par => { TaskbarIconFlushes = !TaskbarIconFlushes; }, viewModelInstance: this);
            }
        }

        public ICommand Edit
        {
            get
            {
                return new ActionBase(
                    par =>
                        {
                            Collection = new ConcurrentObservableCollection<int> { 15 };
                            Task.Factory.StartNew(() =>
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    Collection.Add(i);
                                }
                            }).ContinueWith(prev => Messenger.SendMessage("All 100 items added OK from worker thread", "All good"));
                            Collection.Add(20);

                            //if (BarValue.Equals(0))
                            //{
                            //    BarState = TaskbarItemProgressState.Normal;
                            //}
                            //BarValue = BarValue + 0.05;
                            //if (BarValue >= 0.95)
                            //{
                            //    BarState = TaskbarItemProgressState.Error;
                            //}
                        });
            }
        }
    }
}
