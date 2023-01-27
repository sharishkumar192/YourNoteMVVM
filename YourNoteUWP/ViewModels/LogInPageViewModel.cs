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

        //---------------------------------------------Email Box----------------------------------------------------

        //------------------------------------------Email TextBox---------------------------------------------------
        private string _emailBoxContent;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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


        private ObservableCollection<YourNoteUWP.Models.User> _frequentEmailItemSource = Models.User.GetFrequentUsers();

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

























        public void LogInButtonClick()
        {
            Tuple<Models.User, bool> validation = Models.User.ValidateLogInUser(EmailBoxContent, "123");

            if (validation.Item2 == true)
            {
               _manageView.Content = new AccountPage(validation.Item1);

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
