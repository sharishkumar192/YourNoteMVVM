﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using YourNoteUWP.Models;
using YourNoteUWP.View;
namespace YourNoteUWP.ViewModels
{
    internal class AccountPageViewModel : INotifyPropertyChanged
    {


        private Tuple<ObservableCollection<Note>, ObservableCollection<Note>, ObservableCollection<Note>> _searchNotes;
        private Note _selectedNote = null;
        private DispatcherTimer _dispatcherTimer;
        private Frame _frame;
        public AccountPageViewModel(Tuple<Frame, Models.User> tuple)
        {
            _frame = tuple.Item1;
            LoggedUser = tuple.Item2;
            _searchNotes = Note.GetSearchNotes(LoggedUser);
            NotesDataItemSource = _searchNotes.Item1;
            SearchBoxContentItemSource = _searchNotes.Item2;
            SubSearchItemSource = _searchNotes.Item2;
            PersonalContentIsSelected = true;

            if (_searchNotes != null && _searchNotes.Item3.Count > 0)
            {
                //  RecentSuggestedVisibility = Visibility.Visible;
                //  SearchBoxContentVisibility = Visibility.Collapsed;
                RecentSuggestedItemSource = _searchNotes.Item3;
            }

        }
        public AccountPageViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private User _loggedUser;

        public User LoggedUser
        {
            get { return _loggedUser; }
            set
            {
                _loggedUser = value;
                OnPropertyChanged();
            }

        }

        //----------------------------Main Menu List Box---------------------------------------------------

        private bool _personalContentIsSelected = true;
        public bool PersonalContentIsSelected
        {
            get { return _personalContentIsSelected; }
            set
            {
                _personalContentIsSelected = value;
                OnPropertyChanged();
            }
        }



        private bool _sharedContentIsSelected = false;
        public bool SharedContentIsSelected
        {
            get { return _sharedContentIsSelected; }
            set
            {
                _sharedContentIsSelected = value;
                OnPropertyChanged();
            }
        }




        private bool _noteCreationIsSelected = false;
        public bool NoteCreationIsSelected
        {
            get { return _noteCreationIsSelected; }
            set
            {
                _noteCreationIsSelected = value;
                OnPropertyChanged();
            }
        }

        public void MainMenuOptionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lb = (ListBox)sender;
            var li = (ListBoxItem)lb.SelectedItem;

            string name = li.Name;

            if (PersonalContentIsSelected == true)
            {


                //  PersonalContentIsSelected = true ;
                SharedContentIsSelected = false;
                NoteCreationIsSelected = false;




                NotesDataItemSource = Note.GetPersonalNotes(LoggedUser);

                SearchTextBoxText = "";
                _selectedNote = new Note("", "", "", "");
                SuggestionsPopupIsOpen = false;

            }
            else if (SharedContentIsSelected == true)
            {
                PersonalContentIsSelected = false;
                //   SharedContentIsSelected = true;
                NoteCreationIsSelected = false;

                NotesDataItemSource = Note.GetSharedNotes(LoggedUser);
                _selectedNote = new Note("", "", "", "");
                SearchTextBoxText = "";
                SuggestionsPopupIsOpen = false;
            }
            else if (NoteCreationIsSelected == true)
            {
                PersonalContentIsSelected = false;
                SharedContentIsSelected = false;
                //   NoteCreationIsSelected = true;

                SuggestionsPopupIsOpen = false;
                Random random = new Random();
                int r = random.Next(0, 4);
                List<string> l = new List<string>()
            {
                "#c6e8b7","#c3e9fd","#f8bec5","#fdefad",
            };

                NoteDisplayPopUpIsOpen = true;
                Note note = new Note(LoggedUser.userId, "Owner : " + LoggedUser.userId, "No Content", l[r]);
                DBUpdation.InsertNewNote(note);


                //selectedNote.noteId = DBFetch.GetNoteId(DBCreation.notesTableName);

                TitleOfNoteText = note.title;
                ContentOfNoteText = note.content;
                NoteDisplayPopUpIsOpen = true;


            }
        }


        //----------------------------Search Text Box---------------------------------------------------
        public void SearchBoxLostFocus()
        {
            SuggestionsPopupIsOpen = false;
            SearchBoxContentItemSource = SubSearchItemSource;
        }



         private string _searchTextBoxText;
        public string SearchTextBoxText
        {
            get { return _searchTextBoxText; }
            set
            {
                _searchTextBoxText = value;
               // SearchBoxLostFocus();
                OnPropertyChanged();
            }
        }


        public void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ChangeVar())
            {
                SuggestionsPopupIsOpen = true;
                TextBox contentOfTextBox = (TextBox)sender;
                if (contentOfTextBox.Text.Length > 2)
                {
                    RecentSuggestedVisibility = Visibility.Collapsed;
                    SearchBoxContentVisibility = Visibility.Visible;
                    var suitableItems = new ObservableCollection<Note>();
                    var splitText = contentOfTextBox.Text.Split(" ");
                    foreach (var eachNote in SearchBoxContentItemSource)
                    {
                        var found = splitText.All((key) =>
                        {
                            return eachNote.title.Contains(key);
                        });
                        if (found)
                        {
                            suitableItems.Add(eachNote);
                        }
                    }
                    SearchBoxContentItemSource = suitableItems;
                }

                else
                {
                    RecentSuggestedVisibility = Visibility.Visible;
                    SearchBoxContentVisibility = Visibility.Collapsed;
                    SearchBoxContentItemSource = SubSearchItemSource;

                }

            }
        }


        //----------------------------Search Popup---------------------------------------------------

        private bool _suggestionsPopupIsOpen = false;
        public bool SuggestionsPopupIsOpen
        {
            get { return _suggestionsPopupIsOpen; }
            set
            {
                _suggestionsPopupIsOpen = value;

                OnPropertyChanged();
            }
        }



        //----------------------------Search -> Recently Searched List Box ---------------------------------------------------

        private ObservableCollection<Note> _recentSuggestedItemSource;
        public ObservableCollection<Note> RecentSuggestedItemSource
        {
            get { return _recentSuggestedItemSource; }
            set
            {
                _recentSuggestedItemSource = value;
            }
        }



        public void RecentSuggestedSelectionChanged()
        {
            Note selectedNote = RecentSuggestedSelectedItem;
            //    SearchTextBoxText = selectedNote.title;
            SuggestionsPopupIsOpen = true;
            _selectedNote = selectedNote;
            //   selectedNote.searchCount++;

            NoteDisplayPopUpIsOpen = true;

            //   TitleOfNoteText = selectedNote.title;
            //    ContentOfNoteText = selectedNote.content;   




        }
        
        
        private Visibility _recentSuggestedVisibility = Visibility.Visible;
        public Visibility RecentSuggestedVisibility
        {
            get { return _recentSuggestedVisibility; }
            set
            {
                _recentSuggestedVisibility = value;
                OnPropertyChanged();
            }
        }




        private Note _recentSuggestedSelectedItem;
        public Note RecentSuggestedSelectedItem
        {
            get { return _recentSuggestedSelectedItem; }
            set { _recentSuggestedSelectedItem = value; }
        }
        //----------------------------Search -> Suggestion List View ---------------------------------------------------

        public void SearchBoxContainerItemClick(object sender, ItemClickEventArgs e)
        {
            Note note = (Note)e.ClickedItem;
            SearchTextBoxText = note.title;
            SuggestionsPopupIsOpen = false;
            _selectedNote = note;
            note.searchCount++;

            NoteDisplayPopUpIsOpen = true;
            TitleOfNoteText = note.title;
            ContentOfNoteText = note.content;

        }


        private ObservableCollection<Note> _searchBoxContentItemSource;
        public ObservableCollection<Note> SearchBoxContentItemSource
        {
            get { return _searchBoxContentItemSource; }
            set
            {
                _searchBoxContentItemSource = value;
                OnPropertyChanged();

            }
        }



        //Temp Original Content of SearchBoxContent ItemSource
        private ObservableCollection<Note> _subSearchItemSource;
        public ObservableCollection<Note> SubSearchItemSource
        {
            get { return _subSearchItemSource; }
            set
            {
                _subSearchItemSource = value;
                OnPropertyChanged();

            }
        }



        private Visibility _searchBoxContentVisibility = Visibility.Collapsed;
        public Visibility SearchBoxContentVisibility
        {
            get { return _searchBoxContentVisibility; }
            set
            {
                _searchBoxContentVisibility = value;
                OnPropertyChanged();
            }
        }


        //----------------------------Sign Out Button---------------------------------------------------
        public void LogoutContentTapped()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }

        }

        //----------------------------Note Grid View---------------------------------------------------

        private ObservableCollection<Note> _notesDataItemSource;
        public ObservableCollection<Note> NotesDataItemSource
        {
            get { return _notesDataItemSource; }
            set
            {
                _notesDataItemSource = value;
                OnPropertyChanged();
            }
        }

        public void NotesDataItemClick(object sender, ItemClickEventArgs e)
        {
            Note snote = (YourNoteUWP.Models.Note)e.ClickedItem;
            //MFrame.Navigate(Page());.

            NoteDisplayPopUpIsOpen = true;
            TitleOfNoteText = snote.title;
            ContentOfNoteText = snote.content;

            // NoteContentBackground = GetSolidColorBrush(snote.noteColor);


        }

        //----------------------------Note Display Popup---------------------------------------------------
        private bool _noteDisplayPopUpIsOpen = false;
        public bool NoteDisplayPopUpIsOpen
        {
            get { return _noteDisplayPopUpIsOpen; }
            set
            {
                _noteDisplayPopUpIsOpen = value;
                OnPropertyChanged();
            }
        }



        private double _noteContentPopUpHeight;
        public double NoteContentPopUpHeight
        {
            get { return _noteContentPopUpHeight; }
            set
            {
                _noteContentPopUpHeight = value;
                OnPropertyChanged();
            }
        }



        private double _noteContentPopUpWidth;
        public double NoteContentPopUpWidth
        {
            get { return _noteContentPopUpWidth; }
            set
            {
                _noteContentPopUpWidth = value;
                OnPropertyChanged();
            }
        }




        //----------------------------USER CONTROL---------------------------------------------------



        //----------------------------Title Text Box---------------------------------------------------

        //----------------------------Note Close Button ---------------------------------------------------

        //----------------------------Note Share Button ---------------------------------------------------

        //----------------------------Note Delete Button ---------------------------------------------------


        //----------------------------Content Text Box ---------------------------------------------------





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

     
       bool ChangeVar()
        {
            if (_selectedNote != null)
            {
                _selectedNote = null;
                return false;
            }
            return true;
        }


     












      





        public void AccountPageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            NoteContentPopUpHeight = Window.Current.Bounds.Height;
            NoteContentPopUpWidth = Window.Current.Bounds.Width;
        }



        //-----------------------------Note Content UserControl-----------------------------------------------


        private string _contentOfNoteText;

        public string ContentOfNoteText
        {
            get { return _contentOfNoteText; }
            set { _contentOfNoteText = value; }
        }

        private string _titleOfNoteText;

        public string TitleOfNoteText
        {
            get { return _titleOfNoteText; }
            set { _titleOfNoteText = value; }
        }

        private bool _titleOfNoteIsReadOnly = true;

        public bool TitleOfNoteIsReadOnly
        {
            get { return _titleOfNoteIsReadOnly; }
            set { _titleOfNoteIsReadOnly = value; }
        }

        private bool _contentOfNoteIsReadOnly = true;

        public bool ContentOfNoteIsReadOnly
        {
            get { return _contentOfNoteIsReadOnly; }
            set { _contentOfNoteIsReadOnly = value; }
        }



        private string _OldTitle;

        public string OldTitle
        {
            get { return _OldTitle; }
            set { _OldTitle = value; }
        }


        private string _OldContent;

        public string OldContent
        {
            get { return _OldContent; }
            set { _OldContent = value; }
        }





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

        public void NoteDeleteButtonClick()
        {
            Note.DeleteNote(_selectedNote.noteId);
            NoteDisplayPopUpIsOpen = false;

        }

        public void TitleOfNoteTapped()
        {
            TitleOfNoteIsReadOnly = false;


            OldTitle = TitleOfNoteText;
            DispatcherTimerSetup();

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
            Note.NoteUpdation(_selectedNote);
        }

        public void ContentOfNoteTapped()
        {
            ContentOfNoteIsReadOnly = false;
            OldContent = ContentOfNoteText;
            DispatcherTimerSetup();
        }



        public void NoteCloseButtonClick(object sender, RoutedEventArgs e)
        {
            //_selectedNote.title = TitleOfNoteText;
            //  _selectedNote.content = ContentOfNoteText;
            //    Note.NoteUpdation(_selectedNote);
            Popup p = (Popup)sender;

            // close the Popup
            if (p != null) { p.IsOpen = false; }

            //    this.Content = new AccountPage(noteOwner);
        }

        public void NoteShareButtonClick()
        {

        }


    }


}
