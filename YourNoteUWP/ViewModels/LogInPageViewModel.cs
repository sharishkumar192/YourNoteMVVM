using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using YourNoteUWP.Models;

namespace YourNoteUWP.ViewModels
{
    internal class LogInPageViewModel : IEmail, INotifyPropertyChanged
    {
        IMainView _manageView;
        private SignUpPageViewModel _signUpPageViewModel;
         public LogInPageViewModel(IMainView view)
        {
            _manageView = view;
            _signUpPageViewModel = new SignUpPageViewModel();

            if (FrequentEmailItemSource == null || FrequentEmailItemSource.Count == 0)
                FrequentEmailBox = Visibility.Collapsed;

            else

            {
                //Column1.Visibility = Visibility.Collapsed;
                FrequentEmailBox = Visibility.Visible;

            }

        }
        public LogInPageViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //------------------------------------------Email TextBox---------------------------------------------------
        private string _emailBoxContent;

         public string EmailBoxContent
        {
            get { return _emailBoxContent; }
            set
            {
                _emailBoxContent = value;
                EmailBoxLostFocus();
                OnPropertyChanged();
            }
        }

        public string IsEmailCheck(string email)
        {
            string checkNullOrEmpty = CheckNullOrEmpty(email);

            if (checkNullOrEmpty != null)
                return checkNullOrEmpty;

            string checkValid = CheckValidEmail(email);


            if (checkValid != null)
                return checkValid;

            return null;
        }

     
        public string CheckNullOrEmpty(string email)
        {
            if (String.IsNullOrEmpty(email) == true)
                return "Email Cant Be Empty";

            return null;
        }

        public string CheckValidEmail(string email)
        {
            if (Models.User.IsValidEmail(email) == false)
                return "Email Id Not Valid";
            return null;
        }

        public void EmailBoxLostFocus()
        {
            string value = IsEmailCheck(EmailBoxContent);
            if (value == null)
            {
                _signUpPageViewModel.EmailBoxToolTipContent = _signUpPageViewModel.EmailToolTipContent = "";
                _signUpPageViewModel.EmailBoxToolTipVisibility = _signUpPageViewModel.EmailToolTipVisibility = _signUpPageViewModel.EmailCheckVisibility = Visibility.Collapsed;
            }
            else
            {
                _signUpPageViewModel.EmailBoxToolTipContent = _signUpPageViewModel.EmailToolTipContent = value;
                _signUpPageViewModel.EmailBoxToolTipVisibility = _signUpPageViewModel.EmailToolTipVisibility = _signUpPageViewModel.EmailCheckVisibility = Visibility.Visible;

            }

        }


        //-------------------------------------------------- Password Box --------------------------------------------------------

        private bool _revealModeCheckBoxIsChecked = false;

        public bool RevealModeCheckBoxIsChecked
        {
            get { return _revealModeCheckBoxIsChecked; }
            set
            {
                _revealModeCheckBoxIsChecked = value;
                RevealModeCheckBoxChanged();
                OnPropertyChanged();
            }
        }


        
        public void RevealModeCheckBoxChanged()
        {
            if (RevealModeCheckBoxIsChecked == true)
            {
                _signUpPageViewModel.PasswordBoxRevealMode = PasswordRevealMode.Visible;

            }
            else
            {
                _signUpPageViewModel.PasswordBoxRevealMode = PasswordRevealMode.Hidden;

            }


        }

        //---------------------------------------------- Frequent Users ListView ----------------------------------------------------
        public ObservableCollection<YourNoteUWP.Models.User> FrequentEmailItemSource
        {
            get { return _frequentEmailItemSource; }

        }


        public void FrequentEmailItemClick(object sender, ItemClickEventArgs e)
        {
            var frequentUser = (Models.User)e.ClickedItem;

            EmailBoxContent = frequentUser.userId;
        }


        private Visibility _frequentEmailBox = Visibility.Collapsed;

        public Visibility FrequentEmailBox
        {
            get { return _frequentEmailBox; }
            set
            {
                _frequentEmailBox = value;

            }
        }


        private ObservableCollection<YourNoteUWP.Models.User> _frequentEmailItemSource = Models.User.GetFrequentUsers();

        //------------------------------------------Page Navigation Buttons---------------------------------------------
      public void LogInButtonClick()
        {
            //_signUpPageViewModel.PasswordBoxPassword
            Tuple<Models.User, bool> validation = Models.User.ValidateLogInUser(EmailBoxContent, "123");

            if (validation.Item2 == true)
            {
                AccountPageViewModel _accountPageViewModel = new AccountPageViewModel();
                _accountPageViewModel.LoggedUser = validation.Item1;
                _manageView.Content = new AccountPage();

            }
            else
            {
                //Credentials Wrong
            }
        }

        public void LogInBackButtonClick()

        {
            _manageView.Content = new MainPage();
        }
    }
}
