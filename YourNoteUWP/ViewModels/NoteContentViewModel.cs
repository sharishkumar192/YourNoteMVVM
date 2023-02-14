using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using YourNoteUWP.Models;
using System.Runtime.CompilerServices;
using System.Data.SQLite;
using Windows.Storage;

namespace YourNoteUWP.ViewModels
{
    internal class NoteContentViewModel  
    {

        private NoteContentViewModel()
        {

        }

        private static NoteContentViewModel _noteViewModel;

        public static NoteContentViewModel NoteViewModel
        {
            get
            {
                if (_noteViewModel == null)
                {
                    _noteViewModel = new NoteContentViewModel();
                }
                return _noteViewModel;
            }
        }


    


        public void UpdateCount(long searchCount, long noteId)
        {
            DBUpdation.UpdateNoteCount(DBCreation.notesTableName, searchCount, noteId);
        }
        public  void NoteContentUpdation(string content, long noteId)
        {
            DBUpdation.UpdateNoteContent(DBCreation.notesTableName, content, noteId);
        }

        public void NoteUpdation(string title, string content, long noteId)
        {
            DBUpdation.UpdateNote(DBCreation.notesTableName, title, content, noteId);
        }
        public void NoteTitleUpdation(string title, long noteId)
        {
            DBUpdation.UpdateNoteTitle(DBCreation.notesTableName, title, noteId);
        }

        public  void DeleteNote(long noteId)
        {
            DBUpdation.DeleteNote(DBCreation.notesTableName, DBCreation.sharedTableName, noteId);
        }











    }
}
