using System;
using System.Collections.Generic;
using System.ComponentModel;
using MvvmExample.Dto;
using MvvmExample.Infrastrucutre;
using RestSharp;

namespace MvvmExample.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IList<Item> _posts;

        public IList<Item> Posts
        {
            get { return _posts; }
        }
        public DelegateCommand OpenDetailCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            OpenDetailCommand = new DelegateCommand(ExecuteOpenDetailCommand);

            LoadFeed(@"http://www.webdebs.org/category/eventi/feed/");
        }

        private void ExecuteOpenDetailCommand()
        {
            App.Container.Resolve<INavigationService>().NavigateTo(new Uri("/View/DetailView.xaml", UriKind.RelativeOrAbsolute));
            
        }

        private void LoadFeed(string url)
        {
            RestClient client = new RestClient
            {
                BaseUrl = url
            };
            var request = new RestRequest { RequestFormat = DataFormat.Xml };

            client.ExecuteAsync<List<Item>>(request, response =>
                                                         {
                                                        
                _posts = response.Data;
                                                             OnPropertyChanged("Posts");
            });
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        
    }
}