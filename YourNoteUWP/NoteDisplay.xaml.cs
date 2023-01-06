using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YourNoteUWP.Models;
using System.Windows;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourNoteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoteDisplay : Page
    {
        ObservableCollection<YourNoteUWP.Models.User> usersToShare;
        Models.User noteOwner = null;
        Models.Note displayNote = null;
        private string oldTitle;
        private string oldContent;
        DispatcherTimer dispatcherTimer;
        public NoteDisplay()
        {
            this.InitializeComponent();
            titleOfNote.AddHandler(TappedEvent, new TappedEventHandler(titleOfNote_Tapped), true);
            contentOfNote.AddHandler(TappedEvent, new TappedEventHandler(contentOfNote_Tapped), true);

        }

        public NoteDisplay(YourNoteUWP.Models.Note note, Models.User user)
        {
            this.InitializeComponent();
            displayNote = new Note(note);
            noteOwner = new Models.User(user);  
            titleOfNote.Text = displayNote.title;
            contentOfNote.Text = displayNote.content;
            this.noteOwner = new Models.User(noteOwner);
            usersToShare = DBFetch.ValidUsersToShare(DBCreation.sharedTableName, noteOwner, note.noteId);
            titleOfNote.AddHandler(TappedEvent, new TappedEventHandler(titleOfNote_Tapped), true);
            contentOfNote.AddHandler(TappedEvent, new TappedEventHandler(contentOfNote_Tapped), true);
        }

        private void noteCloseButton_Click(object sender, RoutedEventArgs e)
        {
            DBUpdation.UpdateNote(DBCreation.notesTableName, displayNote);
            this.Content = new AccountPage(noteOwner);
        }

        private void usersToShare_ItemClick(object sender, ItemClickEventArgs e)
        {
            
                var sharedToUser = (Models.User)e.ClickedItem;
                bool isNoteShared = DBUpdation.InsertSharedNote(noteOwner.emailId, sharedToUser.emailId, displayNote.noteId);
                usersToShare.Remove(sharedToUser);
                NoteShared(isNoteShared);
          
            
        }

        private void noteShareButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void NoteShared(bool isNoteShared)
        {
            MessageDialog showDialog;
            if (isNoteShared == true)
            {
                showDialog  = new MessageDialog("Note Has Been Shared!");

            }
            else
            {
                showDialog = new MessageDialog("The Note Has Already Been Shared!");
            }
            showDialog.Commands.Add(new UICommand("Ok")
            {
                Id = 0
            });
           showDialog.DefaultCommandIndex = 0;
            var result = await showDialog.ShowAsync();
        }

        //private async void ContentOfNoteChanged(object sender, RoutedEventArgs e)
        //{
        //    MessageDialog showDialog;
        //    showDialog = new MessageDialog("It Seems The Note has been modified!");
        //    showDialog.Commands.Add(new UICommand("Close")
        //    {
        //        Id = 0
        //    });
        //    showDialog.Commands.Add(new UICommand("Save and Close")
        //    {
        //        Id = 1
        //    });
        //    showDialog.DefaultCommandIndex = 0;
        //    var result = await showDialog.ShowAsync();
        //    if ((int)result.Id == 0)
        //    {
        //        displayNote.title = oldTitle;
        //        displayNote.content = oldContent;
        //        noteSaveButton.Visibility = Visibility.Collapsed;
        //        noteCloseButton_Click(sender, e);
        //    }
        //    else
        //    {
        //        noteSaveButton_Click(sender, e);
        //        noteCloseButton_Click(sender, e);
        //    }
        //}

        private void titleOfNote_Tapped(object sender, TappedRoutedEventArgs e)
        {
            titleOfNote.IsReadOnly = false;
            //noteSaveButton.Visibility = Visibility.Visible;
            if (titleOfNote.IsReadOnly == false) 
            {
                titleOfNote.Background = new SolidColorBrush(Windows.UI.Colors.White);
                contentOfNote.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
                
            oldTitle = displayNote.title;
            oldContent = displayNote.content;
            if(titleOfNote.IsReadOnly == false || contentOfNote.IsReadOnly== false)
                DispatcherTimerSetup();

        }

        private void contentOfNote_Tapped(object sender, TappedRoutedEventArgs e)
        {
            contentOfNote.IsReadOnly = false;
            if(contentOfNote.IsReadOnly == false)
            {
                titleOfNote.Background = new SolidColorBrush(Windows.UI.Colors.White);
                contentOfNote.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
            //noteSaveButton.Visibility = Visibility.Visible;
            oldContent = displayNote.content;

            if (titleOfNote.IsReadOnly == false || contentOfNote.IsReadOnly == false)
                DispatcherTimerSetup();
        }



        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            //save the text
            //var text = MyTextBox.Text;


            contentOfNote.IsReadOnly = true;
            titleOfNote.IsReadOnly = true;
            if (titleOfNote.IsReadOnly != false)
                titleOfNote.Background = new SolidColorBrush(Windows.UI.Colors.Black);
            if (contentOfNote.IsReadOnly != false)
                contentOfNote.Background = new SolidColorBrush(Windows.UI.Colors.Black);
            
            displayNote.title = titleOfNote.Text;
            displayNote.content = contentOfNote.Text;

 
            DBUpdation.UpdateNote(DBCreation.notesTableName, displayNote);
        }

        private void noteDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DBUpdation.DeleteNote(DBCreation.notesTableName, DBCreation.sharedTableName, displayNote);
            this.Content = new AccountPage(noteOwner);
        }
    }

}
