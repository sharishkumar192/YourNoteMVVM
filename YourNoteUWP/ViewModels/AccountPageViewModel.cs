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
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using YourNoteUWP.Models;
using YourNoteUWP.View;
namespace YourNoteUWP.ViewModels
{
    internal class AccountPageViewModel 
    {


       
        private DispatcherTimer _dispatcherTimer;
     
        public AccountPageViewModel(Tuple<Frame, Models.User> tuple)
        {
         

        }
        public static ObservableCollection<Note> GetPersonalNotes(User user)
        {
            return DBFetch.GetPersonalNotes(DBCreation.notesTableName, user);

        }

        public static ObservableCollection<Note> GetSharedNotes(Models.User user)
        {

            return DBFetch.GetSharedNotes(DBCreation.notesTableName, DBCreation.sharedTableName, user);

        }

        public static Tuple<ObservableCollection<Note>, ObservableCollection<Note>, ObservableCollection<Note>> GetSearchNotes(Models.User user)
        {
            ObservableCollection<Note> noteForSearch = null;
            ObservableCollection<Note> recentNotes = null;
            ObservableCollection<Note> personalNotes = GetPersonalNotes(user);
            ObservableCollection<Note> sharedNotes = GetSharedNotes(user);
            if (personalNotes != null)
            {
                foreach (Note note in personalNotes)
                {
                    if (noteForSearch == null)
                        noteForSearch = new ObservableCollection<Note>();
                    if (recentNotes == null)
                        recentNotes = new ObservableCollection<Note>();
                    noteForSearch.Add(note);
                    if (recentNotes.Count < 6 && note.searchCount != 0)
                        recentNotes.Add(note);
                }
            }

            if (sharedNotes != null)
            {
                foreach (Note note in sharedNotes)
                {
                    if (noteForSearch == null)
                        noteForSearch = new ObservableCollection<Note>();
                    if (recentNotes == null)
                        recentNotes = new ObservableCollection<Note>();

                    if (recentNotes.Count < 6 && note.searchCount != 0)
                        recentNotes.Add(note);

                    noteForSearch.Add(note);
                }
            }

            Tuple<ObservableCollection<Note>, ObservableCollection<Note>, ObservableCollection<Note>> searchNotes = new Tuple<ObservableCollection<Note>, ObservableCollection<Note>, ObservableCollection<Note>>(personalNotes, noteForSearch, recentNotes);
            return searchNotes;
        }






























        //----------------------------USER CONTROL---------------------------------------------------



        //----------------------------Title Text Box---------------------------------------------------

        //----------------------------Note Close Button ---------------------------------------------------

        //----------------------------Note Share Button ---------------------------------------------------

        //----------------------------Note Delete Button ---------------------------------------------------


        //----------------------------Content Text Box ---------------------------------------------------

































        ////-----------------------------Note Content UserControl-----------------------------------------------


        //private string _contentOfNoteText;

        //public string ContentOfNoteText
        //{
        //    get { return _contentOfNoteText; }
        //    set { _contentOfNoteText = value; }
        //}

        //private string _titleOfNoteText;

        //public string TitleOfNoteText
        //{
        //    get { return _titleOfNoteText; }
        //    set { _titleOfNoteText = value; }
        //}

        //private bool _titleOfNoteIsReadOnly = true;

        //public bool TitleOfNoteIsReadOnly
        //{
        //    get { return _titleOfNoteIsReadOnly; }
        //    set { _titleOfNoteIsReadOnly = value; }
        //}

        //private bool _contentOfNoteIsReadOnly = true;

        //public bool ContentOfNoteIsReadOnly
        //{
        //    get { return _contentOfNoteIsReadOnly; }
        //    set { _contentOfNoteIsReadOnly = value; }
        //}



        //private string _OldTitle;

        //public string OldTitle
        //{
        //    get { return _OldTitle; }
        //    set { _OldTitle = value; }
        //}


        //private string _OldContent;

        //public string OldContent
        //{
        //    get { return _OldContent; }
        //    set { _OldContent = value; }
        //}





        //private SolidColorBrush _noteContentBackground;

        //public SolidColorBrush NoteContentBackground
        //{
        //    get { return _noteContentBackground; }
        //    set
        //    {
        //        _noteContentBackground = value;
        //        OnPropertyChanged();

        //    }
        //}

        //public void NoteDeleteButtonClick()
        //{
        //   // Note.DeleteNote(_selectedNote.noteId);
        //   // NoteDisplayPopUpIsOpen = false;

        //}

        //public void TitleOfNoteTapped()
        //{
        //    TitleOfNoteIsReadOnly = false;


        //    OldTitle = TitleOfNoteText;
        //    DispatcherTimerSetup();

        //}

        //public void DispatcherTimerSetup()
        //{
        //    _dispatcherTimer = new DispatcherTimer();
        //    _dispatcherTimer.Tick += DispatcherTimer_Tick;
        //    _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
        //    _dispatcherTimer.Start();
        //}

        //private void DispatcherTimer_Tick(object sender, object e)
        //{
        //    _selectedNote.title = TitleOfNoteText;
        //    _selectedNote.content = ContentOfNoteText;
        //    Note.NoteUpdation(_selectedNote);
        //}

        //public void ContentOfNoteTapped()
        //{
        //    ContentOfNoteIsReadOnly = false;
        //    OldContent = ContentOfNoteText;
        //    DispatcherTimerSetup();
        //}



        //public void NoteCloseButtonClick(object sender, RoutedEventArgs e)
        //{
        //    //_selectedNote.title = TitleOfNoteText;
        //    //  _selectedNote.content = ContentOfNoteText;
        //    //    Note.NoteUpdation(_selectedNote);
        //    Popup p = (Popup)sender;

        //    // close the Popup
        //    if (p != null) { p.IsOpen = false; }

        //    //    this.Content = new AccountPage(noteOwner);
        //}

        //public void NoteShareButtonClick()
        //{

        //}


    }


}
