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
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;

namespace RAMACHAT
{
    public partial class ChatPage : PhoneApplicationPage
    {
        public string _reuserid = "";
        public PhotoChooserTask photoChooserTask;
        public CameraCaptureTask cameraTask;
        public ChatPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
            //App.viewModel.addThemeList();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //string ThemeChat = 
            //App.viewModel.setThemChat(1);
            try
            {
                App.ViewModel.getAllThemChat();
            }

            catch { }
            ThemListBox.ItemsSource = App.ViewModel.ThemeChatlist;
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
                if (resultObject.data._id != null)
                {
                    JArray memberArray = new JArray();
                    memberArray.Add(App._userid);
                    memberArray.Add(App._reuserid);
                    App.connectView.sendMesS(App._userid, false, resultObject.data._id, 2, App._username, memberArray, Convert.ToString(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
                }
            }
        }

        private void cameraBtn_Click(object sender, EventArgs e)
        {
            cameraTask = new CameraCaptureTask();
            cameraTask.Completed += cameraTask_Completed;
            cameraTask.Show();
        }

        public async void cameraTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                MemoryStream photoStream;
                photoStream = new MemoryStream();
                e.ChosenPhoto.CopyTo(photoStream);
                string result = await App.client.PostImage(e.OriginalFileName, photoStream);
                UploadImageResponse resultObject = JsonConvert.DeserializeObject<UploadImageResponse>(result);
                if (resultObject.data._id != null)
                {
                    JArray memberArray = new JArray();
                    memberArray.Add(App._userid);
                    memberArray.Add(App._reuserid);
                    App.connectView.sendMesS(App._userid, false, resultObject.data._id, 2, App._username, memberArray, Convert.ToString(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
                }
            }
        }

        private async void ShowMyLocationOnTheMap()
        {
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate =
                CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);
            this.map.Center = myGeoCoordinate;
            this.map.ZoomLevel = 15;
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Blue);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myGeoCoordinate;
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);
            map.Layers.Add(myLocationLayer);

        }


        private void Map_Click(object sender, EventArgs e)
        {
            map.Visibility = Visibility.Visible;
            ShowMyLocationOnTheMap();
        }

        private void setThemebtn_Click(object sender, EventArgs e)
        {
            Background.ImageSource = new BitmapImage(new Uri("/ThemeChat/1.jpg", UriKind.RelativeOrAbsolute));
        }

        private void SetTheme_Click(object sender, EventArgs e)
        {
            ThemePanel.Visibility = System.Windows.Visibility.Visible;
        }

        private void ThemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x = ThemListBox.SelectedItem as RAMACHAT.ThemeChat.ThemeChatItem;
            Background.ImageSource = new BitmapImage(new Uri(x.ImageSource, UriKind.RelativeOrAbsolute));
            ThemePanel.Visibility = System.Windows.Visibility.Collapsed;

        }
    }
}