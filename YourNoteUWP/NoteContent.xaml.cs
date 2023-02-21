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
        private DispatcherTimer _dispatcherTimer = null;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        ObservableCollection<YourNoteUWP.Models.User> usersToShare = null;
        private long _noteId;
        private long _searchCount;
        private bool gotCount = false;
        private NoteContentViewModel _noteContentViewModel;

        private bool _gotCount;

        public bool  GotCount
        {
            get { return _gotCount; }
            set { _gotCount = value;
                UpdateCount();
            }
        }

        private void UpdateCount()
        {
                _noteContentViewModel = NoteContentViewModel.NoteViewModel;
                _noteContentViewModel.UpdateCount(_searchCount, _noteId);
        }
        public NoteContent()
        {
            this.InitializeComponent();


            TitleOfNote.AddHandler(TappedEvent, new TappedEventHandler(TitleOfNoteTapped), TitleOfNoteIsTapped);
            ContentOfNote.AddHandler(TappedEvent, new TappedEventHandler(ContentOfNoteTapped), ContentOfNoteIsTapped);

        }

        public void DisplayContent(long noteId, string title, string content, long noteColor)
        {
            _noteId = noteId;
            TitleOfNoteText = title;
            ContentOfNoteText = content;
            NoteContentBackground = GetSolidColorBrush(noteColor);
        }

        public void DisplayContent(long noteId, string title, string content, long searchCount, long noteColor)
        {
            _noteId = noteId;
            TitleOfNoteText = title;
            ContentOfNoteText = content;
            _searchCount = searchCount;
            gotCount = true;
            NoteContentBackground = GetSolidColorBrush(noteColor);
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




        private static SolidColorBrush GetSolidColorBrush(long value)
        {
            int index = (int)value;
            List<string> color = new List<string>()
        { "#f8bec5", "#c6e8b7", "#fdefad", "#c3e9fd"};
            string hex = color[index];
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb((byte)255, r, g, b));
            return myBrush;
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
            set
            {
                _titleOfNoteIsTapped = value;
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

        private string _oldTitle = "";




        //----------------------------Note Close Button ---------------------------------------------------
        public void NoteCloseButtonClick(object sender, RoutedEventArgs e)
        {
            if(_dispatcherTimer != null)
            {
                DispatcherTimer_Tick(sender, e);
                DispatcherTimerStop(_dispatcherTimer);

            }

            Popup p = this.Parent as Popup;
            NoteMenuOptionsContainerVisibility = Visibility.Collapsed;
            if (p != null) { p.IsOpen = false; }
            AccountPageViewModel _accountPageViewModel = new AccountPageViewModel();
            //_accountPageViewModel.
        }

        //--------------------------- NoteMenuOptions List Box ------------------------------------------
        private Visibility _noteMenuOptionsContainerVisibility = Visibility.Collapsed;

        public Visibility NoteMenuOptionsContainerVisibility
        {
            get { return _noteMenuOptionsContainerVisibility; }
            set
            {
                _noteMenuOptionsContainerVisibility = value;
                OnPropertyChanged();
            }
        }


        //----------------------------Note Share Button ---------------------------------------------------
        public void NoteShareButtonClick()
        {

        }

        //----------------------------Note Delete Button ---------------------------------------------------
        public void NoteDeleteButtonClick()
        {
            _noteContentViewModel = NoteContentViewModel.NoteViewModel;
            _noteContentViewModel.DeleteNote(_noteId);
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
            get { return _contentOfNoteIsTapped; }
            set
            {
                _contentOfNoteIsTapped = value;
                OnPropertyChanged();
            }
        }


        private bool _contentOfNoteIsReadOnly = true;

        public bool ContentOfNoteIsReadOnly
        {
            get { return _contentOfNoteIsReadOnly; }
            set
            {
                _contentOfNoteIsReadOnly = value;
                OnPropertyChanged();
            }
        }

        private string _oldContent = "";




        private void UsersToShareView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }


        //----------------------------------------Auto Save----------------------------------------
        public void TitleOfNoteTapped(object sender, TappedRoutedEventArgs e)
        {
           if(TitleOfNoteIsTapped)
            EditModeEnabled();
        }

        public void ContentOfNoteTapped(object sender, TappedRoutedEventArgs e)
        {
           if(ContentOfNoteIsTapped)
            EditModeEnabled();
        }

        private void EditModeEnabled()
        {
            if (NoteMenuOptionsContainerVisibility != Visibility.Visible)
                NoteMenuOptionsContainerVisibility = Visibility.Visible;
            if (TitleOfNoteIsReadOnly == true || ContentOfNoteIsReadOnly == true)
            {
                TitleOfNoteIsReadOnly = false;
                ContentOfNoteIsReadOnly = false;

                TitleOfNoteIsTapped = false;
                ContentOfNoteIsTapped = false;

                _oldContent = ContentOfNoteText;
                _oldTitle = TitleOfNoteText;
                _dispatcherTimer = new DispatcherTimer();
                DispatcherTimerStart(_dispatcherTimer);
            }
        }
        private void DispatcherTimerStart(DispatcherTimer dispatcherTimer)
        {
            if(dispatcherTimer !=null)
            {
                dispatcherTimer.Tick += DispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
                dispatcherTimer.Start();
            }
        }
        private void DispatcherTimerStop(DispatcherTimer dispatcherTimer)
        {
            dispatcherTimer.Tick -= DispatcherTimer_Tick;
            dispatcherTimer.Stop();
        }
        private void DispatcherTimer_Tick(object sender, object e)
        {
            bool contentChange = IsChanged(_oldContent, ContentOfNoteText);
            bool titleChange = IsChanged(_oldTitle, TitleOfNoteText);
            if (contentChange && titleChange)
            {
                _oldContent = ContentOfNoteText;
                _oldTitle = TitleOfNoteText;
                _noteContentViewModel = NoteContentViewModel.NoteViewModel;
                _noteContentViewModel.NoteUpdation(TitleOfNoteText, ContentOfNoteText, _noteId);

            }
            else
            {
                if (contentChange)
                {
                    _oldContent = ContentOfNoteText;
                    _noteContentViewModel = NoteContentViewModel.NoteViewModel;
                    _noteContentViewModel.NoteContentUpdation(ContentOfNoteText, _noteId);
                }
                if (titleChange)
                {
                    _oldTitle = TitleOfNoteText;
                    _noteContentViewModel = NoteContentViewModel.NoteViewModel;
                    _noteContentViewModel.NoteContentUpdation(TitleOfNoteText, _noteId);
                }
            }
           
        }

        private bool IsChanged(string oContext, string nContext)
        {
            bool isChange = false;
            if (oContext != nContext)
                isChange = true;
            return isChange;
        }

        private void ContentOfNoteTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox Context = (TextBox)sender;
            ContentOfNoteText = Context.Text;           
        }

        private void TitleOfNoteTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox Context = (TextBox)sender;
            TitleOfNoteText = Context.Text;
        }
    }
}
