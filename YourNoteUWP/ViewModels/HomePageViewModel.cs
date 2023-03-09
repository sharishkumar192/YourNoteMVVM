﻿using Microsoft.Toolkit.Uwp.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.System;
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
                foreach(Note note in result)
                {
                    sortedNotes.Add(note);
                }
            }

            return sortedNotes; 
        }

        public static ObservableCollection<Note> GetPersonalNotes(string userId, bool isSort)
        {
            HomePageViewModel apvm = new HomePageViewModel();
            var notes = DBFetch.GetPersonalNotes(DBCreation.notesTableName, userId);
            if (isSort == true)
                return apvm.SortByModificationtime(notes);
            return notes;
        }

        public static ObservableCollection<Note> GetSharedNotes(string userId, bool isSort)
        {
            HomePageViewModel apvm = new HomePageViewModel();
            var  notes = DBFetch.GetSharedNotes(DBCreation.notesTableName, DBCreation.sharedTableName, userId);
            if (isSort == true)
               return apvm.SortByModificationtime(notes);
            return notes;
        }



        public static ObservableCollection<Note> GetAllNotes(string userId, bool isSort)
        {
            HomePageViewModel apvm = new HomePageViewModel();
            var allNotes = new ObservableCollection<Note>();
        
                var pnotes = GetPersonalNotes(userId, false);
                var snotes = GetSharedNotes(userId, false);
        
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
            ObservableCollection<Note> recentNotes = null;
            ObservableCollection<Note> personalNotes = GetPersonalNotes(userId, false);
            ObservableCollection<Note> sharedNotes = GetSharedNotes(userId, false);
            if (personalNotes == null)
                    personalNotes = new ObservableCollection<Note>();
            if (sharedNotes != null)
            {
                foreach (Note note in sharedNotes)
                    personalNotes.Add(note);
            }
            personalNotes.OrderByDescending(note => note.searchCount);
            foreach (Note note in personalNotes)
            {
                if (note.searchCount > 0)
                {
                    if(recentNotes == null)
                        recentNotes = new ObservableCollection<Note>();  
                    recentNotes.Add(note);
                }
                    
            }
            return recentNotes;
        }

        public ObservableCollection<Note> GetSuggestedNote(string userId, string title)
        {
           return SortByModificationtime(DBFetch.GetSuggestedNotes(DBCreation.notesTableName, userId, title));
        }
        public long CreateNewNote(Note newNote)
        {
          return DBUpdation.InsertNewNote(DBCreation.notesTableName, newNote);
        }
    }


}
