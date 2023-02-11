using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YourNoteUWP.Models;
using YourNoteUWP.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace YourNoteUWP
{
    public sealed partial class NoteContent : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        ObservableCollection<YourNoteUWP.Models.User> usersToShare = null;
        Models.User noteOwner = null;
        private static Note _displayNote ;
        private Note _selectedNote = null;
        private string oldTitle;
        private string oldContent;
        private DispatcherTimer _dispatcherTimer;
        private NoteContentViewModel _noteContentViewModel;

        public NoteContent()
        {
            this.InitializeComponent();
            
        }

        public void Hello(Note note)
        {
            _displayNote = note;
            TitleOfNoteText = _displayNote.title;
            ContentOfNoteText = _displayNote.content;
        }






        private void noteCloseButton_Click(object sender, RoutedEventArgs e)
        {
            _noteContentViewModel = NoteContentViewModel.NoteViewModel;
            _noteContentViewModel.NoteUpdation(_displayNote);
        }



        private async void NoteShared()
        {
            MessageDialog showDialog;

            showDialog = new MessageDialog("Note Has Been Shared!");
            showDialog.Commands.Add(new UICommand("Ok")
            {
                Id = 0
            });
            showDialog.DefaultCommandIndex = 0;
            var result = await showDialog.ShowAsync();
        }




        private static SolidColorBrush GetSolidColorBrush(string hex)
        {
            if (hex == null)
                hex = "#fdefad";
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb((byte)255, r, g, b));
            return myBrush;
        }

        public void DispatcherTimerSetup()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            _dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            _selectedNote.title = TitleOfNoteText;
            _selectedNote.content = ContentOfNoteText;
            _noteContentViewModel = NoteContentViewModel.NoteViewModel;
            _noteContentViewModel.NoteUpdation(_displayNote);
          
        }



        //---------------------------Note Background--------------------------------------------------
        private SolidColorBrush _noteContentBackground;
        public SolidColorBrush NoteContentBackground
        {
            get { return _noteContentBackground; }
            set
            {
                _noteContentBackground = value;
                OnPropertyChanged();

            }
        }

        public void TransparentClick(object sender, RoutedEventArgs e)
        {
            Popup p = this.Parent as Popup;

            // close the Popup
            if (p != null) { p.IsOpen = false; }

            //    this.Content = new AccountPage(noteOwner);
        }




        //----------------------------Title Text Box---------------------------------------------------
        private bool _titleOfNoteIsReadOnly = true;
        public bool TitleOfNoteIsReadOnly
        {
            get { return _titleOfNoteIsReadOnly; }
            set
            {
                _titleOfNoteIsReadOnly = value;
                OnPropertyChanged();
            }
        }

        private string _titleOfNoteText;
        public string TitleOfNoteText
        {
            get { return _titleOfNoteText; }
            set
            {
                _titleOfNoteText = value;
                OnPropertyChanged();
            }
        }

        private string _oldTitle;
        public string OldTitle
        {
            get { return _oldTitle; }
            set
            {
                _oldTitle = value;
            }
        }

        public void TitleOfNoteTapped()
        {
            TitleOfNoteIsReadOnly = false;
            OldTitle = TitleOfNoteText;
            DispatcherTimerSetup();
        }



        //----------------------------Note Close Button ---------------------------------------------------
        public void NoteCloseButtonClick(object sender, RoutedEventArgs e)
        {
            //_selectedNote.title = TitleOfNoteText;
            //  _selectedNote.content = ContentOfNoteText;
            //    Note.NoteUpdation(_selectedNote);
            Popup p = this.Parent as Popup;

            // close the Popup
            if (p != null) { p.IsOpen = false; }

      

            //    this.Content = new AccountPage(noteOwner);
        }

        //----------------------------Note Share Button ---------------------------------------------------
        public void NoteShareButtonClick()
        {

        }

        //----------------------------Note Delete Button ---------------------------------------------------
        public void NoteDeleteButtonClick()
        {
            // Note.DeleteNote(_selectedNote.noteId);
            // NoteDisplayPopUpIsOpen = false;

        }
        //----------------------------Content Text Box ---------------------------------------------------

        private string _contentOfNoteText;

        public string ContentOfNoteText
        {
            get { return _contentOfNoteText; }
            set
            {
                _contentOfNoteText = value;
                OnPropertyChanged();
            }
        }



        private bool _contentOfNoteIsReadOnly = true;

        public bool ContentOfNoteIsReadOnly
        {
            get { return _contentOfNoteIsReadOnly; }
            set { _contentOfNoteIsReadOnly = value; }
        }






        private string _OldContent;

        public string OldContent
        {
            get { return _OldContent; }
            set { _OldContent = value; }
        }


        public void ContentOfNoteTapped()
        {
            ContentOfNoteIsReadOnly = false;
            OldContent = ContentOfNoteText;
            DispatcherTimerSetup();
        }

        private void UsersToShareView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }


    }
}
