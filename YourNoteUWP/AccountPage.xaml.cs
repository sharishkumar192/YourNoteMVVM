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
    public sealed partial class AccountPage : Page, IMainView
    {
     

      

        private AccountPageViewModel _accountPageViewModel;
        public AccountPage()
        {

        }
        public AccountPage(Models.User loggedUser)
        {
            this.InitializeComponent();
            _accountPageViewModel = new AccountPageViewModel(this);
  
          //  Navigation.CompactPaneLength = userImage.Width;

        }

        //Change the value of the Selected Note -> To prevent the firing event of the AutoSuggestionBox TextChanged after choosing the options
       
       
     
       

        //When an item in the SplitView Panel is clicked

        private void userImage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
          
                Navigation.IsPaneOpen = true;


        }

        private void userImage_PointerExited(object sender, PointerRoutedEventArgs e)
        {

            //SplitViewz.IsPaneOpen = false;
                Navigation.IsPaneOpen = false;

        }


    }
}