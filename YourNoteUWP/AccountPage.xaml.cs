using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.AI.MachineLearning;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YourNoteUWP.Models;
using YourNoteUWP.ViewModels;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourNoteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountPage : Page, INotifyPropertyChanged
    {
        private AccountPageViewModel _accountPageViewModel;
        private Note _selectedNote = null;
        private Tuple<ObservableCollection<Note>, ObservableCollection<Note>, ObservableCollection<Note>> _searchNotes;

        private Frame _frame;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AccountPage()
        {
            this.InitializeComponent();
            this.SizeChanged += AccountPage_SizeChanged;

        
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Tuple<Frame, Models.User> tuple = (Tuple<Frame, Models.User>)e.Parameter;
            _frame = tuple.Item1;
            LoggedUser = tuple.Item2;
            _searchNotes = AccountPageViewModel.GetSearchNotes(LoggedUser);
            NotesDataItemSource = _searchNotes.Item1;
            SearchBoxContentItemSource = _searchNotes.Item2;
            SubSearchItemSource = _searchNotes.Item2;
          //  if(PersonalContentIsSelected == true)
          //          MainMenuOptions()


            if (_searchNotes != null && _searchNotes.Item3.Count > 0)
            {
                //  RecentSuggestedVisibility = Visibility.Visible;
                //  SearchBoxContentVisibility = Visibility.Collapsed;
                RecentSuggestedItemSource = _searchNotes.Item3;
            }
            //_accountPageViewModel = new AccountPageViewModel(tuple);
        }

        private void AccountPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
             NoteContentPopUpHeight = Window.Current.Bounds.Height;
            NoteContentPopUpWidth = Window.Current.Bounds.Width;
        }


        private Models.User _loggedUser;

        public Models.User LoggedUser
        {
            get { return _loggedUser; }
            set
            {
                _loggedUser = value;
                OnPropertyChanged();
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
            // var lb = (ListBox)sender;
            // var li = (ListBoxItem)lb.SelectedItem;

            //  string name = li.Name;

            if (PersonalContentIsSelected == true)
            {


                //  PersonalContentIsSelected = true ;
                SharedContentIsSelected = false;
                NoteCreationIsSelected = false;




                NotesDataItemSource = AccountPageViewModel.GetPersonalNotes(LoggedUser);

                SearchTextBoxText = "";
                _selectedNote = new Note("", "", "", "");
                SuggestionsPopupIsOpen = false;

            }
            else if (SharedContentIsSelected == true)
            {
                PersonalContentIsSelected = false;
                //   SharedContentIsSelected = true;
                NoteCreationIsSelected = false;

                NotesDataItemSource = AccountPageViewModel.GetSharedNotes(LoggedUser);
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

            NoteContentPopUp.Hello(snote);
            // NoteContentBackground = GetSolidColorBrush(snote.noteColor);


        }




        //----------------------------Sign Out Button---------------------------------------------------
        public void LogoutContentTapped()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }

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


    }
}