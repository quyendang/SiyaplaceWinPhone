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
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.IO;

namespace RAMACHAT
{
    public partial class ChatPage : PhoneApplicationPage
    {
        public string _reuserid = "";
        public PhotoChooserTask photoChooserTask;
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
                    UserName.Text = userObject.data.username;
                    avatar.ImageSource = new BitmapImage(new Uri(userObject.data.avatar, UriKind.RelativeOrAbsolute));
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
                App.connectView.sendMesS(App._userid, false, enter.Text, 1, App._username, memberArray, Convert.ToString(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
                enter.Text = "";
                ContactListBox.SelectedIndex = App.ViewModel.Items.Count() - 1;
                ContactListBox.Focus();
            }
        }

        private void ContactListBox_LayoutUpdated(object sender, EventArgs e)
        {
            this.ContactListBox.SelectedIndex = App.ViewModel.Items.Count - 1;
        }

        private void imageChatBtn_Click(object sender, EventArgs e)
        {
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            photoChooserTask.Show();
        }
        public async void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
               MemoryStream photoStream;
               photoStream = new MemoryStream();
               e.ChosenPhoto.CopyTo(photoStream);
               string result = await App.client.PostImage(e.OriginalFileName, photoStream);
               UploadImageResponse resultObject = JsonConvert.DeserializeObject<UploadImageResponse>(result);
               if(resultObject.data._id !=null)
               {
                   JArray memberArray = new JArray();
                   memberArray.Add(App._userid);
                   memberArray.Add(App._reuserid);
                   App.connectView.sendMesS(App._userid, false, resultObject.data._id, 2, App._username, memberArray, Convert.ToString(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
               }
            }
        }
    }
}