using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using YourNoteUWP.Models;
namespace YourNoteUWP.ViewModels
{
    internal class SignUpPageViewModel : INotifyPropertyChanged
    {

        private string _name;

        public Visibility NameBoxToolTipVisibility
        {
            get { return _nameBoxToolTipVisibility; }
            set { 
                _nameBoxToolTipVisibility = value ;
                OnPropertyChanged();
            }
        }

        private Visibility _nameBoxToolTipVisibility = Visibility.Collapsed;
        private string _nameBoxToolTipContent;

        private string _nameToolTipContent;
        private Visibility _nameToolTipVisibility;

        private Visibility _nameCheckVisibility;
        private string _emailId;
        private string _pass;
        private string _rpass;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string NameBox
        {
            get { return _name; }
            set { _name = value; }
        }

        public string NameBoxToolTipContent
        {
            get { return _nameBoxToolTipContent; }
            set { _nameBoxToolTipContent = value; }
        }

        public string NameToolTipContent
        {
            get { return _nameToolTipContent; }
            set {  _nameBoxToolTipContent = value;  }
        }

        public Visibility NameToolTipVisibility
        {
            get { return _nameToolTipVisibility; }
            set {
               // OnPropertyChanged(nameof(NameToolTipVisibility));
                _nameToolTipVisibility = value;
                OnPropertyChanged();

            }
        }

        public Visibility NameCheckVisibility
        {
            get { return _nameCheckVisibility; }
            set {
               // OnPropertyChanged(nameof(NameCheckVisibility));
                _nameCheckVisibility = value;
                OnPropertyChanged();
            }
        }
        public string EmailId
        {
            get { return _emailId; }
            set { _emailId = value; }
        }

        public string Password
        {
            get { return _pass; }
            set { _pass = value; }
        }

        public string RPassword
        {
            get { return _rpass; }
            set { _rpass = value; }
        }



        public string IsNameCheck(string check)
        {
            if (String.IsNullOrEmpty(check) == true)
                return "Name Cant Be Empty";
            return null;
        }

        public string IsEmailCheck(string check)
        {
            if (String.IsNullOrEmpty(check) == true)
                return "Email Cant Be Empty";
            else if (User.IsValidEmail(check) == false)
                return "Email Id Not Valid";
            else if (User.CheckEmail(check) == true)
                return "The Email Id Already Exists!";
            return null;
        }

        public string IsPasswordCheck(string check)
        {

            if (String.IsNullOrEmpty(check) == true)
                return "Password Cant Be Empty";
            else if (check.Length < 2)
                return "Password Must Be Alteast Length 3";

            return null;
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


        public void NameBoxLostFocus()
        {

            string value = IsNameCheck(_name);
            if (value == null)
            {
                _nameBoxToolTipContent = _nameToolTipContent = "";
                _nameBoxToolTipVisibility = _nameToolTipVisibility = _nameCheckVisibility = Visibility.Collapsed;
            }
            else
            {
                _nameBoxToolTipContent = _nameToolTipContent = value;
                _nameBoxToolTipVisibility = _nameToolTipVisibility = _nameCheckVisibility = Visibility.Visible;

            }
        }


    }




}
