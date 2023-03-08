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
    internal class HomePageViewModel
    {
       
     
        private  long GetMilliSeconds(string time)
        {
            DateTimeOffset milli = DateTime.Parse(time);
            return milli.ToUnixTimeMilliseconds();
        }
        private  ObservableCollection<Note> SortByModificationtime(ObservableCollection<Note> notes)
        {
            ObservableCollection<Note> sortedNotes = new ObservableCollection<Note>();
            if (notes != null)
            {
                var result = notes.OrderByDescending(a => GetMilliSeconds(a.modifiedDay));
               // return (ObservableCollection<Note>)result;

                foreach(Note note in result)
                {
                    sortedNotes.Add(note);
                }
            }

            return sortedNotes; 
        }

        public static ObservableCollection<Note> GetPersonalNotes(User user, bool isSort)
        {
            HomePageViewModel apvm = new HomePageViewModel();
            var notes = DBFetch.GetPersonalNotes(DBCreation.notesTableName, user);
            if (isSort == true)
                return apvm.SortByModificationtime(notes);
            return notes;
        }

        public static ObservableCollection<Note> GetSharedNotes(Models.User user, bool isSort)
        {
            HomePageViewModel apvm = new HomePageViewModel();
            var  notes = DBFetch.GetSharedNotes(DBCreation.notesTableName, DBCreation.sharedTableName, user);
            if (isSort == true)
               return apvm.SortByModificationtime(notes);
            return notes;
        }



        public static ObservableCollection<Note> GetAllNotes(Models.User user, bool isSort)
        {
            HomePageViewModel apvm = new HomePageViewModel();
            var allNotes = new ObservableCollection<Note>();
        
                var pnotes = GetPersonalNotes(user, false);
                var snotes = GetSharedNotes(user, false);
        
            if (pnotes != null)
                foreach (Note notes in pnotes)
                {
                    allNotes.Add(notes);
                }
            if (snotes!= null)
                foreach (Note notes in snotes)
                {
                    allNotes.Add(notes);
                }

            if (isSort == true)
                return apvm.SortByModificationtime(allNotes);
            return allNotes;

        }
        public ObservableCollection<Note> GetRecentNotes(string userId)
        {
            return DBFetch.GetRecentNotes(DBCreation.notesTableName, userId);
        }

        public ObservableCollection<Note> GetSuggestedNote(string userId, string title)
        {
           return SortByModificationtime(DBFetch.GetSuggestedNotes(DBCreation.notesTableName, userId, title));
        }

        public ObservableCollection<Note> GetSearchNotes(User user)
        {
           
            HomePageViewModel apvm = new HomePageViewModel();
            ObservableCollection<Note> noteForSearch = null;
            ObservableCollection<Note> recentNotes = null;
            ObservableCollection<Note> personalNotes = GetPersonalNotes(user, false);
            ObservableCollection<Note> sharedNotes = GetSharedNotes(user, false);
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
           var x = SortByModificationtime(noteForSearch);
           var y =  SortByModificationtime(recentNotes);
            

            ObservableCollection<Note> searchNotes = y;
            return searchNotes;
        }

        public long CreateNewNote(Note newNote)
        {
          return DBUpdation.InsertNewNote(DBCreation.notesTableName, newNote);
        }

        //public ObservableCollection<Note> SuggestedLists(string text)
        //{
        //    return DBFetch.SuggestList(DBCreation.notesTableName, text);
        //}


   



        //public long GetNoteId(string userId)

    }


}
