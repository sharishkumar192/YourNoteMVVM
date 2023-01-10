using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private ObservableCollection<Note> notesFeeder;
        private ObservableCollection<Note> notesForSearch;
        private ObservableCollection<Note> recentNotes;
        private Note selectedNote = null;
        public static Models.User currentUser;
        public Note notesTemplate { get { return this.DataContext as Note; } }

        public AccountPage()
        {
            this.InitializeComponent();
            Navigation.IsPaneOpen = false;
        }
        public AccountPage(Models.User loggedUser)
        {
            this.InitializeComponent();
            Navigation.IsPaneOpen = false;
            loggedUser.name = DBFetch.GetName(DBCreation.userTableName, loggedUser);
            currentUser = new Models.User(loggedUser.emailId, loggedUser.password);
          


            //Grid View ItemSource
            notesFeeder = Note.GetPersonalNotes(currentUser);

            //Auto Suggestion Box Content ItemSources
            notesForSearch = DBFetch.ReadAllNotesOfUser(DBCreation.notesTableName, DBCreation.sharedTableName, currentUser);
            recentNotes = Note.GetRecentNotes();
            if(recentNotes == null )
                recentNotes = new ObservableCollection<Note>();


            PersonalContent.IsHitTestVisible = true
;                PersonalContent.IsHoldingEnabled = true;
        }

        //Change the value of the Selected Note -> To prevent the firing event of the AutoSuggestionBox TextChanged after choosing the options
        bool ChangeVar()
        {
            if (selectedNote != null) {
                selectedNote = null;
                return false;
            }
            return true;
        }
       
       //When an item from the GridView is clicked 
        private void NotesData_ItemClick(object sender, ItemClickEventArgs e)
        {
            Note note = (YourNoteUWP.Models.Note)e.ClickedItem;
            //MFrame.Navigate(Page());
            this.Content = new NoteDisplay(note, currentUser);
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
                    var suitableItems = new ObservableCollection<Note>();
                    var splitText = contentOfTextBox.Text.Split(" ");

                


                    foreach (var eachNote in notesForSearch)
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
                    notesFeeder.Clear();
                    notesFeeder = suitableItems;
                    NotesData.ItemsSource = notesFeeder;



                }
                else
                {
                    
                        notesFeeder.Clear();
                        if (SharedContent.IsSelected == false)
                            notesFeeder = Note.GetPersonalNotes(currentUser);
                        else
                            notesFeeder = Note.GetSharedNotes(currentUser);
                        NotesData.ItemsSource = notesFeeder;
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
            selectedNote = note;
            //            notesFeeder.Clear();
            //            notesFeeder.Add(selectedNote);
            //            NotesData.ItemsSource = notesFeeder;

            DBUpdation.UpdateRecentSearchedCount(DBCreation.recentSearchesTableName, note);
            this.Content = new NoteDisplay(note, currentUser);

        }

        //(Suggestions)
        private void SearchBoxContainerContent_ItemClick(object sender, ItemClickEventArgs e)
        {
            Note note = (Note)e.ClickedItem;
           SearchTextBox.Text = note.title;
            SuggestionsPopup.IsOpen = false;
            selectedNote = note;
//            notesFeeder.Clear();
//            notesFeeder.Add(selectedNote);
//            NotesData.ItemsSource = notesFeeder;
            DBUpdation.UpdateRecentSearchedCount(DBCreation.recentSearchesTableName, note);
            this.Content = new NoteDisplay(note, currentUser);
        }

        private void Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (PersonalContent.IsSelected)
            {

                notesFeeder.Clear();
                notesFeeder = Note.GetPersonalNotes(currentUser);
                NotesData.ItemsSource = notesFeeder;

                SearchTextBox.Text = "";
                selectedNote = new Note("", "", "", "");
                SuggestionsPopup.IsOpen = false;

            }
            else if (SharedContent.IsSelected)
            {
                notesFeeder.Clear();
                notesFeeder = Note.GetSharedNotes(currentUser);
                NotesData.ItemsSource = notesFeeder;
   
                selectedNote = new Note("", "", "", "");
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


                Note note = new Note(currentUser.emailId, "Owner : " + currentUser.emailId, "No Content", l[r]);
                DBUpdation.InsertNewNote(note);

           
                //note.noteId = DBFetch.GetNoteId(DBCreation.notesTableName);


                this.Content = new NoteDisplay(note, currentUser);


            }


        }

        private void LogoutContent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Content = new LogInPage();
        }




    }
}