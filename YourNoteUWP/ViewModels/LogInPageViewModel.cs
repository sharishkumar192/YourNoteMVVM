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
    internal class LogInPageViewModel : INotifyPropertyChanged, IEmail, INullorEmpty 
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

        public string CheckValidEmail(string email)
        {
            return _signUpPageViewModel.CheckValidEmail(email);
        }

        public string CheckNullOrEmpty(string email)
        {
           return  _signUpPageViewModel.CheckNullOrEmpty(email);

        }


        //Text Box

        private string _emailBoxContent = "";
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

        public string EmailBoxPlaceHolderText
        {
            get { return _signUpPageViewModel.EmailBoxPlaceHolderText; }
        }

        // Font Icon
        public Visibility EmailCheckVisibility
        {
            get { return _signUpPageViewModel.EmailCheckVisibility; }
            set
            {
                _signUpPageViewModel.EmailCheckVisibility = value;
                ShowEmailToolTip();
                OnPropertyChanged();
            }
        }

        //Tool Tip
        public string EmailToolTipContent
        {
            get { return _signUpPageViewModel.EmailToolTipContent; }
            set
            {
                _signUpPageViewModel.EmailToolTipContent = value;
                OnPropertyChanged();
            }
        }
        public Visibility EmailToolTipVisibility
        {
            get { return _signUpPageViewModel.EmailToolTipVisibility; }
            set
            {
                _signUpPageViewModel.EmailToolTipVisibility = value;
                OnPropertyChanged();
            }
        }

        public void EmailBoxLostFocus()
        {
            string value = IsEmailCheck(EmailBoxContent);
            if (value == null)
            {
                EmailToolTipContent = "";
                EmailCheckVisibility = Visibility.Collapsed;
            }
            else
            {
                EmailToolTipContent = value;
                EmailCheckVisibility = Visibility.Visible;

            }

        }

        public void ShowEmailToolTip()
        {
            if (EmailCheckVisibility == Visibility.Collapsed)
            {
                EmailToolTipVisibility = Visibility.Collapsed;
            }
            else
            {
                EmailToolTipVisibility = Visibility.Visible;
            }
        }

        public void EmailBoxTextChanged()
        {
            EmailToolTipContent = "";
            EmailCheckVisibility = Visibility.Collapsed;
        }

        //-------------------------------------------------- Password Box --------------------------------------------------------


        //Password Box
        private string _passwordBoxPassword = "";
        public string PasswordBoxPassword
        {
            get { return _passwordBoxPassword; }
            set
            {
                _passwordBoxPassword = value;
                PasswordBoxLostFocus();
                OnPropertyChanged();
            }
        }

        public PasswordRevealMode PasswordBoxRevealMode
        {
            get { return _signUpPageViewModel.PasswordBoxRevealMode; }
            set
            {
                _signUpPageViewModel.PasswordBoxRevealMode = value;
                OnPropertyChanged();
            }
        }

        public string PasswordBoxPlaceHolderText

        {
            get { return _signUpPageViewModel.PasswordBoxPlaceHolderText; }
        }

        //Check Box
        public bool RevealModeCheckBoxIsChecked
        {
            get { return _signUpPageViewModel.RevealModeCheckBoxIsChecked; }
            set
            {
                _signUpPageViewModel.RevealModeCheckBoxIsChecked = value;
                OnPropertyChanged();
            }
        }


        // Font Icon
        public Visibility PasswordCheckVisibility
        {
            get { return _signUpPageViewModel.PasswordCheckVisibility; }
            set {  _signUpPageViewModel.PasswordCheckVisibility = value;
                ShowPasswordToolTip();
                OnPropertyChanged();

            }
        }

        //Tool Tip
        public string PasswordToolTipContent
        {
            get { return _signUpPageViewModel.PasswordToolTipContent; }
            set { 
                _signUpPageViewModel.PasswordToolTipContent = value;
                OnPropertyChanged();

            }
        }

        public Visibility PasswordToolTipVisibility
        {
            get { return _signUpPageViewModel.PasswordToolTipVisibility; }
            set
            {
                _signUpPageViewModel.PasswordToolTipVisibility = value;
                OnPropertyChanged();
            }
        }

        public void RevealModeCheckBoxChecked()
        {
            if (RevealModeCheckBoxIsChecked == false)
            {
                RevealModeCheckBoxIsChecked = true;
                PasswordBoxRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                RevealModeCheckBoxIsChecked = false;
                PasswordBoxRevealMode = PasswordRevealMode.Hidden;
            }
        }

        public void PasswordBoxLostFocus()
        {
            string value = _signUpPageViewModel.IsPasswordCheck(PasswordBoxPassword);
            if (value == null)
            {
                PasswordToolTipContent = "";
                PasswordCheckVisibility = Visibility.Collapsed;
            }
            else
            {
                PasswordToolTipContent = value;
                PasswordCheckVisibility = Visibility.Visible;


            }
        }

        public void PasswordBoxTextChanged()
        {
            PasswordToolTipContent = "";
            PasswordCheckVisibility = Visibility.Collapsed;
        }


        public void ShowPasswordToolTip()
        {
            if (PasswordCheckVisibility == Visibility.Collapsed)
            {
                PasswordToolTipVisibility = Visibility.Collapsed;
            }
            else
            {
                PasswordToolTipVisibility = Visibility.Visible;
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

            EmailBoxLostFocus();
            PasswordBoxLostFocus();
            if (EmailCheckVisibility == Visibility.Collapsed &&
                PasswordCheckVisibility == Visibility.Collapsed)
            {
                Tuple<Models.User, bool> validation = Models.User.ValidateLogInUser(EmailBoxContent, PasswordBoxPassword);
                if (validation.Item2 == true)
                {
                    _manageView.Content = new AccountPage(validation.Item1);
                }
                else
                {
                    //TODO -> Case : Credentials are wrong
                }

            }

        }

        public void LogInBackButtonClick()

        {
            _manageView.Content = new MainPage();
        }














    }
}
