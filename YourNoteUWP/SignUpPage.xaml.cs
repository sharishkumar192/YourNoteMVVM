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
            //passwordBox.Height = nameBoxContent.Height;
            //passwordBox.Width = nameBoxContent.Width;
            //repasswordBox.Height = passwordBox.Height;
            //repasswordBox.Width = passwordBox.Width;


        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {

            //nameBox_LostFocus(sender, e);
            //emailBox_LostFocus(sender, e);
            //passwordBox_LostFocus(sender, e);
            //rPasswordBox_LostFocus(sender, e);


            //if (nameToolTip.Visibility == Visibility.Visible &&
            //    emailToolTip.Visibility == Visibility.Collapsed &&
            //           passwordBoxToolTip.Visibility == Visibility.Collapsed &&
            //           rPasswordBoxToolTip.Visibility == Visibility.Collapsed)

            //{

                User user = new User(nameBox.Text, emailBox.Text, rPasswordBox.Password);
                DBUpdation.InsertNewUser(user);
                this.Content = new LogInPage();
            //}



        }
    
   
   

        private void signUpBackButton_Click(object sender, RoutedEventArgs e)
        {

            this.Content = new MainPage();
        }




        private void nameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(nameBox.Text.Length==0)
            {
                nameCheck.Visibility = Visibility.Visible;
                nameBoxToolTip.Content = nameToolTip.Content = "The Name cant be empty";
            }
            else
            {
                nameCheck.Visibility = Visibility.Collapsed;
                nameBoxToolTip.Content = nameToolTip.Content =  "";
            }
        }
        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameCheck.Visibility = Visibility.Collapsed;
        }


        private void emailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (emailBox.Text.Length == 0 || ( emailBox.Text.Length != 0 && User.IsValidEmail(emailBox.Text) == false))
            {
                emailCheck.Visibility = Visibility.Visible;
                emailBoxToolTip.Content = emailToolTip.Content = "Enter a Valid Email Id";
            }

            else if (emailBox.Text.Length != 0 && User.CheckEmail(emailBox.Text) == true)
            {
                emailCheck.Visibility = Visibility.Visible;
                emailBoxToolTip.Content = emailToolTip.Content = "The Email Id Already Exists!";
            }
            else
            {
                emailCheck.Visibility = Visibility.Collapsed;
                emailBoxToolTip.Visibility = Visibility.Collapsed;  
                emailBoxToolTip.Content = emailToolTip.Content = "";

            }
        }
        private void emailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            emailCheck.Visibility = Visibility.Collapsed;
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ( passwordBox.Password.Length <= 2)
            {
                passwordCheck.Visibility = Visibility.Visible;
                passwordBoxToolTip.Content = passwordToolTip.Content = "The Length of the Password must be greater than 2";
            }
            else if (passwordBox.Password.Length != rPasswordBox.Password.Length && passwordBox.Password != rPasswordBox.Password)
            {
                rPasswordCheck.Visibility = Visibility.Visible;
                rPasswordBoxToolTip.Content = rPasswordToolTip.Content = "Password did not match";
            }
            else
            {
                passwordCheck.Visibility = Visibility.Collapsed;
                passwordBoxToolTip.Visibility = Visibility.Collapsed;
                passwordBoxToolTip.Content = passwordToolTip.Content = "";

            }

        }
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            passwordCheck.Visibility = Visibility.Collapsed;
        }

        private void rPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (passwordBox.Password.Length==0 || (passwordBox.Password.Length!= rPasswordBox.Password.Length  && passwordBox.Password != rPasswordBox.Password))
            {

                rPasswordCheck.Visibility = Visibility.Visible;
                rPasswordBoxToolTip.Content = rPasswordToolTip.Content = "password did not match";
                passwordCheck.Visibility = Visibility.Visible;
                rPasswordBoxToolTip.Content = rPasswordToolTip.Content = "The Length of the password must be greater than 2";
            }
            else
            {
                rPasswordCheck.Visibility = Visibility.Collapsed;
                rPasswordBoxToolTip.Visibility = Visibility.Collapsed;
                rPasswordBoxToolTip.Content = rPasswordToolTip.Content = "";

            }
        }

        private void revealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                passwordBox.PasswordRevealMode = PasswordRevealMode.Visible;
                rPasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                passwordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
                rPasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }


        private void repasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            rPasswordCheck.Visibility = Visibility.Collapsed;

        }



    }
}
