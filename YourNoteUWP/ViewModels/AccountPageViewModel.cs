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


       
        public AccountPageViewModel()
        {


        }
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

        public Tuple<ObservableCollection<Note>, ObservableCollection<Note>, ObservableCollection<Note>> GetSearchNotes(Models.User user)
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

        public void CreateNewNote(Note newNote)
        {
            DBUpdation.InsertNewNote(DBCreation.notesTableName, newNote);
        }




    }


}
