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
using Windows.UI.Core;
using System.Windows.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourNoteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page, IMainView
    {
        private SignUpPageViewModel _signUpPageViewModel;
    

        public SignUpPage()
        {
            this.InitializeComponent();
            _signUpPageViewModel = new SignUpPageViewModel(this);
            this.DataContext = _signUpPageViewModel;
            //passwordBox.Height = nameBoxContent.Height;
            //passwordBox.Width = nameBoxContent.Width;
            //repasswordBox.Height = passwordBox.Height;
            //repasswordBox.Width = passwordBox.Width;


        }

    
    }
    }

