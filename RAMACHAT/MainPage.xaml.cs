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
using System.Windows;
using System.Windows.Threading;
namespace RAMACHAT
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        public string _username = "";
        public string _token = "";
        public string _userid = "";
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
           // App.ViewModel.LoadData();
            if (NavigationContext.QueryString.TryGetValue("username", out _username))
            {
                NavigationContext.QueryString.TryGetValue("token", out _token);
                
                NavigationContext.QueryString.TryGetValue("userid", out _userid);
                string result = await App.client.getAllFriends();
                resultObject = JsonConvert.DeserializeObject<GetFriendResponse>(result);
                friendList.ItemsSource = resultObject.data;
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(App.ViewModel.Items.Count().ToString());

        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                
                JArray memberArray = new JArray();
                memberArray.Add(this._userid);
                memberArray.Add(this._reuserid);
                App.connectView.sendMesS(this._userid, false, enter.Text, 1, this._username, memberArray, DateTime.Now.ToShortDateString());
                enter.Text = "";
                ContactListBox.SelectedIndex = App.ViewModel.Items.Count()-1;
                ContactListBox.Focus();
            }
        }

        private async void ContactListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ContactListBox.Items.Clear();
            App.ViewModel.Items.Clear();
            if (friendList.SelectedItem != null && friendList != null)
            {
                User Item = (User)friendList.SelectedItem;
                this._reuserid = Item._id;
                string result = await App.client.getRoomMessagesByUserId(Item._id);
                Debug.WriteLine(result);
                MessageObject resultObject = JsonConvert.DeserializeObject<MessageObject>(result);
                foreach(var message in resultObject.data)
                {
                    App.ViewModel.Items.Add(new ViewModels.ItemViewModel() { Avatar = new Uri(message._userId.avatar), CreateAt = message.createdAt, MessageText = message.message, SenderID = message._userId._id });
                }
            }{ }
           // friendList.SelectedItem = null;
        }
        
    }
}