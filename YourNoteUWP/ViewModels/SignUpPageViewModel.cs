using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using YourNoteUWP.Models;
namespace YourNoteUWP.ViewModels
{
    internal class SignUpPageViewModel : INotifyPropertyChanged, INullorEmpty, IEmailExt, IPassword
    {
        private IMainView _view;
        private LogInPageViewModel _logInPageViewModel;
        public SignUpPageViewModel(IMainView view)
        {
            _view = view;
            _logInPageViewModel = new LogInPageViewModel();
        }

        public SignUpPageViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //------------------------------------------Name TextBox---------------------------------------------------
        public string CheckNullOrEmpty(string email)
        {
            if (String.IsNullOrEmpty(email) == true)
                return "This field cant be empty";

            return null;
        }
        public string IsNameCheck(string check)
        {
            string checkNullOrEmpty = CheckNullOrEmpty(check);
            if (checkNullOrEmpty != null)
                return checkNullOrEmpty;
            return null;
        }

        //Text Box

        private string _nameboxContent = "";
        public string NameBoxContent
        {
            get { return _nameboxContent; }
            set
            {
                _nameboxContent = value;

                NameBoxLostFocus();
                OnPropertyChanged();

            }
        }

        private string _nameBoxPlaceHolderText = "Name"; 

        public string NameBoxPlaceHolderText
        {
            get { return _nameBoxPlaceHolderText; }

        }


        //Font Icon
        private Visibility _nameCheckVisibility = Visibility.Collapsed;
        public Visibility NameCheckVisibility
        {
            get { return _nameCheckVisibility; }
            set
            {

                _nameCheckVisibility = value;
                ShowNameToolTip();
                OnPropertyChanged();
            }
        }


        // Tool Tip

        private string _nameToolTipContent = "";
        public string NameToolTipContent
        {
            get { return _nameToolTipContent; }
            set
            {
                _nameToolTipContent = value;
                OnPropertyChanged();
            }
        }


        private Visibility _nameToolTipVisibility = Visibility.Collapsed;
        public Visibility NameToolTipVisibility
        {
            get { return _nameToolTipVisibility; }
            set
            {
                _nameToolTipVisibility = value;
                OnPropertyChanged();

            }
        }

        public void NameBoxTextChanged()
        {
            NameToolTipContent = "";
            NameCheckVisibility = Visibility.Collapsed;
        }

        public void NameBoxLostFocus()
        {

            string value = IsNameCheck(NameBoxContent);
            if (value == null)
            {
                NameToolTipContent = "";
                NameCheckVisibility = Visibility.Collapsed;

            }
            else
            {
                NameToolTipContent = value;
                NameCheckVisibility = Visibility.Visible;

            }
        }

        public void ShowNameToolTip()
        {
            if (NameCheckVisibility == Visibility.Collapsed)
            {
                NameToolTipVisibility = Visibility.Collapsed;
            }
            else
            {
                NameToolTipVisibility = Visibility.Visible;
            }
        }


        //------------------------------------------Email TextBox---------------------------------------------------



        public string CheckAlreadyExistingEmail(string email)
        {
            if (User.CheckEmail(email) == true)
                return "An account already exists for this email address";
            return null;
        }

        public string CheckValidEmail(string email)
        {
            if (Models.User.IsValidEmail(email) == false)
                return "This is not a valid email address";
            return null;

        }

        public string IsEmailCheck(string email)
        {
            string checkNullOrEmpty = CheckNullOrEmpty(email);

            if (checkNullOrEmpty != null)
                return checkNullOrEmpty;

            string checkValid = CheckValidEmail(email);


            if (checkValid != null)
                return checkValid;

            string existedMail = CheckAlreadyExistingEmail(email);


            if (existedMail != null)
                return existedMail;

            return null;
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

        private string _emailBoxPlaceHolderText = "Email address";
        public string EmailBoxPlaceHolderText
        {
            get { return _emailBoxPlaceHolderText; }
        }


        // Font Icon

        private Visibility _emailCheckVisibility = Visibility.Collapsed;

        public Visibility EmailCheckVisibility
        {
            get { return _emailCheckVisibility; }
            set
            {
                _emailCheckVisibility = value;
                ShowEmailToolTip();
                OnPropertyChanged();
            }
        }

        //Tool Tip

        private string _emailToolTipContent = "";

        public string EmailToolTipContent
        {
            get { return _emailToolTipContent; }
            set
            {
                _emailToolTipContent = value;
                OnPropertyChanged();
            }
        }

        private Visibility _emailToolTipVisibility = Visibility.Collapsed;

        public Visibility EmailToolTipVisibility
        {
            get { return _emailToolTipVisibility; }
            set
            {
                _emailToolTipVisibility = value;
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

        //------------------------------------------Password Box---------------------------------------------------
        public string IsPasswordCheck(string check)
        {
            string checkNullOrEmpty = CheckNullOrEmpty(check);

            if (checkNullOrEmpty != null)
                return checkNullOrEmpty;

            string checkLength = CheckLength(check);


            if (checkLength != null)
                return checkLength;

            return null;
        }

        public string CheckLength(string email)
        {
            if (email.Length < 2)
                return "Password cannot be less than 3 characters";

            return null;
        }



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


        private PasswordRevealMode _passwordBoxRevealMode = PasswordRevealMode.Hidden;
        public PasswordRevealMode PasswordBoxRevealMode
        {
            get { return _passwordBoxRevealMode; }
            set
            {
                _passwordBoxRevealMode = value;
                OnPropertyChanged();
            }
        }



        private string _passwordBoxPlaceHolderText = "Password";
        public string PasswordBoxPlaceHolderText
        {
            get { return _passwordBoxPlaceHolderText; }
        }

        //Font Icon
        private Visibility _passwordCheckVisibility = Visibility.Collapsed;
        public Visibility PasswordCheckVisibility
        {
            get { return _passwordCheckVisibility; }
            set
            {
                _passwordCheckVisibility = value;
                ShowPasswordToolTip();
                OnPropertyChanged();
            }
        }

        // Tool Tip
        private string _passwordToolTipContent = "";

        public string PasswordToolTipContent
        {
            get { return _passwordToolTipContent; }
            set
            {
                _passwordToolTipContent = value;
                OnPropertyChanged();
            }
        }


        private Visibility _passwordToolTipVisibility = Visibility.Collapsed;
        public Visibility PasswordToolTipVisibility
        {
            get { return _passwordToolTipVisibility; }
            set
            {
                _passwordToolTipVisibility = value;
                OnPropertyChanged();
            }
        }


        // Check Box
        private bool _revealModeCheckBoxIsChecked = false;
        public bool RevealModeCheckBoxIsChecked
        {
            get { return _revealModeCheckBoxIsChecked; }
            set
            {
                _revealModeCheckBoxIsChecked = value;
                OnPropertyChanged();
            }
        }


        public void PasswordBoxTextChanged()
        {
            PasswordToolTipContent = "";
            PasswordCheckVisibility = Visibility.Collapsed;
        }

        public void PasswordBoxLostFocus()
        {
            string value = IsPasswordCheck(PasswordBoxPassword);
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

        public void RevealModeCheckBoxChecked()
        {

            if (RevealModeCheckBoxIsChecked == false)
            {
                RevealModeCheckBoxIsChecked = true;
                PasswordBoxRevealMode = PasswordRevealMode.Visible;
                ConfirmPasswordBoxRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                RevealModeCheckBoxIsChecked = false;
                PasswordBoxRevealMode = PasswordRevealMode.Hidden;
                ConfirmPasswordBoxRevealMode = PasswordRevealMode.Hidden;
            }
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

        //------------------------------------------Re Password Box---------------------------------------------------
        public string IsConfirmPasswordCheck(string password, string confirmPassword)
        {
            string value = IsPasswordCheck(password);
            if (value == null && password != confirmPassword)
            {
                return "Password and Confirm Password does not match";
            }
            return null;

        }


        //Password Box
        private string _confirmPasswordBoxPassword = "";
        public string ConfirmPasswordBoxPassword
        {
            get { return _confirmPasswordBoxPassword; }
            set
            {
                _confirmPasswordBoxPassword = value;
                ConfirmPasswordBoxLostFocus();
                OnPropertyChanged();
            }
        }


        private PasswordRevealMode _confirmPasswordBoxRevealMode = PasswordRevealMode.Hidden;
        public PasswordRevealMode ConfirmPasswordBoxRevealMode
        {
            get { return _confirmPasswordBoxRevealMode; }
            set
            {
                _confirmPasswordBoxRevealMode = value;
                OnPropertyChanged();
            }
        }



        private string _confirmPasswordBoxPlaceHolderText = "Confirm password";
        public string ConfirmPasswordBoxPlaceHolderText
        {
            get { return _confirmPasswordBoxPlaceHolderText; }
        }


        //Font Icon
        private Visibility _confirmPasswordCheckVisibility = Visibility.Collapsed;
        public Visibility ConfirmPasswordCheckVisibility
        {
            get { return _confirmPasswordCheckVisibility; }
            set
            {
                _confirmPasswordCheckVisibility = value;
                ShowConfirmPasswordToolTip();
                OnPropertyChanged();
            }
        }


        // Tool Tip
        private string _confirmPasswordToolTipContent = "";

        public string ConfirmPasswordToolTipContent
        {
            get { return _confirmPasswordToolTipContent; }
            set
            {
                _confirmPasswordToolTipContent = value;
                OnPropertyChanged();

            }
        }


        private Visibility _confirmPasswordToolTipVisibility = Visibility.Collapsed;

        public Visibility ConfirmPasswordToolTipVisibility
        {
            get { return _confirmPasswordToolTipVisibility; }
            set
            {
                _confirmPasswordToolTipVisibility = value;
                OnPropertyChanged();
            }
        }




        public void ConfirmPasswordBoxPasswordChanged()
        {
            ConfirmPasswordToolTipContent = "";
            ConfirmPasswordCheckVisibility = Visibility.Collapsed;
        }

        public void ConfirmPasswordBoxLostFocus()
        {
            string value = IsConfirmPasswordCheck(PasswordBoxPassword, ConfirmPasswordBoxPassword);
            if (value == null)
            {

                ConfirmPasswordToolTipContent = "";
                ConfirmPasswordCheckVisibility = Visibility.Collapsed;
            }
            else
            {
                ConfirmPasswordToolTipContent = value;
                ConfirmPasswordCheckVisibility = Visibility.Visible;

            }
        }

        public void ShowConfirmPasswordToolTip()
        {
            if (ConfirmPasswordCheckVisibility == Visibility.Collapsed)
            {
                ConfirmPasswordToolTipVisibility = Visibility.Collapsed;
            }
            else
            {
                ConfirmPasswordToolTipVisibility = Visibility.Visible;
            }
        }












        private bool _logInButtonIsEnabled = false ;

        public bool LogInButtonIsEnabled
        {
            get { return _logInButtonIsEnabled ; }
            set { _logInButtonIsEnabled  = value; }
        }






        public void SignUpBackButtonClick()
        {
            _view.Content = new MainPage();


        }






        public void LogInButtonClick()
        {
            NameBoxLostFocus();
            EmailBoxLostFocus();
            PasswordBoxLostFocus();
            ConfirmPasswordBoxLostFocus();

            if (NameCheckVisibility == Visibility.Collapsed &&
                EmailCheckVisibility == Visibility.Collapsed &&
                PasswordCheckVisibility == Visibility.Collapsed &&
                ConfirmPasswordCheckVisibility == Visibility.Collapsed)
            {

                User.NewUserInsertion(NameBoxContent, EmailBoxContent, PasswordBoxPassword);
                _view.Content = new LogInPage();
            }
        }
    }




}