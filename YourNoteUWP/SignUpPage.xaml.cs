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
using YourNoteUWP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourNoteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        private SignUpPageViewModel _signUpPageViewModel;
    

        public SignUpPage()
        {
            this.InitializeComponent();
            _signUpPageViewModel = new SignUpPageViewModel();
            this.DataContext = _signUpPageViewModel;
            //passwordBox.Height = nameBoxContent.Height;
            //passwordBox.Width = nameBoxContent.Width;
            //repasswordBox.Height = passwordBox.Height;
            //repasswordBox.Width = passwordBox.Width;


        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
           // NameBox_LostFocus(sender, e);
            EmailBox_LostFocus(sender, e);
            PasswordBox_LostFocus(sender, e);
            RPasswordBox_LostFocus(sender, e);

            if ( NameCheck.Visibility == Visibility.Collapsed  &&
                EmailCheck.Visibility == Visibility.Collapsed &&
                PasswordCheck.Visibility == Visibility.Collapsed &&
                RPasswordCheck.Visibility == Visibility.Collapsed )
            {
                User user = new User(NameBox.Text, EmailBox.Text, RPasswordBox.Password);
                DBUpdation.InsertNewUser(user);
                this.Content = new LogInPage();
            }
              



        }

        public string IsEmailCheck(string check)
        {
            if (String.IsNullOrEmpty(check) == true)
                return "Email Cant Be Empty";
            else if (User.IsValidEmail(check) == false)
                return "Email Id Not Valid";
            else if (User.CheckEmail(check) == true)
                return "The Email Id Already Exists!";
            return null;
        }

        public string IsPasswordCheck(string check)
        {

            if (String.IsNullOrEmpty(check) == true)
                return "Password Cant Be Empty";
            else if (check.Length < 2)
                return "Password Must Be Alteast Length 3";

            return null;
        }

        public string IsRPasswordCheck(string check1, string check2)
        {

            string value = IsPasswordCheck(check2);
            if (value == null)
            {
                if (check1 == check2)
                    return null;
                else
                    return "Password Not Matching!";
            }
            else if (value == "Password Cant Be Empty" || value == "Password Must Be Alteast Length 3")
                return null;
            else if (String.IsNullOrEmpty(check2) == true)
                return "Retype The Password!";

            return null;

        }



        private void SignUpBackButton_Click(object sender, RoutedEventArgs e)
        {

            this.Content = new MainPage();
        }

       
        private void NameBox_LostFocus(object sender, RoutedEventArgs e)// Done
        {

            _signUpPageViewModel.NameBoxLostFocus();
   
        } 
      
        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)// Done
        {

            string value = IsEmailCheck(EmailBox.Text);
            if (value == null)
            {
                EmailBoxToolTip.Content = EmailToolTip.Content = "";
                EmailBoxToolTip.Visibility = EmailToolTip.Visibility = EmailCheck.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmailBoxToolTip.Content = EmailToolTip.Content =  value;
                EmailBoxToolTip.Visibility = EmailToolTip.Visibility = EmailCheck.Visibility = Visibility.Visible;
               
            }
         
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)// Done
        {
            

            string value = IsPasswordCheck(PasswordBox.Password);
            if (value == null)
            {
                PasswordBoxToolTip.Content = PasswordToolTip.Content = "";
                PasswordBoxToolTip.Visibility = PasswordToolTip.Visibility = PasswordCheck.Visibility = Visibility.Collapsed;
            }
            else
            {
                PasswordBoxToolTip.Content = PasswordToolTip.Content= value;
                PasswordBoxToolTip.Visibility = PasswordToolTip.Visibility = PasswordCheck.Visibility = Visibility.Visible;

            }


        }

        private void RPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string value = IsRPasswordCheck(RPasswordBox.Password, PasswordBox.Password);
            if (value == null)
            {
                RPasswordBoxToolTip.Content = RPasswordToolTip.Content = "";
                RPasswordBoxToolTip.Visibility = RPasswordToolTip.Visibility = RPasswordCheck.Visibility = Visibility.Collapsed;
            }
            else
            {
                RPasswordBoxToolTip.Content = RPasswordToolTip.Content = value;
                RPasswordBoxToolTip.Visibility = RPasswordToolTip.Visibility = RPasswordCheck.Visibility = Visibility.Visible;

            }
          
        }

        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            if (RevealModeCheckBox.IsChecked == true)
            {
                PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
                RPasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
                RPasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }


   



    }
}
