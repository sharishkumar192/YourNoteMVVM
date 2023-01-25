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
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _nameboxContent;
        public string NameBoxContent
        {
            get { return _nameboxContent; }
            set
            {
                _nameboxContent = value;

                NameBoxLostFocus();
                OnPropertyChanged("NameBoxContent");

            }
        }

        private string _nameBoxToolTipContent ;
        public string NameBoxToolTipContent
        {
            get { return _nameBoxToolTipContent; }
            set { _nameBoxToolTipContent = value; OnPropertyChanged("NameBoxToolTipContent"); }
        }


        private Visibility _nameBoxToolTipVisibility = Visibility.Collapsed;
        public Visibility NameBoxToolTipVisibility
        {
            get { return _nameBoxToolTipVisibility; }
            set
            {
                _nameBoxToolTipVisibility = value;
                OnPropertyChanged("NameBoxToolTipVisibility");
            }
        }



        private string _nameToolTipContent ;
        public string NameToolTipContent
        {
            get { return _nameToolTipContent; }
            set
            {
                _nameToolTipContent = value;
                OnPropertyChanged("NameToolTipContent");
            }
        }


        private Visibility _nameToolTipVisibility = Visibility.Collapsed;
        public Visibility NameToolTipVisibility
        {
            get { return _nameToolTipVisibility; }
            set
            {
                _nameToolTipVisibility = value;
                OnPropertyChanged("NameToolTipVisibility");

            }
        }

        private Visibility _nameCheckVisibility = Visibility.Collapsed;
        public Visibility NameCheckVisibility
        {
            get { return _nameCheckVisibility; }
            set
            {

                _nameCheckVisibility = value;
                OnPropertyChanged("NameCheckVisibility");
            }
        }

        private string _emailBoxContent;
        public string EmailBoxContent
        {
            get { return _emailBoxContent; }
            set { _emailBoxContent = value;
            OnPropertyChanged("EmailBoxContent")
            }
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

            string value = IsNameCheck(_nameboxContent);
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


      
        private string _pass;
        private string _rpass;


     
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


    }




}
