using System;
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
using Windows.UI.Xaml.Media;
using YourNoteUWP.Models;

namespace YourNoteUWP.ViewModels
{
    internal class AccountPageViewModel : INotifyPropertyChanged
    {
        IMainView _manageView;
         
        private Tuple<ObservableCollection<Note>, ObservableCollection<Note>, ObservableCollection<Note>> _searchNotes;
        private Note _selectedNote = null;
        private DispatcherTimer _dispatcherTimer;

        public AccountPageViewModel(IMainView view)
        {
            _manageView = view;
            _searchNotes = Note.GetSearchNotes(LoggedUser);
            NotesDataItemSource = _searchNotes.Item1;
            SearchBoxContentItemSource = _searchNotes.Item2;
         
            if(_searchNotes.Item3.Count > 0)
            {
                RecentSuggestedVisibility = Visibility.Visible;
                SearchBoxContentVisibility = Visibility.Collapsed;
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
            set { _loggedUser = value;
                OnPropertyChanged();
            }
          
        }

        private bool _personalContentIsSelected = false;

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
            set { _sharedContentIsSelected = value; }
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

        private ObservableCollection<Note> _notesForSearch  ;

        public ObservableCollection<Note> NoteForSearch
        {
            get { return _notesForSearch  ; }
         
        }

        private ObservableCollection<Note> _notesDataItemSource;

        public ObservableCollection<Note> NotesDataItemSource
        {
            get { return _notesDataItemSource; }
            set { _notesDataItemSource = value; }
        }

        //When an item from the GridView is clicked 
        public void NotesDataItemClick(object sender, ItemClickEventArgs e)
        {
            Note note = (YourNoteUWP.Models.Note)e.ClickedItem;
            //MFrame.Navigate(Page());.
            NoteDisplayPopUpIsOpen = true;
            this.Content = new NoteContent(note, currentUser);
        }

        private bool _noteDisplayPopUpIsOpen = false;

        public bool NoteDisplayPopUpIsOpen
        {
            get { return _noteDisplayPopUpIsOpen = false; }
            set { _noteDisplayPopUpIsOpen  = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Note> _recentSuggestedItemSource;

        public ObservableCollection<Note> RecentSuggestedItemSource
        {
            get { return _recentSuggestedItemSource; }
            set { _recentSuggestedItemSource = value;
            }   
        }

        private ObservableCollection<Note> _searchBoxContentItemSource;

        public ObservableCollection<Note> SearchBoxContentItemSource
        {
            get { return _searchBoxContentItemSource; }
            set { _searchBoxContentItemSource = value; }
        }



        public void NavigationSelectionChanged()
        {
            if (PersonalContentIsSelected == true)
            {

                //  _notesFeeder.Clear();
                NotesDataItemSource = Note.GetPersonalNotes(LoggedUser);

                SearchTextBoxText = "";
                _selectedNote = new Note("", "", "", "");
                SuggestionsPopupIsOpen = false;

            }
            else if (SharedContentIsSelected == true)
            {
                NotesDataItemSource = Note.GetSharedNotes(LoggedUser);
                _selectedNote = new Note("", "", "", "");
                SearchTextBoxText = "";
                SuggestionsPopupIsOpen = false;
            }
            else if (NoteCreationIsSelected == true)
            {
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
            }
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


        //The Suggested Options for the AutoSugggetionBox
        public void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ChangeVar())
            {
                SuggestionsPopupIsOpen = true;
                //PopUpContainer.Width = SearchTextBox.Width;

                TextBox contentOfTextBox = (TextBox)sender;


                if (contentOfTextBox.Text.Length > 2)
                {
                    RecentSuggestedVisibility = Visibility.Collapsed;
                    SearchBoxContentVisibility = Visibility.Visible;
                    var suitableItems = new ObservableCollection<Note>();
                    var splitText = contentOfTextBox.Text.Split(" ");




                    foreach (var eachNote in NoteForSearch)
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
                    //  _notesFeeder.Clear();
                    //  _notesFeeder = suitableItems;
                    // NotesData.ItemsSource = _notesFeeder;



                }
                else
                {
  
                    RecentSuggestedVisibility = Visibility.Visible;
                    SearchBoxContentVisibility = Visibility.Collapsed;

                    NotesDataItemSource.Clear();
                    if (SharedContentIsSelected == false)
                        NotesDataItemSource = Note.GetPersonalNotes(LoggedUser);
                    else
                        NotesDataItemSource = Note.GetSharedNotes(LoggedUser);

                }

            }
        }


        public void LogoutContentTapped()
        {
            _manageView.Content = new LogInPage();
        }

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

        private Visibility _recentSuggestedVisibility = Visibility.Collapsed;

        public Visibility RecentSuggestedVisibility
        {
            get { return _recentSuggestedVisibility; }
            set { _recentSuggestedVisibility = value; }
        }


        private Visibility _searchBoxContentVisibility = Visibility.Visible;

        public Visibility SearchBoxContentVisibility
        {
            get { return _searchBoxContentVisibility; }
            set { _searchBoxContentVisibility = value;
                OnPropertyChanged();
            }
        }


        private bool _suggestionsPopupIsOpen = false;

        public bool SuggestionsPopupIsOpen
        {
            get { return _suggestionsPopupIsOpen; }
            set { _suggestionsPopupIsOpen = value; }
        }

        private Note _recentSuggestedSelectedItem;

        public Note RecentSuggestedSelectedItem
        {
            get { return _recentSuggestedSelectedItem; }
            set { _recentSuggestedSelectedItem = value; }
        }




        // Handle currentUser selecting an item, in our case just output the selected item (Recently Titles)
        private string _searchTextBoxText;

        public string SearchTextBoxText
        {
            get { return _searchTextBoxText; }
            set { _searchTextBoxText = value;
                SearchTextBoxLostFocus();
                OnPropertyChanged();
            }
        }

        public void RecentSuggestedSelectionChanged()
        {
            Note selectedNote = RecentSuggestedSelectedItem;
            SearchTextBoxText = selectedNote.title;
            SuggestionsPopupIsOpen = false;
            _selectedNote = selectedNote;
            selectedNote.searchCount++;

            NoteDisplayPopUpIsOpen = true;
            
            TitleOfNoteText = selectedNote.title;
            ContentOfNoteText = selectedNote.content;   

        


        }

        public void SearchTextBoxLostFocus()
        {
            SuggestionsPopupIsOpen = false;

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
            get { return  _contentOfNoteIsReadOnly; }
            set {  _contentOfNoteIsReadOnly = value; }
        }

        public void NoteDeleteButtonClick()
        {
            Note.DeleteNote(displayNote);

            this.Content = new AccountPage(noteOwner);
        }

        public void TitleOfNoteTapped()
        {
            TitleOfNoteIsReadOnly = false;
       

            oldTitle = displayNote.title;
            oldContent = displayNote.content;
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
            Note.NoteUpdation(displayNote);
            DBUpdation.UpdateNote(DBCreation.notesTableName, displayNote);
        }

        public void ContentOfNoteTapped()
        {
            ContentOfNoteIsReadOnly = false;

            //noteSaveButton.Visibility = Visibility.Visible;
            oldTitle = displayNote.title;
            oldContent = displayNote.content;
                DispatcherTimerSetup();
        }
    }


}
