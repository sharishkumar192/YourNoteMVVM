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

            NameBox_LostFocus(sender, e);
            EmailBox_LostFocus(sender, e);
            PasswordBox_LostFocus(sender, e);
            RPasswordBox_LostFocus(sender, e);
           

            if (NameToolTip.Visibility == Visibility.Visible &&
                EmailToolTip.Visibility == Visibility.Collapsed &&
                       PasswordBoxToolTip.Visibility == Visibility.Collapsed &&
                       RPasswordBoxToolTip.Visibility == Visibility.Collapsed)
        
            {

                User user = new User(NameBoxContent.Text, EmailBox.Text, RPasswordBox.Password);
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
            if(NameBoxContent.Text.Length==0)
            {
                NameCheck.Visibility = Visibility.Visible;
                NameBoxToolTip.Content = NameToolTip.Content = "The Name cant be empty";
            }
            else
            {
                NameCheck.Visibility = Visibility.Collapsed;
                NameBoxToolTip.Content = NameToolTip.Content =  "";
            }
        }
        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameCheck.Visibility = Visibility.Collapsed;
        }
        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text.Length == 0 || ( EmailBox.Text.Length != 0 && User.IsValidEmail(EmailBox.Text) == false))
            {
                EmailCheck.Visibility = Visibility.Visible;
                EmailBoxToolTip.Content = EmailToolTip.Content = "Enter a Valid Email Id";
            }

            else if (EmailBox.Text.Length != 0 && DBFetch.CheckValidEmail(DBCreation.userTableName, EmailBox.Text) == true)
            {
                EmailCheck.Visibility = Visibility.Visible;
                EmailBoxToolTip.Content = EmailToolTip.Content = "The Email Id Already Exists!";
            }
            else
            {
                EmailCheck.Visibility = Visibility.Collapsed;
                EmailBoxToolTip.Visibility = Visibility.Collapsed;  
                EmailBoxToolTip.Content = EmailToolTip.Content = "";

            }
        }
        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailCheck.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ( PasswordBox.Password.Length <= 2)
            {
                PasswordCheck.Visibility = Visibility.Visible;
                PasswordBoxToolTip.Content = PasswordToolTip.Content = "The Length of the Password must be greater than 2";
            }
            else if (PasswordBox.Password.Length != RPasswordBox.Password.Length && PasswordBox.Password != RPasswordBox.Password)
            {
                RPasswordCheck.Visibility = Visibility.Visible;
                RPasswordBoxToolTip.Content = RPasswordToolTip.Content = "Password did not match";
            }
            else
            {
                PasswordCheck.Visibility = Visibility.Collapsed;
                PasswordBoxToolTip.Visibility = Visibility.Collapsed;
                PasswordBoxToolTip.Content = PasswordToolTip.Content = "";

            }

        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            PasswordCheck.Visibility = Visibility.Collapsed;
        }

        private void RPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (PasswordBox.Password.Length==0 || (PasswordBox.Password.Length!= RPasswordBox.Password.Length  && PasswordBox.Password != RPasswordBox.Password))
            {
                
                RPasswordCheck.Visibility = Visibility.Visible;
                RPasswordBoxToolTip.Content = RPasswordToolTip.Content = "Password did not match";
                PasswordCheck.Visibility = Visibility.Visible;
                RPasswordBoxToolTip.Content = RPasswordToolTip.Content = "The Length of the Password must be greater than 2";
            }
            else
            {
                RPasswordCheck.Visibility = Visibility.Collapsed;
                RPasswordBoxToolTip.Visibility = Visibility.Collapsed;
                RPasswordBoxToolTip.Content = RPasswordToolTip.Content = "";

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

            RPasswordCheck.Visibility = Visibility.Collapsed;

        }

        private void NameBoxContent_LostFocus(object sender, RoutedEventArgs e)
        {
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
