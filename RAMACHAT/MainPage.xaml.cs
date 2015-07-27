using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RAMACHAT.SocketIO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using RAMACHAT.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
namespace RAMACHAT
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        public string _reuserid = "";
        public MainPage()
        {
            InitializeComponent();
            Dispatcher dispatcher = Deployment.Current.Dispatcher;
            dispatcher.BeginInvoke(() =>
            {
                App.connectView.connectSocket();
            });
            
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            //App.ViewModel.Items.Add(new ViewModels.ItemViewModel() { SenderID = "aaa", CreateAt = "aaaa", MessageText = "AAA" });
        }
        public GetFriendResponse resultObject { get; set; }
        // Load data for the ViewModel Items
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            string result = await App.client.getAllFriends();
            resultObject = JsonConvert.DeserializeObject<GetFriendResponse>(result);
            friendList.ItemsSource = resultObject.data;
        }

        

        private void ContactListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ContactListBox.Items.Clear();
            if (friendList.SelectedItem != null && friendList != null)
            {
                User Item = (User)friendList.SelectedItem;
                App._reuserid = Item._id;
                NavigationService.Navigate(new Uri("/ChatPage.xaml", UriKind.Relative));
            }{ }
           friendList.SelectedItem = null;
        }
        
    }
}