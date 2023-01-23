using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using YourNoteUWP.Models;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourNoteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogInPage : Page
    {
        private ObservableCollection<YourNoteUWP.Models.User> users;
        public LogInPage()
        {
            this.InitializeComponent();
            users = User.GetFrequentUsers();
            //  users = new ObservableCollection(Collection.OrderBy(users => users[].Date).ToList());
            if (users == null || users.Count == 0)
                Column1.Visibility = Visibility.Collapsed;

            else

            {
                //Column1.Visibility = Visibility.Collapsed;
                Column1.Visibility = Visibility.Visible;

            }
        }





        private void revealModeCheckbox_Changed(object sender, RoutedEventArgs e)
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



        private void emailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            emailCheck.Visibility = Visibility.Collapsed;
        }

        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            Tuple<Models.User, bool> validation = DBFetch.ValidateUser(DBCreation.userTableName, emailBox.Text, passwordBox.Password);
            if (validation.Item2 == true)  
            {
                this.Content = new AccountPage(validation.Item1);

            }
            else
            {
                //Credentials Wrong
            }
        }

        private void frequentEmail_ItemClick(object sender, ItemClickEventArgs e)
        {
            var user = (User)e.ClickedItem;

            emailBox.Text = user.userId;

        }

        private void logInBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new MainPage();
            // this.Frame.Navigate(typeof(OpeningPage));
        }

        private void emailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (emailBox.Text.Length == 0 || (emailBox.Text.Length != 0 && User.IsValidEmail(emailBox.Text) == false))
            {
                emailCheck.Visibility = Visibility.Visible;
                emailBoxToolTip.Content = emailToolTip.Content = "Enter a Valid Email Id";
            }
            else
            {
                emailCheck.Visibility = Visibility.Collapsed;
                emailBoxToolTip.Visibility = Visibility.Collapsed;
                emailBoxToolTip.Content = emailToolTip.Content = "";

            }
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Length <= 2)
            {
                passwordCheck.Visibility = Visibility.Visible;
                passwordBoxToolTip.Content = passwordToolTip.Content = "The Length of the Password must be greater than 2";
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
    }


}
