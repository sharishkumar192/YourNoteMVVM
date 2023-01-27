using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YourNoteUWP.Models;
using YourNoteUWP.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace YourNoteUWP
{
    public sealed partial class NoteContent : UserControl
    {
        ObservableCollection<YourNoteUWP.Models.User> usersToShare = null;
        Models.User noteOwner = null;
        Models.Note displayNote = null;
        private string oldTitle;
        private string oldContent;
       
       private AccountPageViewModel _accountPageViewModel;
        public NoteContent()
        {
            this.InitializeComponent();
            titleOfNote.AddHandler(TappedEvent, new TappedEventHandler(titleOfNote_Tapped), true);
            contentOfNote.AddHandler(TappedEvent, new TappedEventHandler(contentOfNote_Tapped), true);

            _accountPageViewModel = new AccountPageViewModel();
        }


        public NoteContent(YourNoteUWP.Models.Note note, Models.User user)
        {
            this.InitializeComponent();
            displayNote = new Note(note);
            noteOwner = new Models.User(user);
            titleOfNote.Text = displayNote.title;
            contentOfNote.Text = displayNote.content;
            usersToShare = Models.User.GetUsersToShare(noteOwner, displayNote.noteId);
            titleOfNote.AddHandler(TappedEvent, new TappedEventHandler(titleOfNote_Tapped), true);
            contentOfNote.AddHandler(TappedEvent, new TappedEventHandler(contentOfNote_Tapped), true);
        }


        private void noteCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Note.NoteUpdation(displayNote);
            this.Content = new AccountPage(noteOwner);
        }

        private void usersToShare_ItemClick(object sender, ItemClickEventArgs e)
        {

            var sharedToUser = (Models.User)e.ClickedItem;
            Note.ShareNote(sharedToUser.userId, displayNote.noteId);
            usersToShare.Remove(sharedToUser);
            NoteShared();


        }


        private async void NoteShared()
        {
            MessageDialog showDialog;

            showDialog = new MessageDialog("Note Has Been Shared!");
            showDialog.Commands.Add(new UICommand("Ok")
            {
                Id = 0
            });
            showDialog.DefaultCommandIndex = 0;
            var result = await showDialog.ShowAsync();
        }

 



      

       

       
    }
}
