using System;
using MvvmExample.Infrastrucutre;

namespace MvvmExample.ViewModel
{
    public class MainViewModel
    {
        public DelegateCommand OpenDetailCommand { get; set; }

        public MainViewModel()
        {
            OpenDetailCommand = new DelegateCommand(ExecuteOpenDetailCommand);
        }

        private void ExecuteOpenDetailCommand()
        {
            App.Container.Resolve<INavigationService>().NavigateTo(new Uri("/View/DetailView.xaml",UriKind.RelativeOrAbsolute));
        }
    }
}