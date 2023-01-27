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
using YourNoteUWP.ViewModels;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourNoteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogInPage : Page, IMainView
    {
        private LogInPageViewModel _logInPageViewModel;
        private SignUpPageViewModel _signUpPageViewModel;
        public LogInPage()
        {
            this.InitializeComponent();
            _logInPageViewModel = new LogInPageViewModel(this);
            _signUpPageViewModel = new SignUpPageViewModel(this);   
         
        }

    }


}
