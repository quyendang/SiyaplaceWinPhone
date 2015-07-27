using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RAMACHAT.Model;
using Newtonsoft.Json;
using RAMACHAT.ApiClient;
using System.Diagnostics;
using System.ComponentModel;

namespace RAMACHAT
{
    public partial class LoginPage : PhoneApplicationPage
    {
        private bool signUp = false;
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void signBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUpForm user = new SignUpForm() {  username = userTbn.Text, password = passTbn.Text };
            string jsonString = JsonConvert.SerializeObject(user);
            string result = await App.client.Login( jsonString.ToString());
            Debug.WriteLine(result);
            LoginResponse resultObject = JsonConvert.DeserializeObject<LoginResponse>(result);
            if(resultObject.data.token != null)
            {
                App.client.setToken(resultObject.data.token);
                App.client.USERID = resultObject.data._id;
                App.connectView.TOKEN = resultObject.data.token ;
                App._token = resultObject.data.token;
                App._userid = resultObject.data._id;
                App._username = resultObject.data.username;
                //App._
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        private void signupBtn_Click(object sender, RoutedEventArgs e)
        {
            this.signUp = true;
            mainPivot.IsLocked = false;
            mainPivot.SelectedIndex = 1;
            mainPivot.IsLocked = true;
        }
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = true;
            if(signUp)
            {
                mainPivot.IsLocked = false;
                mainPivot.SelectedIndex = 0;
                mainPivot.IsLocked = true;
                this.signUp = false;
            }
            //NavigationService.GoBack();
        }
    }
}