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
        public Note(string userId, long noteId, string title, string content, string noteColor, long searchCount)
        {
            this.userId = userId;
            this.noteId = noteId;
            this.title = title;
            this.content = content;
            this.noteColor = noteColor;
            this.searchCount = searchCount;
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

        public static void NoteUpdation(Note displayNote)
        {
            DBUpdation.UpdateNote(DBCreation.notesTableName, displayNote);
        }

        public static void DeleteNote(long noteId)
        {
            DBUpdation.DeleteNote(DBCreation.notesTableName, DBCreation.sharedTableName, noteId);
        }

        public static void ShareNote(string sharedUserId, long noteId)
        {
           DBUpdation.InsertSharedNote(DBCreation.sharedTableName, sharedUserId, noteId);
        }

      

    }





}
