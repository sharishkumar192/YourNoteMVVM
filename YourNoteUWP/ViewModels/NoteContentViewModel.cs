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

        private Note _selectedNote; 
        private Note _displayNote;
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


    



        public  void NoteUpdation(Note displayNote)
        {
            DBUpdation.UpdateNote(DBCreation.notesTableName, displayNote);
        }

        public  void DeleteNote(long noteId)
        {
            DBUpdation.DeleteNote(DBCreation.notesTableName, DBCreation.sharedTableName, noteId);
        }











    }
}
