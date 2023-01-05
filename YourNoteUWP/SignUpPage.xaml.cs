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
            if (NameBoxContent.Text.Length == 0)
            {
                NameBoxContent.Description = "Please Enter The Name!";
            }
            else
            {
                NameBoxContent.Description = "";
            }


            if (EmailBoxContent.Text.Length == 0)
            {
                EmailBoxContent.Description = "Please Enter The Email Id!";
            }
            else if (DBFetch.CheckValidEmail(DBCreation.userTableName, EmailBoxContent.Text)== true )
            {
                EmailBoxContent.Description = "The Email Id Already Exists!";
            }
            else
            {
                EmailBoxContent.Description = "";
                
            }


            if (passwordBox.Password.Length == 0)
            {
                passwordBox.Description = "Please Enter The Password";
            }
            else
            {
                passwordBox.Description = "";
            }


            if (repasswordBox.Password.Length == 0)
            {
                repasswordBox.Description = "Please ReType The Password";
            }
            else if (passwordBox.Password != repasswordBox.Password && repasswordBox.Password.Length != 0)
            {
                repasswordBox.Description = "The Password Is Not Matching!";
            }
            else
                repasswordBox.Description = "";

            if (NameBoxContent.Text.Length != 0 &&

                EmailBoxContent.Text.Length != 0 &&

                    passwordBox.Password.Length != 0 &&

                repasswordBox.Password.Length != 0  &&  

                string.Compare(passwordBox.Password, repasswordBox.Password) == 0

                )
    
            {




                 User user = new User(NameBoxContent.Text, EmailBoxContent.Text, passwordBox.Password);
                DBUpdation.InsertNewUser(user);
                this.Content = new LogInPage();

            }
            

        }
        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                passwordBox.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                passwordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (passwordBox.Password.Length < 2 && passwordBox.Password.Length!=0)
            {
                passwordBox.Description = "The Password Length must be atleast of size 2";

            }
            else
                passwordBox.Description = "";
        }

        private void RepasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password!=repasswordBox.Password  && repasswordBox.Password.Length!=0)
            {
                passwordBox.Description = "The Password Is Not Matching!";
            }
            else
                passwordBox.Description = "";

        }

        private void EmailBoxContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(EmailBoxContent.Text.Length!=0 && User.IsValidEmail(EmailBoxContent.Text) == false )
            {
                EmailBoxContent.Description = "Provide A Proper Email";
            }
            else
            {
                EmailBoxContent.Description = "";
            }
        }

        private void SignUpBackButton_Click(object sender, RoutedEventArgs e)
        {

            this.Content = new MainPage();
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
