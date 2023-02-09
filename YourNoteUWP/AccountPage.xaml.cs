using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.AI.MachineLearning;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YourNoteUWP.Models;
using YourNoteUWP.ViewModels;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourNoteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountPage : Page
    {
        private AccountPageViewModel _accountPageViewModel;
        public AccountPage()
        {
            this.InitializeComponent();
            this.SizeChanged += AccountPage_SizeChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Tuple<Frame, Models.User> tuple = (Tuple<Frame, Models.User>)e.Parameter;
            
            _accountPageViewModel = new AccountPageViewModel(tuple);
        }

        private void AccountPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _accountPageViewModel.AccountPageSizeChanged(sender, e);
        }




        void ChangeVar()
        {

        }
        //When an item from the GridView is clicked 
        private void NotesData_ItemClick(object sender, ItemClickEventArgs e)
        {
                //asdad
        }


        //When an item in the SplitView Panel is clicked
        public void HamburgerOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         


        }

        private void userImage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
       


        }

        private void userImage_PointerExited(object sender, PointerRoutedEventArgs e)
        {


        }


        //The Suggested Options for the AutoSugggetionBox
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
     
        }



        // Handle currentUser selecting an item, in our case just output the selected item (Recently Titles)
        private void RecentSuggestedContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          

        }

        //(Suggestions)
        private void SearchBoxContainerContent_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // PopupName.IsOpen = true;
        }

        private void MainMenuOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}