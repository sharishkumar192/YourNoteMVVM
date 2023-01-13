using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourNoteUWP.Models
{
    public class Note
    {
        public string userId;
        public long noteId;
        public string title;
        public string content;
        public string noteColor;
        public long searchCount;
        public Note(string userId, long noteId, string title, string content, string noteColor)
        {
            this.userId = userId;   
            this.noteId = noteId;
            this.title = title;
            this.content = content;
            this.noteColor = noteColor;
            
        }

        public Note(Note copyNote)
        {
            this.userId = copyNote.userId;
            this.noteId = copyNote.noteId;
            this.title = copyNote.title;
            this.content = copyNote.content;
            this.noteColor = copyNote.noteColor;
            this.searchCount = copyNote.searchCount;
        }
        public Note(string userId, string title, string content, string noteColor)
        {
            this.userId = userId;
            this.title = title;
            this.content = content;
            this.noteColor = noteColor;
        }

        public static ObservableCollection<Note> GetPersonalNotes(User user)
        {
            return DBFetch.ReadAllNotes(DBCreation.notesTableName, user);

        }

        public static ObservableCollection<Note> GetSharedNotes(Models.User user)
        {
            Dictionary<long, bool> sharedNoteIds = DBFetch.ReadAllSharedNotes(DBCreation.sharedTableName, user.userId);
            return DBFetch.ReadAllNotes(DBCreation.notesTableName, sharedNoteIds);

        }

        public static ObservableCollection<Note> GetRecentNotes()
        {
            return null;
            // ObservableCollection<Note> notes = DBFetch.GetRecentlySearchedNotes(DBCreation.recentSearchesTableName);
           // return notes;

        }

        //public static List<Note> GetSharedNotes()
        //{
        //    var notes = new List<Note>();
        //    notes.Add(new Note("1", "1Vulpate", "Futurum"));
        //    notes.Add(new Note("2", "2Mazim", "Sequiter Que"));
        //    notes.Add(new Note("3", "3Elit", "Tempor"));
        //    notes.Add(new Note("1", "1Vulpate", "Futurum"));
        //    notes.Add(new Note("2", "2Mazim", "Sequiter Que"));
        //    notes.Add(new Note("3", "3Elit", "Tempor"));
        //    notes.Add(new Note("1", "1Vulpate", "Futurum"));
        //    notes.Add(new Note("2", "2Mazim", "Sequiter Que"));
        //    notes.Add(new Note("3", "3Elit", "Tempor"));
        //    notes.Add(new Note("1", "1Vulpate", "Futurum"));
        //    notes.Add(new Note("2", "2Mazim", "Sequiter Que"));
        //    notes.Add(new Note("3", "3Elit", "Tempor"));


        //    return notes;

        //}


    }

   


    
}
