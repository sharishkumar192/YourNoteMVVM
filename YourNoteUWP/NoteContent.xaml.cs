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
     //  private Type _parentPage;
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        ObservableCollection<YourNoteUWP.Models.User> usersToShare = null;
        Models.User noteOwner = null;
        private static Note _displayNote ;
      
        private string oldTitle;
        private string oldContent;
        private NoteContentViewModel _noteContentViewModel;

        private DispatcherTimer _titleTimer = null;

        public DispatcherTimer TitleTimer
        {
            get { return _titleTimer; }
            set { _titleTimer = value;
                OnPropertyChanged();
            }
        }

        private DispatcherTimer _contentTimer;

        public DispatcherTimer ContentTimer
        {
            get { return _contentTimer; }
            set { _contentTimer = value;
                OnPropertyChanged();
            }
        }

        public NoteContent()
        {
            this.InitializeComponent();
          

            TitleOfNote.AddHandler(TappedEvent, new TappedEventHandler(TitleOfNoteTapped), true);
            ContentOfNote.AddHandler(TappedEvent, new TappedEventHandler(ContentOfNoteTapped), true);

        }

        public void Hello(Note snote)
        {
            _displayNote = snote;
            TitleOfNoteText = _displayNote.title;
            ContentOfNoteText = _displayNote.content;
            NoteContentBackground = GetSolidColorBrush(_displayNote.noteColor);
          
            //(this.Parent as ).NoteDisplayPopUpOpened();
            
            //  _parentPage = parentPage;
            // Page page = this.Parent as parenPage;


            //.NoteDisplayPopUpOpened();
            //page.;
            //.NoteDisplayPopUpOpened();
            //_uIElement = nameProperty;
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

        private void DispatcherTimerStart(DispatcherTimer _dispatcherTimer)
        {
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            _dispatcherTimer.Start();
        }
       
        private void DispatcherTimer_Tick(object sender, object e)
        {
            if (TitleTimer == null || ContentTimer == null)
            {
                    DispatcherTimer senderTimer = (DispatcherTimer)sender;
                    senderTimer.Stop();

            }
            else
            {
                _displayNote.title = TitleOfNoteText;
                _displayNote.content = ContentOfNoteText;
                _noteContentViewModel = NoteContentViewModel.NoteViewModel;
                _noteContentViewModel.NoteUpdation(_displayNote);

            }

        }
        private void DispatcherTimerStop(DispatcherTimer dispatcherTimer)
        {
            //dispatcherTimer.Tick = DispatcherTimer_Tick;
            if(dispatcherTimer != null )
            dispatcherTimer.Stop();
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

        public void TransparentTapped(object sender, TappedRoutedEventArgs e)
        {
            Popup p = this.Parent as Popup;
           /// p.Closed;
            // close the Popup
            if (p != null) { p.IsOpen = false; }
         //   NoteDisplayIsOpen = false;
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

        private bool _titleOfNoteIsTapped = true;

        public bool TitleOfNoteIsTapped
        {
            get { return _titleOfNoteIsTapped; }
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

        public void TitleOfNoteTapped(object sender, TappedRoutedEventArgs e)
        {
            TitleOfNoteIsReadOnly = false;
            OldTitle = TitleOfNoteText;
            TitleTimer = new DispatcherTimer();
            DispatcherTimerStart(TitleTimer);
        }



        //----------------------------Note Close Button ---------------------------------------------------
        public void NoteCloseButtonClick(object sender, RoutedEventArgs e)
        {
            //_displayNote.title = TitleOfNoteText;
            //  _displayNote.content = ContentOfNoteText;
            //    Note.NoteUpdation(_displayNote);
            _noteContentViewModel = NoteContentViewModel.NoteViewModel;
            _noteContentViewModel.NoteUpdation(_displayNote);
            if(TitleOfNoteIsReadOnly == false)
            DispatcherTimerStop(TitleTimer);
            if (ContentOfNoteIsReadOnly == false)
                DispatcherTimerStop(ContentTimer);
            TitleTimer = ContentTimer = null;

       //     AccountPage accountPage = new AccountPage();
      //      accountPage.NoteDisplayPopUpClosed();
                           Popup p = this.Parent as Popup;
             
                 if (p != null) { p.IsOpen = false; }


            //((MyPageName)this.Parent).CustomMethod(); 

            //  Popup.PopOutStoryboard.Begin();


            //    this.Content = new AccountPage(noteOwner);
        }

        //----------------------------Note Share Button ---------------------------------------------------
        public void NoteShareButtonClick()
        {

        }

        //----------------------------Note Delete Button ---------------------------------------------------
        public void NoteDeleteButtonClick()
        {
            _noteContentViewModel = NoteContentViewModel.NoteViewModel;
            _noteContentViewModel.DeleteNote(_displayNote.noteId);

            Popup p = this.Parent as Popup;

            if (p != null) { p.IsOpen = false; }
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

        private bool _contentOfNoteIsTapped = true;

        public bool ContentOfNoteIsTapped
        {
            get { return _contentOfNoteIsTapped;  }
        }


        private bool _contentOfNoteIsReadOnly = true;

        public bool ContentOfNoteIsReadOnly
        {
            get { return _contentOfNoteIsReadOnly; }
            set { _contentOfNoteIsReadOnly = value;
                OnPropertyChanged();
            }
        }






        private string _OldContent;

        public string OldContent
        {
            get { return _OldContent; }
            set { _OldContent = value; }
        }

      
        public void ContentOfNoteTapped(object sender, TappedRoutedEventArgs e)
        {
            ContentOfNoteIsReadOnly = false;
            OldContent = ContentOfNoteText;
            ContentTimer = new DispatcherTimer();
            DispatcherTimerStart(ContentTimer);
        }

        private void UsersToShareView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

      
    }
}
