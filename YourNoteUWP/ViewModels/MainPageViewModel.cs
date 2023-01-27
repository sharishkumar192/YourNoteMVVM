using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace YourNoteUWP.ViewModels
{
    internal class MainPageViewModel 
    {
        private IMainView _view;
        public MainPageViewModel(IMainView view)
        {
            _view = view;
        }
        public void LoadSignUpPage()
        {
            _view.Content = new SignUpPage();

        }
        public void LoadLogInPage()
        {
            _view.Content = new LogInPage();

        }
    }
}
