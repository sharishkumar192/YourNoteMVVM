using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
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
        private Note _selectedNote = null;
        private AccountPageViewModel _accountPageViewModel;
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
            SearchPopup.Translation += new Vector3(0, 0, 32);

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Tuple<Frame, Models.User> tuple = (Tuple<Frame, Models.User>)e.Parameter;
            _frame = tuple.Item1;
            LoggedUser = tuple.Item2;
            _accountPageViewModel = new AccountPageViewModel();
            _searchNotes = _accountPageViewModel.GetSearchNotes(LoggedUser);
            NotesDataItemSource = _searchNotes.Item1;
            SuggestionContentItemSource = _searchNotes.Item2;
            SubSearchItemSource = _searchNotes.Item2;
          //  if(PersonalContentIsSelected == true)
          //          MainMenuOptions()


            if (_searchNotes != null && _searchNotes.Item3.Count > 0)
            {
                //  RecentlySearchedVisibility = Visibility.Visible;
                //  SuggestionContentVisibility = Visibility.Collapsed;
                RecentlySearchedItemSource = _searchNotes.Item3;
            }
            //_accountPageViewModel = new AccountPageViewModel(tuple);
        }

        private void AccountPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
             NoteContentPopUpHeight = Window.Current.Bounds.Height;
            NoteContentPopUpWidth = Window.Current.Bounds.Width/2;
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

        private int _mainMenuOptionsSelectedIndex = 0 ;

        public int MainMenuOptionsSelectedIndex
        {
            get { return _mainMenuOptionsSelectedIndex  ; }
            set { _mainMenuOptionsSelectedIndex = value;
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
                MainMenuOptionsSelectedIndex = 0;

                //  PersonalContentIsSelected = true ;
                SharedContentIsSelected = false;
                NoteCreationIsSelected = false;




                NotesDataItemSource = AccountPageViewModel.GetPersonalNotes(LoggedUser);

                SearchTextBoxText = "";
                _selectedNote = new Note("", "", "", "");
                SearchPopupIsOpen = false;

            }
            else if (SharedContentIsSelected == true)
            {
                MainMenuOptionsSelectedIndex = 1;
                PersonalContentIsSelected = false;
                //   SharedContentIsSelected = true;
                NoteCreationIsSelected = false;

                NotesDataItemSource = AccountPageViewModel.GetSharedNotes(LoggedUser);
                _selectedNote = new Note("", "", "", "");
                SearchTextBoxText = "";
                SearchPopupIsOpen = false;
            }
            else if (NoteCreationIsSelected == true)
            {
                MainMenuOptionsSelectedIndex = 2;
                PersonalContentIsSelected = false;
                SharedContentIsSelected = false;
                //   NoteCreationIsSelected = true;

                SearchPopupIsOpen = false;
                Random random = new Random();
                int r = random.Next(0, 4);
                List<string> l = new List<string>()
            {
                "#c6e8b7","#c3e9fd","#f8bec5","#fdefad",
            };

                Note note = new Note(LoggedUser.userId, "Owner : " + LoggedUser.userId, "No Content", l[r]);
                DBUpdation.InsertNewNote(note);


                //selectedNote.noteId = DBFetch.GetNoteId(DBCreation.notesTableName);
               NoteDisplayPopUpIsOpen = true;


            }
        }

        //----------------------------Search Text Box---------------------------------------------------
        public void SearchBoxContainerLostFocus()
        {
            SearchPopupIsOpen = false;
            SuggestionContentItemSource = SubSearchItemSource;
        }



        private string _searchTextBoxText;
        public string SearchTextBoxText
        {
            get { return _searchTextBoxText; }
            set
            {
                _searchTextBoxText = value;
                // SuggestionLostFocus();
                OnPropertyChanged();
            }
        }


        public void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ChangeVar())
            {
                SearchPopupIsOpen = true;
                TextBox contentOfTextBox = (TextBox)sender;
                if (contentOfTextBox.Text.Length > 2)
                {
                     RecentlySearchedVisibility = Visibility.Collapsed;
                    SuggestionContentVisibility = Visibility.Visible;
                    var suitableItems = new ObservableCollection<Note>();
                    var splitText = contentOfTextBox.Text.Split(" ");
                    foreach (var eachNote in SuggestionContentItemSource)
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
                    SuggestionContentItemSource = suitableItems;
                }

                else
                {
                    RecentlySearchedVisibility = Visibility.Visible;
                    SuggestionContentVisibility = Visibility.Collapsed;
                    SuggestionContentItemSource = SubSearchItemSource;

                }

            }
        }

        //----------------------------Search Popup---------------------------------------------------

        private bool _searchPopupIsOpen = false;
        public bool SearchPopupIsOpen
        {
            get { return _searchPopupIsOpen; }
            set
            {
                _searchPopupIsOpen = value;

                OnPropertyChanged();
            }
        }



        //----------------------------Search -> Recently Searched List Box ---------------------------------------------------

        private ObservableCollection<Note> _recentlySearchedItemSource;
        public ObservableCollection<Note> RecentlySearchedItemSource
        {
            get { return _recentlySearchedItemSource; }
            set
            {
                _recentlySearchedItemSource = value;
            }
        }



        public void RecentlySearchedSelectionChanged()
        {
            Note selectedNote = RecentlySearchedSelectedItem;
            //    SearchTextBoxText = selectedNote.title;
            SearchPopupIsOpen = true;
            _selectedNote = selectedNote;
            //   selectedNote.searchCount++;

            NoteDisplayPopUpIsOpen = true;

             




        }


        private Visibility _recentlySearchedVisibility = Visibility.Visible;
        public Visibility RecentlySearchedVisibility
        {
            get { return _recentlySearchedVisibility; }
            set
            {
                _recentlySearchedVisibility = value;
                OnPropertyChanged();
            }
        }




        private Note _recentlySearchedSelectedItem;
        public Note RecentlySearchedSelectedItem
        {
            get { return _recentlySearchedSelectedItem; }
            set { _recentlySearchedSelectedItem = value; }
        }





        //----------------------------Search -> Suggestion List View ---------------------------------------------------

        public void SuggestionContainerItemClick(object sender, ItemClickEventArgs e)
        {
            Note selectedNote = (Note)e.ClickedItem;
            selectedNote.searchCount++;
            NoteContentPopUp.DisplayContent(selectedNote.noteId, selectedNote.title, selectedNote.content, selectedNote.searchCount, selectedNote.noteColor);

            NoteDisplayPopUpOpened();


        }


        private ObservableCollection<Note> _suggestionContentItemSource;
        public ObservableCollection<Note> SuggestionContentItemSource
        {
            get { return _suggestionContentItemSource; }
            set
            {
                _suggestionContentItemSource = value;
                OnPropertyChanged();

            }
        }



        //Temp Original Content of SuggestionContent ItemSource
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



        private Visibility _suggestionContentVisibility = Visibility.Visible;
        public Visibility SuggestionContentVisibility
        {
            get { return _suggestionContentVisibility; }
            set
            {
                _suggestionContentVisibility = value;
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
            Note selectedNote = (YourNoteUWP.Models.Note)e.ClickedItem;
            NoteContentPopUp.DisplayContent(selectedNote.noteId, selectedNote.title, selectedNote.content, selectedNote.noteColor);
            NoteDisplayPopUpOpened();
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

        public  void NoteDisplayPopUpOpened()
        {
          //  PopOut.Stop();
            //PopIn.Begin();
            NoteDisplayPopUpIsOpen = true;
        }
        public  void NoteDisplayPopUpClosed()
        {
            //PopOut.Begin();

        //    PopIn.Stop();
            
            NoteDisplayPopUpIsOpen = false;
        }

        private void NoteDisplayPopUp_Opened(object sender, object e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NoteDisplayPopUpOpened();
        }



        
        //{
        //    if (gdChild.ActualWidth == 0 && gdChild.ActualHeight == 0)
        //    {
        //        return;
        //    }

        //    var coordinates = NoteDisplayPopUp.TransformToVisual(Window.Current.Content).TransformPoint(new Point(0, 0));

        //    double ActualHorizontalOffset = NoteDisplayPopUp.HorizontalOffset;
        //    double ActualVerticalOffset = NoteDisplayPopUp.VerticalOffset;

        //    double NewHorizontalOffset = ((Window.Current.Bounds.Width - gdChild.ActualWidth) / 2) - coordinates.X;
        //    double NewVerticalOffset = ((Window.Current.Bounds.Height - gdChild.ActualHeight) / 2) - coordinates.Y;

        //    if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
        //    {
        //        this.NoteDisplayPopUp.HorizontalOffset = NewHorizontalOffset;
        //        this.NoteDisplayPopUp.VerticalOffset = NewVerticalOffset;
        //    }
    }
    }



