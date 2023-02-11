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
