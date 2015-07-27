using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using Newtonsoft.Json;
using RAMACHAT.Model;
using System.Diagnostics;

namespace RAMACHAT
{
    public partial class ChatPage : PhoneApplicationPage
    {
        public string _reuserid = "";
        public ChatPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
                try
                {
                    string userInfo = await App.client.getUserById(App._reuserid);
                    FriendInfo userObject = JsonConvert.DeserializeObject<FriendInfo>(userInfo);
                    Username.Text = userObject.data.username;
                    App.client.getRoomMessagesByUserId(App._reuserid);
                }
            catch
                { }

        }
        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                JArray memberArray = new JArray();
                memberArray.Add(App._userid);
                memberArray.Add(App._reuserid);
                App.connectView.sendMesS(App._userid, false, enter.Text, 1, App._username, memberArray, DateTime.Now.ToShortDateString());
                enter.Text = "";
                ContactListBox.SelectedIndex = App.ViewModel.Items.Count() - 1;
                ContactListBox.Focus();
            }
        }

        private void ContactListBox_LayoutUpdated(object sender, EventArgs e)
        {
            this.ContactListBox.SelectedIndex = App.ViewModel.Items.Count - 1;
        }
    }
}