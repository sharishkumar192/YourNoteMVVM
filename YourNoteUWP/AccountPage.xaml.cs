using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourNoteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountPage : Page
    {
        private ObservableCollection<Note> _notesFeeder = null ;
        private ObservableCollection<Note> _noteForSearch = new ObservableCollection<Note>();
        private ObservableCollection<Note> _recentNotes = new ObservableCollection<Note>();
        private Note _selectedNote = null;

        public static Models.User currentUser = null;

        public AccountPage(Models.User loggedUser)
        {
            this.InitializeComponent();
            Navigation.IsPaneOpen = false;
            currentUser = new Models.User(loggedUser);
            Navigation.CompactPaneLength = userImage.Width;


            _notesFeeder = Note.GetPersonalNotes(currentUser);
            NotesData.ItemsSource = _notesFeeder;

            Tuple<ObservableCollection<Note>, ObservableCollection<Note>> searchNotes = Note.GetSearchNotes(currentUser);
            _noteForSearch = searchNotes.Item1; 
            _recentNotes = searchNotes.Item2;



        }

        //Change the value of the Selected Note -> To prevent the firing event of the AutoSuggestionBox TextChanged after choosing the options
        bool ChangeVar()
        {
            if (_selectedNote != null) {
                _selectedNote = null;
                return false;
            }
            return true;
        }
       
       //When an item from the GridView is clicked 
        private void NotesData_ItemClick(object sender, ItemClickEventArgs e)
        {
            Note note = (YourNoteUWP.Models.Note)e.ClickedItem;
            //MFrame.Navigate(Page());.
            NoteDisplayPopUp.IsOpen = true;
            this.Content = new NoteContent(note, currentUser);
        }


        //When an item in the SplitView Panel is clicked

        private void userImage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
          
                Navigation.IsPaneOpen = true;


        }

        private void userImage_PointerExited(object sender, PointerRoutedEventArgs e)
        {

            //SplitViewz.IsPaneOpen = false;
                Navigation.IsPaneOpen = false;

        }


        //The Suggested Options for the AutoSugggetionBox
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ChangeVar())
            {
                SuggestionsPopup.IsOpen = true;
                PopUpContainer.Width = SearchTextBox.Width;
               
                TextBox contentOfTextBox = (TextBox)sender;
             

                if(contentOfTextBox.Text.Length >2)
                {
                    RecentSuggestedContent.Visibility = Visibility.Collapsed;
                    SearchBoxContainerContent.Visibility = Visibility.Visible;  
                    var suitableItems = new ObservableCollection<Note>();
                    var splitText = contentOfTextBox.Text.Split(" ");

                


                    foreach (var eachNote in _noteForSearch)
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


                   SearchBoxContainerContent.ItemsSource = suitableItems;
                  //  _notesFeeder.Clear();
                  //  _notesFeeder = suitableItems;
                   // NotesData.ItemsSource = _notesFeeder;



                }
                else
                {
                    RecentSuggestedContent.Visibility = Visibility.Visible; 
                    SearchBoxContainerContent.Visibility = Visibility.Collapsed;

                    _notesFeeder.Clear();
                        if (SharedContent.IsSelected == false)
                            _notesFeeder = Note.GetPersonalNotes(currentUser);
                        else
                            _notesFeeder = Note.GetSharedNotes(currentUser);
                        NotesData.ItemsSource = _notesFeeder;
                        return;

                }

            }
        }



        // Handle currentUser selecting an item, in our case just output the selected item (Recently Titles)
        private void RecentSuggestedContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Note note = (Note)RecentSuggestedContent.SelectedItem;
            SearchTextBox.Text = note.title;
            SuggestionsPopup.IsOpen = false;
            _selectedNote = note;
            note.searchCount++;
            //            _notesFeeder.Clear();
            //            _notesFeeder.Add(_selectedNote);
            //            NotesData.ItemsSource = _notesFeeder;

            /// DBUpdation.UpdateRecentSearchedCount(DBCreation.recentSearchesTableName, note);
            /// 
            NoteDisplayPopUp.IsOpen = true;
            this.Content = new NoteContent(note, currentUser);

        }

        //(Suggestions)
        private void SearchBoxContainerContent_ItemClick(object sender, ItemClickEventArgs e)
        {
            Note note = (Note)e.ClickedItem;
           SearchTextBox.Text = note.title;
            SuggestionsPopup.IsOpen = false;
            _selectedNote = note;
            note.searchCount++;
            //            _notesFeeder.Clear();
            //            _notesFeeder.Add(_selectedNote);
            //            NotesData.ItemsSource = _notesFeeder;
            //  DBUpdation.UpdateRecentSearchedCount(DBCreation.recentSearchesTableName, note);
            NoteDisplayPopUp.IsOpen = true;
            this.Content = new NoteContent(note, currentUser);
        }

        private void Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (PersonalContent.IsSelected)
            {

              //  _notesFeeder.Clear();
                _notesFeeder = Note.GetPersonalNotes(currentUser);
                NotesData.ItemsSource = _notesFeeder;

                SearchTextBox.Text = "";
                _selectedNote = new Note("", "", "", "");
                SuggestionsPopup.IsOpen = false;

            }
            else if (SharedContent.IsSelected)
            {
             //   _notesFeeder.Clear();
                _notesFeeder = Note.GetSharedNotes(currentUser);
                NotesData.ItemsSource = _notesFeeder;
   
                _selectedNote = new Note("", "", "", "");
                SearchTextBox.Text = "";
                SuggestionsPopup.IsOpen = false;
            }
            else if (NoteCreation.IsSelected)
            {
                SuggestionsPopup.IsOpen = false;
                Random random = new Random();
                int r = random.Next(0, 4);
                List<string> l = new List<string>()
            {
                "#c6e8b7","#c3e9fd","#f8bec5","#fdefad",
            };

                NoteDisplayPopUp.IsOpen = true;
                Note note = new Note(currentUser.userId, "Owner : " + currentUser.userId, "No Content", l[r]);
                DBUpdation.InsertNewNote(note);

           
                //note.noteId = DBFetch.GetNoteId(DBCreation.notesTableName);


                this.Content = new NoteContent(note, currentUser);


            }


        }

        private void LogoutContent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Content = new LogInPage();
        }


        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SuggestionsPopup.IsOpen = false;
        }
    }
}