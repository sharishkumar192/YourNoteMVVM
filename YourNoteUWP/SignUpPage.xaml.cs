using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static System.Net.Mime.MediaTypeNames;
using YourNoteUWP.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourNoteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();
            //passwordBox.Height = NameBoxContent.Height;
            //passwordBox.Width = NameBoxContent.Width;
            //repasswordBox.Height = passwordBox.Height;
            //repasswordBox.Width = passwordBox.Width;


        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
          


            //if (NameBoxContent.Text.Length == 0)
            //{
            //    NameBoxContent.Description = "Please Enter The Name!";
            //}
            //else
            //{
            //    NameBoxContent.Description = "";
            //}


            //if (EmailBox.Text.Length == 0)
            //{
            //    EmailBox.Description = "Please Enter The Email Id!";
            //}
            //else if (DBFetch.CheckValidEmail(DBCreation.userTableName, EmailBox.Text)== true )
            //{
            //    EmailBox.Description = "The Email Id Already Exists!";
            //}
            //else
            //{
            //    EmailBox.Description = "";
                
            //}


            //if (passwordBox.Password.Length == 0)
            //{
            //    passwordBox.Description = "Please Enter The Password";
            //}
            //else
            //{
            //    passwordBox.Description = "";
            //}


            //if (repasswordBox.Password.Length == 0)
            //{
            //    repasswordBox.Description = "Please ReType The Password";
            //}
            //else if (passwordBox.Password != repasswordBox.Password && repasswordBox.Password.Length != 0)
            //{
            //    repasswordBox.Description = "The Password Is Not Matching!";
            //}
            //else
            //    repasswordBox.Description = "";

            //if (NameBoxContent.Text.Length != 0 &&

            //    EmailBox.Text.Length != 0 &&

            //        passwordBox.Password.Length != 0 &&

            //    repasswordBox.Password.Length != 0  &&  

            //    string.Compare(RPasswordBox.Password, RPpasswordBox.Password) == 0

            //    )
    
            {




                 User user = new User(NameBoxContent.Text, EmailBox.Text,RPasswordBox.Password);
                DBUpdation.InsertNewUser(user);
                this.Content = new LogInPage();

            }
            

        }
    
   
   

        private void SignUpBackButton_Click(object sender, RoutedEventArgs e)
        {

            this.Content = new MainPage();
        }




        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NameProgress.Visibility = Visibility.Collapsed;
        }
        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameProgress.Visibility = Visibility.Visible;
        }
        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text.Length != 0 && User.IsValidEmail(EmailBox.Text) == false)
            {
                EmailProgress.Visibility = Visibility.Collapsed;
                EmailCheck.Visibility = Visibility.Visible;
                EmailToolTip.Content = "asdasdsadasd";
            }
            else
            {
                EmailCheck.Visibility = Visibility.Collapsed;
                EmailProgress.Visibility = Visibility.Collapsed;
                EmailToolTip.Content = "";

            }
        }
        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailProgress.Visibility = Visibility.Visible;
            EmailCheck.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Length <3 )
            {
                PasswordProgress.Visibility = Visibility.Collapsed;
                PasswordCheck.Visibility = Visibility.Visible;
                PasswordToolTip.Content = "asdasdsadasd";
            }
            else
            {
                PasswordCheck.Visibility = Visibility.Collapsed;
                PasswordProgress.Visibility = Visibility.Collapsed;
                PasswordToolTip.Content = "";

            }

        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            PasswordProgress.Visibility = Visibility.Visible;
            PasswordCheck.Visibility = Visibility.Collapsed;
        }

        private void RPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password != RPasswordBox.Password )
            {
                RPasswordProgress.Visibility = Visibility.Collapsed;
                RPasswordCheck.Visibility = Visibility.Visible;
                RPasswordToolTip.Content = "asdasdsadasd";
            }
            else
            {
                RPasswordCheck.Visibility = Visibility.Collapsed;
                RPasswordProgress.Visibility = Visibility.Collapsed;
                RPasswordToolTip.Content = "";

            }
        }

        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }


        private void RepasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            RPasswordProgress.Visibility = Visibility.Visible;
            RPasswordCheck.Visibility = Visibility.Collapsed;

        }

        private void NameBoxContent_LostFocus(object sender, RoutedEventArgs e)
        {
            NameProgress.Visibility = Visibility.Collapsed; 
        }



        //private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    if (passwordBox.Password == "Password")
        //    {
        //        statusText.Text = "'Password' is not allowed as a password.";
        //    }
        //    else
        //    {
        //        statusText.Text = string.Empty;
        //    }
        //}
    }
}
