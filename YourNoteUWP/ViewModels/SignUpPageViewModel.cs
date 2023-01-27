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
    internal class SignUpPageViewModel : INotifyPropertyChanged, IEmailExt
    {
        private IMainView _view;
        private LogInPageViewModel _logInPageViewModel ;
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

        private string _nameboxContent;
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

        private string _nameBoxToolTipContent;
        public string NameBoxToolTipContent
        {
            get { return _nameBoxToolTipContent; }
            set
            {
                _nameBoxToolTipContent = value;
                OnPropertyChanged();
            }
        }


        private Visibility _nameBoxToolTipVisibility = Visibility.Collapsed;
        public Visibility NameBoxToolTipVisibility
        {
            get { return _nameBoxToolTipVisibility; }
            set
            {
                _nameBoxToolTipVisibility = value;
                OnPropertyChanged();
            }
        }



        private string _nameToolTipContent;
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

        private Visibility _nameCheckVisibility = Visibility.Collapsed;
        public Visibility NameCheckVisibility
        {
            get { return _nameCheckVisibility; }
            set
            {

                _nameCheckVisibility = value;
                OnPropertyChanged();
            }
        }
        public string IsNameCheck(string check)
        {
            if (String.IsNullOrEmpty(check) == true)
                return "Name Cant Be Empty";
            return null;
        }
        public void NameBoxLostFocus()
        {

            string value = IsNameCheck(NameBoxContent);
            if (value == null)
            {
                NameBoxToolTipContent = NameToolTipContent = "";
                NameBoxToolTipVisibility = NameToolTipVisibility = NameCheckVisibility = Visibility.Collapsed;

            }
            else
            {
                NameBoxToolTipContent = NameToolTipContent = value;
                NameBoxToolTipVisibility = NameToolTipVisibility = NameCheckVisibility = Visibility.Visible;

            }
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

        private string _emailBoxToolTipContent;

        public string EmailBoxToolTipContent
        {
            get { return _emailBoxToolTipContent; }
            set
            {
                _emailBoxToolTipContent = value;
                OnPropertyChanged();
            }
        }



        private Visibility _emailBoxToolTipVisibility = Visibility.Collapsed;

        public Visibility EmailBoxToolTipVisibility
        {
            get { return _emailBoxToolTipVisibility; }
            set
            {
                _emailBoxToolTipVisibility = value;
                OnPropertyChanged();
            }
        }




        private Visibility _emailCheckVisibility = Visibility.Collapsed;

        public Visibility EmailCheckVisibility
        {
            get { return _emailCheckVisibility; }
            set
            {
                _emailCheckVisibility = value;
                OnPropertyChanged();
            }
        }



        private string _emailToolTipContent;

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

        public string CheckAlreadyExistingEmail(string email)
        {
            if (User.CheckEmail(email) == true)
                return "The Email Id Already Exists!";
            return null;
        }
        public string CheckNullOrEmpty(string email)
        {
          return  _logInPageViewModel.CheckNullOrEmpty(email);
        }

        public string CheckValidEmail(string email)
        {
            return _logInPageViewModel.CheckValidEmail(email);
        }



        public void EmailBoxLostFocus()
        {
            string value = IsEmailCheck(EmailBoxContent);
            if (value == null)
            {
                EmailBoxToolTipContent = EmailToolTipContent = "";
                EmailBoxToolTipVisibility = EmailToolTipVisibility = EmailCheckVisibility = Visibility.Collapsed;
            }
            else
            {
                EmailBoxToolTipContent = EmailToolTipContent = value;
                EmailBoxToolTipVisibility = EmailToolTipVisibility = EmailCheckVisibility = Visibility.Visible;

            }

        }

        public void EmailBoxTextChanged()
        {
            EmailCheckVisibility = Visibility.Collapsed;

        }

        //------------------------------------------Password Box---------------------------------------------------

        private string _passwordBoxPassword;

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


        public void PasswordBoxTextChanged()
        {
            PasswordCheckVisibility = Visibility.Collapsed;
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


        private string _passwordBoxToolTipContent;

        public string PasswordBoxToolTipContent
        {
            get { return _passwordBoxToolTipContent; }
            set
            {
                _passwordBoxToolTipContent = value;
                OnPropertyChanged();
            }
        }

        private Visibility _passwordBoxToolTipVisibility = Visibility.Collapsed;

        public Visibility PasswordBoxToolTipVisibility
        {
            get { return _passwordBoxToolTipVisibility; }
            set
            {
                _passwordBoxToolTipVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _passwordCheckVisibility = Visibility.Collapsed;

        public Visibility PasswordCheckVisibility
        {
            get { return _passwordCheckVisibility; }
            set
            {
                _passwordCheckVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _passwordToolTipContent;

        public string PasswordToolTipContent
        {
            get { return _passwordToolTipContent; }
            set
            {
                _passwordToolTipContent = value;
                OnPropertyChanged();
            }
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

        public string IsPasswordCheck(string check)
        {

            if (String.IsNullOrEmpty(check) == true)
                return "Password Cant Be Empty";
            else if (check.Length < 2)
                return "Password Must Be Alteast Length 3";

            return null;
        }
        public void PasswordBoxLostFocus()
        {
            string value = IsPasswordCheck(PasswordBoxPassword);
            if (value == null)
            {
                PasswordBoxToolTipContent = PasswordToolTipContent = "";
                PasswordBoxToolTipVisibility = PasswordCheckVisibility = Visibility.Collapsed;
            }
            else
            {
                PasswordBoxToolTipContent = PasswordToolTipContent = value;
                PasswordBoxToolTipVisibility = PasswordCheckVisibility = Visibility.Visible;

            }
        }

        public void RevealModeCheckBoxChanged()
        {
            if (RevealModeCheckBoxIsChecked == true)
            {
                PasswordBoxRevealMode = PasswordRevealMode.Visible;
                RPasswordBoxRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                PasswordBoxRevealMode = PasswordRevealMode.Hidden;
                RPasswordBoxRevealMode = PasswordRevealMode.Hidden;
            }
        }
        //------------------------------------------Re Password Box---------------------------------------------------

        private string _rPasswordBoxPassword;
        public string RPasswordBoxPassword
        {
            get { return _rPasswordBoxPassword; }
            set
            {
                _rPasswordBoxPassword = value;
                RPasswordBoxLostFocus();
                OnPropertyChanged();
            }
        }

        public void RPasswordBoxPasswordChanged()
        {
            RPasswordCheckVisibility = Visibility.Collapsed;
        }

        private PasswordRevealMode _rPasswordBoxRevealMode = PasswordRevealMode.Hidden;

        public PasswordRevealMode RPasswordBoxRevealMode
        {
            get { return _rPasswordBoxRevealMode; }
            set
            {
                _rPasswordBoxRevealMode = value;
                OnPropertyChanged();
            }
        }

        private string _rPasswordBoxToolTipContent;

        public string RPasswordBoxToolTipContent
        {
            get { return _rPasswordBoxToolTipContent; }
            set
            {
                _rPasswordBoxToolTipContent = value;
                OnPropertyChanged();
            }
        }

        private Visibility _rPasswordBoxToolTipVisibility = Visibility.Collapsed;

        public Visibility RPasswordBoxToolTipVisibility
        {
            get { return _rPasswordBoxToolTipVisibility; }
            set
            {
                _rPasswordBoxToolTipVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _rPasswordCheckVisibility = Visibility.Collapsed;

        public Visibility RPasswordCheckVisibility
        {
            get { return _rPasswordCheckVisibility; }
            set
            {
                _rPasswordCheckVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _rPasswordToolTipContent;

        public string RPasswordToolTipContent
        {
            get { return _rPasswordToolTipContent; }
            set
            {
                _rPasswordToolTipContent = value;
                OnPropertyChanged();

            }
        }


        public string IsRPasswordCheck(string check1, string check2)
        {

            string value = IsPasswordCheck(check2);
            if (value == null)
            {
                if (check1 == check2)
                    return null;
                else
                    return "Password Not Matching!";
            }
            else if (value == "Password Cant Be Empty" || value == "Password Must Be Alteast Length 3")
                return null;
            else if (String.IsNullOrEmpty(check2) == true)
                return "Retype The Password!";

            return null;

        }
        public void RPasswordBoxLostFocus()
        {
            string value = IsRPasswordCheck(RPasswordBoxPassword, PasswordBoxPassword);
            if (value == null)
            {
                RPasswordBoxToolTipContent = RPasswordToolTipContent = "";
                RPasswordBoxToolTipVisibility = RPasswordCheckVisibility = Visibility.Collapsed;
            }
            else
            {
                RPasswordBoxToolTipContent = RPasswordToolTipContent = value;
                RPasswordBoxToolTipVisibility = RPasswordCheckVisibility = Visibility.Visible;
            }
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
            RPasswordBoxLostFocus();

            if (NameCheckVisibility == Visibility.Collapsed &&
                EmailCheckVisibility == Visibility.Collapsed &&
                PasswordCheckVisibility == Visibility.Collapsed &&
                RPasswordCheckVisibility == Visibility.Collapsed)
            {

                User.NewUserInsertion(NameBoxContent, EmailBoxContent, RPasswordBoxPassword);
                _view.Content = new LogInPage();
            }
        }

       
    }




}