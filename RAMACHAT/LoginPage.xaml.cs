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
using System.Windows.Media;
using RAMACHAT.Helper;
using System.IO.IsolatedStorage;
using System.Threading;

namespace RAMACHAT
{
    public partial class LoginPage : PhoneApplicationPage
    {
        private int sex;
        
        public LoginPage()
        {
            InitializeComponent();
            user_name_textbox.Text = "a@a.com";
            
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    try
        //    {
        //        //App.ViewModel.Items.Clear();
        //        if (App._token != null && App._username != null && App._userid != null)
        //        {
        //            App.connectView.TOKEN = App._token;
        //            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        //        }
        //    }
        //    catch
        //    { }

        //}
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            // put any code you like here
            //MessageBox.Show("You pressed the Back button");
            if (main_pivot.SelectedItem == SignIn_pivotitem)
            {
                MessageBoxResult mr = MessageBox.Show("", "Exit Horical Chat ?", MessageBoxButton.OKCancel);
                if (mr == MessageBoxResult.OK)
                {
                    Application.Current.Terminate();
                    e.Cancel = true;
                }
                if (mr == MessageBoxResult.Cancel)
                { e.Cancel = true; }
            }
            if (main_pivot.SelectedItem == signup_pivotitem)
            {
                main_pivot.IsLocked = false;
                main_pivot.SelectedItem = SignIn_pivotitem;
                e.Cancel = true;
                main_pivot.IsLocked = true;
            }
            if (main_pivot.SelectedItem == ForgotPass_pivotitem)
            {
                main_pivot.IsLocked = false;
                main_pivot.SelectedItem = SignIn_pivotitem;
                e.Cancel = true;
                main_pivot.IsLocked = true;
            }

        }



        private async void signBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUpForm user = new SignUpForm() {  username = user_name_textbox.Text, password = Password_box_signin.Password };
            string jsonString = JsonConvert.SerializeObject(user);
            string result = await App.client.Login(jsonString.ToString());
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
                IsolatedStorageSettings.ApplicationSettings["TOKEN"] = resultObject.data.token;
                IsolatedStorageSettings.ApplicationSettings["USERID"] = resultObject.data._id;
                IsolatedStorageSettings.ApplicationSettings["USERNAME"] = resultObject.data.username;
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            else
            {

            }
        }

        private void signupBtn_Click(object sender, RoutedEventArgs e)
        {
            main_pivot.IsLocked = false;
            main_pivot.SelectedItem = signup_pivotitem;
            main_pivot.IsLocked = true;
        }
        

        private void ForgotPass_click(object sender, RoutedEventArgs e)
        {
            main_pivot.IsLocked = false;
            main_pivot.SelectedItem = ForgotPass_pivotitem;
            forgot_email_textbox.Text = "Email";
            forgot_email_textbox.Foreground = new SolidColorBrush(Colors.Gray);
            main_pivot.IsLocked = true;
        }

        private void user_box_gotfocus(object sender, RoutedEventArgs e)
        {
            if (user_name_textbox.Text == "User Name")
            {
                user_name_textbox.Text = "";
                user_name_textbox.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {

            }
        }

        private void user_box_lostfocus(object sender, RoutedEventArgs e)
        {
            if (user_name_textbox.Text != "")
            {
                user_name_textbox.SelectionStart = 0;
                user_name_textbox.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                user_name_textbox.Text = "User Name";
                user_name_textbox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void password_gotfocus(object sender, RoutedEventArgs e)
        {
            //password_box.Opacity = 1;
            Password_box_signin.Opacity = 1;
            password_textbox.Text = "";
        }

        private void password_lostfocus(object sender, RoutedEventArgs e)
        {
            if (Password_box_signin.Password != "")
            {

            }
            else
            {
                password_textbox.Text = "Password";
                Password_box_signin.Opacity = 0;
            }
        }

        private void SignUp_textbox_gotfocus(object sender, RoutedEventArgs e)
        {
            TextBox TB = sender as TextBox;
            switch (TB.Name)
            {
                case "UserTxt":
                    {
                        if (UserTxt.Text == "User Name")
                        {
                            UserTxt.Text = "";
                            UserTxt.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else { }
                    }
                    break;
                case "PhoneTxt":
                    {
                        if (PhoneTxt.Text == "Phone Number")
                        {
                            PhoneTxt.Text = "";
                            PhoneTxt.Foreground = new SolidColorBrush(Colors.Black);
                        }
                    }
                    break;
                case "EmailTxt":
                    {
                        if (EmailTxt.Text == "Email")
                        {
                            EmailTxt.Text = "";
                            EmailTxt.Foreground = new SolidColorBrush(Colors.Black);
                        }

                    }
                    break;
                case "CountryTxt":
                    {
                        if (CountryTxt.Text == "Country")
                        {
                            CountryTxt.Text = "";
                            CountryTxt.Foreground = new SolidColorBrush(Colors.Black);
                        }
                    }
                    break;
                case "forgot_email_textbox":
                    {
                        if (forgot_email_textbox.Text == "Email")
                        {
                            forgot_email_textbox.Text = "";
                            forgot_email_textbox.Foreground = new SolidColorBrush(Colors.Black);
                        } break;
                    }

            }
        }

        private void SignUp_textbox_lostfocus(object sender, RoutedEventArgs e)
        {

            TextBox TB = sender as TextBox;
            switch (TB.Name)
            {
                case "UserTxt":
                    {
                        if (UserTxt.Text != "")
                        {
                            user_name_textbox.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            UserTxt.Text = "User Name";
                            UserTxt.Foreground = new SolidColorBrush(Colors.Gray);
                        }
                    }
                    break;

                case "PhoneTxt":
                    {
                        if (PhoneTxt.Text != "")
                        {
                            PhoneTxt.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            PhoneTxt.Text = "Phone Number";
                            PhoneTxt.Foreground = new SolidColorBrush(Colors.Gray);
                        }
                    }
                    break;
                case "EmailTxt":
                    {
                        if (EmailTxt.Text != "")
                        {
                            EmailTxt.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            EmailTxt.Text = "Email";
                            EmailTxt.Foreground = new SolidColorBrush(Colors.Gray);
                        }
                    } break;
                case "CountryTxt":
                    {
                        if (CountryTxt.Text != "")
                        {
                            CountryTxt.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            CountryTxt.Text = "Country";
                            CountryTxt.Foreground = new SolidColorBrush(Colors.Gray);
                        }
                    } break;
                case "forgot_email_textbox":
                    {
                        if (forgot_email_textbox.Text != "")
                        {
                            forgot_email_textbox.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            forgot_email_textbox.Text = "Email";
                            forgot_email_textbox.Foreground = new SolidColorBrush(Colors.Gray);
                        }
                    } break;
            }
        }

        private void password_box_gotfocus(object sender, RoutedEventArgs e)
        {
            Pass_box.Opacity = 1;
            PassTxt.Text = "";
        }

        private void password_box_lostfocus(object sender, RoutedEventArgs e)
        {
            if (Pass_box.Password != "")
            { }
            else
            {
                PassTxt.Text = "Password";
                Pass_box.Opacity = 0;
            }
        }

        private void passwordconfirm_box_gotfocus(object sender, RoutedEventArgs e)
        {
            Passconfirm_box.Opacity = 1;
            PassconfirmTxt.Text = "";
        }

        private void passwordconfirm_box_lostfocus(object sender, RoutedEventArgs e)
        {
            if (Passconfirm_box.Password != "")
            { }
            else
            {
                Passconfirm_box.Opacity = 0;
                PassconfirmTxt.Text = "Password Confirmation";
            }
        }

        private async void Submit_click(object sender, RoutedEventArgs e)
        {
            bool result;

            if (Pass_box.Password != "" && EmailTxt.Text != "" && Pass_box.Password == Passconfirm_box.Password && UserTxt.Text.Length>=3)
            

                {
                    SignUpForm user = new SignUpForm() { username = UserTxt.Text, password = Pass_box.Password, country = CountryTxt.Text, email = EmailTxt.Text, phone = PhoneTxt.Text, gender = sex };
                    string jsonString = JsonConvert.SerializeObject(user);
                    //result = await App.client.SignUp(jsonString);
                    result = await App.client.SignUp(jsonString);
                    if (result)
                    {
                        //Message_Ts.Message = "OK!";
                        MessageBox.Show("You have signup an accout");
                        Thread.Sleep(2000);
                        //NavigationService.Navigate(new Uri("/LoginPage.xaml?username=" + EmailTxt.Text + "&password=" + passTxt.Password, UriKind.Relative));
                        main_pivot.IsLocked = false;
                        main_pivot.SelectedItem = SignIn_pivotitem;
                        user_name_textbox.Text = EmailTxt.Text;
                        Password_box_signin.Password = Pass_box.Password;
                        main_pivot.IsLocked = true;
                    }
                }
            else
                if (!EmailTxt.Text.Contains("@"))
                {
                    //Message_Ts.Message = "Email incorect!";
                    MessageBox.Show("Email incorect");
                }
            else
                    if (Pass_box.Password != Passconfirm_box.Password)
                    {
                        MessageBox.Show("Password and Password confirm must be the Same");
                    }
                else if (UserTxt.Text.Length < 3) { MessageBox.Show("Username must be 3 - 32 characters"); }
                    else MessageBox.Show("Username & Password is require"); 

            }
            
        

        private void submit_forgot_click(object sender, RoutedEventArgs e)
        {
            
            
           
        }

        private void female_radio_btn_Checked(object sender, RoutedEventArgs e)
        {
            sex = 2;
        }

        private void male_radio_btn_Checked(object sender, RoutedEventArgs e)
        {
            sex = 1;
        }
    }
}