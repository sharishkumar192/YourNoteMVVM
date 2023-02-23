using Microsoft.Toolkit.Uwp.UI;
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



        public static ObservableCollection<Note> GetPersonalNotes(User user)
        {
            return DBFetch.GetPersonalNotes(DBCreation.notesTableName, user);

        }

        public static ObservableCollection<Note> GetSharedNotes(Models.User user)
        {
            return DBFetch.GetSharedNotes(DBCreation.notesTableName, DBCreation.sharedTableName, user);

        }

        public static ObservableCollection<Note> GetAllNotes(ObservableCollection<Note> personal, ObservableCollection<Note> shared, Models.User user)
        {
            ObservableCollection<Note> allNotes = new ObservableCollection<Note>();
            if (personal == null)
                personal = GetPersonalNotes(user);
            if (shared == null)
                shared = GetSharedNotes(user);
            if (personal != null)
                foreach (Note notes in personal)
                {
                    allNotes.Add(notes);
                }
            if (shared != null)
                foreach (Note notes in shared)
                {
                    allNotes.Add(notes);
                }
            return allNotes;

        }

        public Tuple<ObservableCollection<Note>, ObservableCollection<Note>> GetSearchNotes(Models.User user)
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

            Tuple< ObservableCollection<Note>, ObservableCollection<Note>> searchNotes = new Tuple< ObservableCollection<Note>, ObservableCollection<Note>>(noteForSearch, recentNotes);
            return searchNotes;
        }

        public void CreateNewNote(Note newNote)
        {
            DBUpdation.InsertNewNote(DBCreation.notesTableName, newNote);
        }




    }


}
