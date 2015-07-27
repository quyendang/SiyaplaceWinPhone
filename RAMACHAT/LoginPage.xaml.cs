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

namespace RAMACHAT
{
    public partial class LoginPage : PhoneApplicationPage
    {
        private bool signUp = false;
        public LoginPage()
        {
            InitializeComponent();
        }


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
            SignUpForm user = new SignUpForm() {  username = user_name_textbox.Text, password = password_box.Password };
            string jsonString = JsonConvert.SerializeObject(user);
            string result = await App.client.Login( jsonString.ToString());
            Debug.WriteLine(result);
            LoginResponse resultObject = JsonConvert.DeserializeObject<LoginResponse>(result);
            if(resultObject.data.token != null)
            {
                App.client.setToken(resultObject.data.token);
                App.client.USERID = resultObject.data._id;
                App.connectView.TOKEN = resultObject.data.token ;
                NavigationService.Navigate(new Uri("/MainPage.xaml?username=" + resultObject.data.username + "&token=" + resultObject.data.token + "&userid="+ resultObject.data._id, UriKind.Relative));
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
            password_box.Opacity = 1;
            password_textbox.Text = "";
        }

        private void password_lostfocus(object sender, RoutedEventArgs e)
        {
            if (password_box.Password != "")
            {

            }
            else
            {
                password_textbox.Text = "Password";
                password_box.Opacity = 0;
            }
        }

        private void SignUp_textbox_gotfocus(object sender, RoutedEventArgs e)
        {
            TextBox TB = sender as TextBox;
            switch (TB.Name)
            {
                case "SignUp_user_textbox":
                    {
                        if (SignUp_user_textbox.Text == "User Name")
                        {
                            SignUp_user_textbox.Text = "";
                            SignUp_user_textbox.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else { }
                    }
                    break;
                case "SignUp_phone_textbox":
                    {
                        if (SignUp_phone_textbox.Text == "Phone Number")
                        {
                            SignUp_phone_textbox.Text = "";
                            SignUp_phone_textbox.Foreground = new SolidColorBrush(Colors.Black);
                        }
                    }
                    break;
                case "SignUp_email_textbox":
                    {
                        if (SignUp_email_textbox.Text == "Email")
                        {
                            SignUp_email_textbox.Text = "";
                            SignUp_email_textbox.Foreground = new SolidColorBrush(Colors.Black);
                        }

                    }
                    break;
                case "SignUp_country_textbox":
                    {
                        if (SignUp_country_textbox.Text == "Country")
                        {
                            SignUp_country_textbox.Text = "";
                            SignUp_country_textbox.Foreground = new SolidColorBrush(Colors.Black);
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
                case "SignUp_user_textbox":
                    {
                        if (SignUp_user_textbox.Text != "")
                        {
                            user_name_textbox.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            SignUp_user_textbox.Text = "User Name";
                            SignUp_user_textbox.Foreground = new SolidColorBrush(Colors.Gray);
                        }
                    }
                    break;

                case "SignUp_phone_textbox":
                    {
                        if (SignUp_phone_textbox.Text != "")
                        {
                            SignUp_phone_textbox.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            SignUp_phone_textbox.Text = "Phone Number";
                            SignUp_phone_textbox.Foreground = new SolidColorBrush(Colors.Gray);
                        }
                    }
                    break;
                case "SignUp_email_textbox":
                    {
                        if (SignUp_email_textbox.Text != "")
                        {
                            SignUp_email_textbox.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            SignUp_email_textbox.Text = "Email";
                            SignUp_email_textbox.Foreground = new SolidColorBrush(Colors.Gray);
                        }
                    } break;
                case "SignUp_country_textbox":
                    {
                        if (SignUp_country_textbox.Text != "")
                        {
                            SignUp_country_textbox.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            SignUp_country_textbox.Text = "Country";
                            SignUp_country_textbox.Foreground = new SolidColorBrush(Colors.Gray);
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
            SignUp_pass_box.Opacity = 1;
            SignUp_pass_textbox.Text = "";
        }

        private void password_box_lostfocus(object sender, RoutedEventArgs e)
        {
            if (SignUp_pass_box.Password != "")
            { }
            else
            {
                SignUp_pass_textbox.Text = "Password";
                SignUp_pass_box.Opacity = 0;
            }
        }

        private void passwordconfirm_box_gotfocus(object sender, RoutedEventArgs e)
        {
            SignUp_passconfirm_box.Opacity = 1;
            SignUp_passconfirm_textbox.Text = "";
        }

        private void passwordconfirm_box_lostfocus(object sender, RoutedEventArgs e)
        {
            if (SignUp_passconfirm_box.Password != "")
            { }
            else
            {
                SignUp_passconfirm_box.Opacity = 0;
                SignUp_passconfirm_textbox.Text = "Password Confirmation";
            }
        }

        private void Creat_an_account_click(object sender, RoutedEventArgs e)
        {

        }

        private void submit_forgot_click(object sender, RoutedEventArgs e)
        {

        }
    }
}