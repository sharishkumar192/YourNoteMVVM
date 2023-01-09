﻿using System;
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
            users = DBFetch.ReadAllUserData(DBCreation.userTableName);
            //  users = new ObservableCollection(Collection.OrderBy(users => users[].Date).ToList());
            if (users == null || users.Count == 0)
                Column1.Visibility = Visibility.Collapsed;
            
            else 
            
            {
                //Column1.Visibility = Visibility.Collapsed;
                Column1.Visibility = Visibility.Visible;

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



        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EmailBox.Text.Length != 0 && User.IsValidEmail(EmailBox.Text) == false)
            {
                EmailBox.Description = "Provide A Proper Email";
            }
            else
            {
                EmailBox.Description = "";
            }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new Models.User(EmailBox.Text, passwordBox.Password);
            if (DBFetch.CheckUser(DBCreation.userTableName, user) == true)
            {
                user.loginCount++;

                DBUpdation.UpdateLoginCount(DBCreation.userTableName, user);
                // MainPageFrame.

                // this.Frame.Navigate(typeof(AccountPage), currentUser);
                this.Content = new AccountPage(user);

            }
            else
            {
                accountExists.Text = "Account Didnt Exist";
            }


        }

        private void FrequentEmail_ItemClick(object sender, ItemClickEventArgs e)
        {
            var user = (User)e.ClickedItem;

            EmailBox.Text = user.emailId;

        }

        private void LogInBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new MainPage();
            // this.Frame.Navigate(typeof(OpeningPage));
        }
    }


}
