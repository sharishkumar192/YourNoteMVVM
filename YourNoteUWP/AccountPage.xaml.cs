using ColorCode.Common;
using Microsoft.Toolkit.Uwp.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YourNoteUWP.Models;
using YourNoteUWP.Util;
using YourNoteUWP.ViewModels;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YourNoteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountPage : Page, INotifyPropertyChanged
    {
        private Note _selectedNote = null;
        private AccountPageViewModel _accountPageViewModel;
        private Tuple<ObservableCollection<Note>, ObservableCollection<Note>> _searchNotes;
        private Frame _frame;
        static Note selectedNoteFromDisplay = null;



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AccountPage()
        {
            this.InitializeComponent();
            this.SizeChanged += AccountPage_SizeChanged;
        }

        //protected EventHandlerList listEventDelegates = new EventHandlerList();

        //readonly object fontBackgroundKey = new object();



        //private void OnFontBackground(RoutedEventArgs e)
        //{
        //    EventHandler fontBackgroundDelegate = (EventHandler)listEventDelegates[fontBackgroundKey];
        //    FontBackgroundClick(this, e);
        //}


        private void DelegateIntialize()
        {
            DelUserControlMethod delUserControlMethod = new DelUserControlMethod(NoteDisplayPopUpClosed);
            NoteContentPopUp.CallingPageMethod = delUserControlMethod;


        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            DelegateIntialize();

            NoteEditOptions.NoteDeleteButtonVisibility = NoteEditOptions.NoteShareButtonVisibility = Visibility.Collapsed;
            Tuple<Frame, Models.User> tuple = (Tuple<Frame, Models.User>)e.Parameter;
            _frame = tuple.Item1;
            LoggedUser = tuple.Item2;
            _accountPageViewModel = new AccountPageViewModel();
            _searchNotes = _accountPageViewModel.GetSearchNotes(LoggedUser);
          
            if (_searchNotes.Item1 != null)
            {
                SuggestionContentItemSource = _searchNotes.Item1;
                SubSearchItemSource = _searchNotes.Item1;

            }


            if (_searchNotes.Item2 != null && _searchNotes.Item2.Count > 0)
            {
                RecentlySearchedItemSource = _searchNotes.Item2;
            }
        }

        private void AccountPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            NoteContentPopUpHeight = Window.Current.Bounds.Height;
            NoteContentPopUpWidth = Window.Current.Bounds.Width/2;
        }


        private Models.User _loggedUser;

        public Models.User LoggedUser
        {
            get { return _loggedUser; }
            set
            {
                _loggedUser = value;
                OnPropertyChanged();
            }

        }

        bool ChangeVar()
        {
            if (_selectedNote != null)
            {
                _selectedNote = null;
                return false;
            }
            return true;
        }



 




        //----------------------------Main Menu List Box---------------------------------------------------

        private bool _personalNotesIsSelected = true;
        public bool PersonalNotesIsSelected
        {
            get { return _personalNotesIsSelected; }
            set
            {
                _personalNotesIsSelected = value;
                OnPropertyChanged();

            }
        }





        private bool _sharedNotesIsSelected = false;
        public bool SharedNotesIsSelected
        {
            get { return _sharedNotesIsSelected; }
            set
            {
                _sharedNotesIsSelected = value;
                OnPropertyChanged();
            }
        }




        private bool _allNotesIsSelected = false;
        public bool AllNotesIsSelected
        {
            get { return _allNotesIsSelected; }
            set
            {
                _allNotesIsSelected = value;
                OnPropertyChanged();
            }
        }

        private int _mainMenuOptionsSelectedIndex = 0;

        public int MainMenuOptionsSelectedIndex
        {
            get { return _mainMenuOptionsSelectedIndex; }
            set
            {
                _mainMenuOptionsSelectedIndex = value;
                OnPropertyChanged();
            }
        }


        public void MainMenuOptionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TitleOfNewNoteVisibility = NoteStyleOptionsVisibility = Visibility.Collapsed;
            ListBox box = (ListBox)sender;

            MainMenuOptionsSelectedIndex = box.SelectedIndex;
            

            if (PersonalNotesIsSelected == true)
            {
                _notesDataItemSource = null;
                SharedNotesIsSelected = AllNotesIsSelected =  false;
                TitleText = "My Personal Notes";

                if ( _notesDataItemSource == null)
                    _notesDataItemSource = AccountPageViewModel.GetPersonalNotes(LoggedUser, true);
                

                NotesDataItemSource = _notesDataItemSource;

                SearchTextBoxText = "";
                _selectedNote = new Note("", "", "", 0);
                SearchPopupIsOpen = false;

            }
            else if (SharedNotesIsSelected == true)
            {
                _notesDataItemSource = null;
                TitleText = "My Shared Notes";
                
                PersonalNotesIsSelected = AllNotesIsSelected = false;
               
                if (_notesDataItemSource == null)
                    _notesDataItemSource = AccountPageViewModel.GetSharedNotes(LoggedUser, true);
                
                NotesDataItemSource = _notesDataItemSource;
                
                _selectedNote = new Note("", "", "", 0);
                SearchTextBoxText = "";
                SearchPopupIsOpen = false;
            }
            else if (AllNotesIsSelected == true)
            {
                _notesDataItemSource = null;
                TitleText = "All Notes";
                if (_notesDataItemSource == null)
                    _notesDataItemSource = AccountPageViewModel.GetAllNotes(LoggedUser, true);
                NotesDataItemSource = _notesDataItemSource;
            }
        }

        //----------------------------Search Text Box---------------------------------------------------
        public void SearchBoxContainerLostFocus()
        {
            SearchPopupIsOpen = false;
            SuggestionContentItemSource = SubSearchItemSource;
        }



        private string _searchTextBoxText;
        public string SearchTextBoxText
        {
            get { return _searchTextBoxText; }
            set
            {
                _searchTextBoxText = value;
                // SuggestionLostFocus();
                OnPropertyChanged();
            }
        }


        public void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
            //    throw new Exception();
                if (ChangeVar())
                {
                    SearchPopupIsOpen = true;
                    TextBox contentOfTextBox = (TextBox)sender;
                    if (contentOfTextBox.Text.Length > 2)
                    {
                        SuggestionContentItemSource = SubSearchItemSource;
                        RecentlySearchedVisibility = Visibility.Collapsed;
                        SuggestionContentVisibility = Visibility.Visible;
                        var suitableItems = new ObservableCollection<Note>();
                        var lowerText = contentOfTextBox.Text.ToLower();
                        var splitText = lowerText.Split(" ");

                        foreach (var eachNote in SuggestionContentItemSource)
                        {
                            var found = splitText.All((key) =>
                            {
                                return eachNote.title.ToLower().Contains(key);
                            });
                            if (found)
                            {
                                suitableItems.Add(eachNote);
                            }
                        }
                        SuggestionContentItemSource = suitableItems;
                    }

                    else
                    {
                        RecentlySearchedVisibility = Visibility.Visible;
                        SuggestionContentVisibility = Visibility.Collapsed;
                        SuggestionContentItemSource = SubSearchItemSource;

                    }

                }
            }
            catch(Exception m)
            {
                TextBox contentOfTextBox = (TextBox)sender;
                Logger.WriteLog(m.Message);
            }
       
        }

        //----------------------------Search Popup---------------------------------------------------

        private bool _searchPopupIsOpen = false;
        public bool SearchPopupIsOpen
        {
            get { return _searchPopupIsOpen; }
            set
            {
                _searchPopupIsOpen = value;

                OnPropertyChanged();
            }
        }



        //----------------------------Search -> Recently Searched List Box ---------------------------------------------------

     


        private ObservableCollection<Note> _recentlySearchedItemSource;
        public ObservableCollection<Note> RecentlySearchedItemSource
        {
            get { return _recentlySearchedItemSource; }
            set
            {
                _recentlySearchedItemSource = value;
            }
        }

        public void RecentlySearchedItemClick(object sender, ItemClickEventArgs e)
        {
            selectedNoteFromDisplay = (Note)e.ClickedItem;
            selectedNoteFromDisplay.searchCount++;
            NoteContentPopUp.DisplayContent(LoggedUser.userId, selectedNoteFromDisplay.noteId, selectedNoteFromDisplay.title, selectedNoteFromDisplay.content, selectedNoteFromDisplay.searchCount, selectedNoteFromDisplay.noteColor, selectedNoteFromDisplay.modifiedDay);
            SearchPopupIsOpen = false;
            NoteDisplayPopUpOpened();


        }



        private Visibility _recentlySearchedVisibility = Visibility.Visible;
        public Visibility RecentlySearchedVisibility
        {
            get { return _recentlySearchedVisibility; }
            set
            {
                _recentlySearchedVisibility = value;
                OnPropertyChanged();
            }
        }










        //----------------------------Search -> Suggestion List View ---------------------------------------------------

        public void SuggestionContainerItemClick(object sender, ItemClickEventArgs e)
        {
            selectedNoteFromDisplay = (Note)e.ClickedItem;
            selectedNoteFromDisplay.searchCount++;
            NoteContentPopUp.DisplayContent(LoggedUser.userId, selectedNoteFromDisplay.noteId, selectedNoteFromDisplay.title, selectedNoteFromDisplay.content, selectedNoteFromDisplay.searchCount, selectedNoteFromDisplay.noteColor, selectedNoteFromDisplay.modifiedDay);
            SearchPopupIsOpen = false;
            NoteDisplayPopUpOpened();


        }


        private ObservableCollection<Note> _suggestionContentItemSource;
        public ObservableCollection<Note> SuggestionContentItemSource
        {
            get { return _suggestionContentItemSource; }
            set
            {
                _suggestionContentItemSource = value;
                OnPropertyChanged();

            }
        }



        //Temp Original Content of SuggestionContent ItemSource
        private ObservableCollection<Note> _subSearchItemSource;
        public ObservableCollection<Note> SubSearchItemSource
        {
            get { return _subSearchItemSource; }
            set
            {
                _subSearchItemSource = value;
                OnPropertyChanged();

            }
        }



        private Visibility _suggestionContentVisibility = Visibility.Visible;
        public Visibility SuggestionContentVisibility
        {
            get { return _suggestionContentVisibility; }
            set
            {
                _suggestionContentVisibility = value;
                OnPropertyChanged();
            }
        }


        //--------------------------- Add a New Note------------------------------------------------------------

        private void SaveOrClose(string title, string content)
        {
            if (title != null && content != null && title.Length == 0 && content.Length == 0)
                CreationButtonContent = "Close";
            else
                CreationButtonContent = "Save";

        }

        private string TextChangedFunction(RichEditBox box)
        {
            string text;
            box.Document.GetText(Windows.UI.Text.TextGetOptions.FormatRtf, out text);
            Windows.UI.Text.ITextRange range = box.Document.GetRange(0, text.Length - 1);
            if (range.Text == "\r")
                return "";
            return text;
            //return range.Text;
        }


        //----------Title TextBlock

        private string _titleText;

        public string TitleText
        {
            get { return _titleText; }
            set { _titleText = value;
                OnPropertyChanged();
            }
        }

        //-----------New Note Container

        private Visibility _newNoteVisibility;

        public Visibility NewNoteVisibility
        {
            get { return _newNoteVisibility; }
            set { _newNoteVisibility = value; }
        }

        //--Title---
        private string _titleOfNewNoteText = "";
        public string TitleOfNewNoteText
        {
            get { return _titleOfNewNoteText; }
            set
            {
                _titleOfNewNoteText = value;
                OnPropertyChanged();
            }
        }



        private Visibility _titleOfNewNoteVisibility = Visibility.Collapsed;
        public Visibility TitleOfNewNoteVisibility
        {
            get { return _titleOfNewNoteVisibility; }
            set
            {
                _titleOfNewNoteVisibility = value;
                OnPropertyChanged();
            }
        }



        private void TitleOfNewNoteTextChanged(object sender, RoutedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            TitleOfNewNoteText = box.Text;
            SaveOrClose(TitleOfNewNoteText, ContentOfNewNoteText);
        }



        //--Content---
        private string _contentOfNewNoteText = "";

        public string ContentOfNewNoteText
        {
            get { return _contentOfNewNoteText; }
            set
            {
                _contentOfNewNoteText = value;
                OnPropertyChanged();
            }
        }

        private void ContentOfNewNoteGotFocus()
        {
            TitleOfNewNoteVisibility = NoteStyleOptionsVisibility = Visibility.Visible;

        }


        private void ContentOfNewNoteTextChanged(object sender, RoutedEventArgs e)
        {
            ContentOfNewNoteText = TextChangedFunction(ContentOfNewNote);
            SaveOrClose(TitleOfNewNoteText, ContentOfNewNoteText);

        }

        //---------Note Style Options-------------------------------------------------------------
        private Visibility _noteStyleOptionsVisibility = Visibility.Collapsed;
        public Visibility NoteStyleOptionsVisibility
        {
            get { return _noteStyleOptionsVisibility; }
            set
            {
                _noteStyleOptionsVisibility = value;
                OnPropertyChanged();
            }
        }

        //----Note Font Background
      
        private void FontBackgroundClick(object sender, RoutedEventArgs e)
        {
            NotesUtilities.FontBackgroundClick(ContentOfNewNote, null);
        }

        //----Note Font Increase
        private void FontIncreaseClick(object sender, RoutedEventArgs e)
        {
            NotesUtilities.FontIncreaseClick(ContentOfNewNote, null);
        }

        //----Note Font Decrease
        private void FontDecreaseClick(object sender, RoutedEventArgs e)
        {
            NotesUtilities.FontDecreaseClick(ContentOfNewNote, null);
        }
    

        //----Note Small Caps
        private void SmallCapsClick(object sender, RoutedEventArgs e)
        {
        NotesUtilities.SmallCapsClick(ContentOfNewNote, null);
        }


        //----Note All Caps
        private void AllCapsClick(object sender, RoutedEventArgs e)
        {
        NotesUtilities.AllCapsClick(ContentOfNewNote, null);
    }

        //----Note Strikethrough
        private void StrikethroughClick(object sender, RoutedEventArgs e)
        {
            NotesUtilities.StrikethroughClick(ContentOfNewNote, null);
        }
        //------------Creation/Close Button-------------------------

        private string _creationButtonContent = "Close";
        public string CreationButtonContent
        {
            get { return _creationButtonContent; }
            set
            {
                _creationButtonContent = value;
                OnPropertyChanged();
            }
        }
        private async void NoTitle()
        {
            MessageDialog showDialog;
           
                showDialog = new MessageDialog("To create a note, please give a title!");
            showDialog.Commands.Add(new UICommand("Ok")
            {
                Id = 0
            });
            showDialog.DefaultCommandIndex = 0;
            var result = await showDialog.ShowAsync();
        }

        private void CreationButtonClick()
        {
            if (CreationButtonContent == "Save")
            {


                if (String.IsNullOrEmpty(TitleOfNewNoteText) )
                {
                    NoTitle();
                    return;

                }


                string creationDay = DateTime.Now.ToString("MMM/dd/yyyy hh:mm:ss.fff tt");
                _accountPageViewModel = new AccountPageViewModel();
                Note newNote = new Note(LoggedUser.userId, TitleOfNewNoteText, ContentOfNewNoteText, NoteEditOptions.ColorOptionsSelectedIndex, creationDay, creationDay);
               long noteId =  _accountPageViewModel.CreateNewNote(newNote);
                if  (_notesDataItemSource == null)
                {
                    _notesDataItemSource = new ObservableCollection<Note>();

                }
                newNote.noteId = noteId;
              //  newNote.noteId = _accountPageViewModel.GetNoteId(newNote.noteId, newNote.userId);
                _notesDataItemSource.Insert(0,newNote);
                NotesDataItemSource = _notesDataItemSource;
                ContentOfNewNoteText = "";
                TitleOfNewNoteText = "";
                ContentOfNewNote.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
                SubSearchItemSource.Add(newNote);
            }


      
                TitleOfNewNoteVisibility = Visibility.Collapsed;
                NoteStyleOptionsVisibility = Visibility.Collapsed;
            

        }

        //----------------------------Note Grid View---------------------------------------------------

        private ObservableCollection<Note> _notesDataItemSource = null;
        public ObservableCollection<Note> NotesDataItemSource
        {
            get { return _notesDataItemSource; }
            set
            {
                _notesDataItemSource = value;
                OnPropertyChanged();
            }
        }

        public void NotesDataItemClick(object sender, ItemClickEventArgs e)
        {
            NoteDisplayPopUpOpened();
           selectedNoteFromDisplay = (YourNoteUWP.Models.Note)e.ClickedItem;
            NoteContentPopUp.DisplayContent(LoggedUser.userId, selectedNoteFromDisplay.noteId, selectedNoteFromDisplay.title, selectedNoteFromDisplay.content, selectedNoteFromDisplay.noteColor, selectedNoteFromDisplay.modifiedDay);
           
        }




        //----------------------------Sign Out Button---------------------------------------------------
        public void LogoutContentTapped()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }

        }

        //----------------------------Note Display Popup---------------------------------------------------
        private void IsCloseAutoSave()
        {
            
        }

        private bool _noteDisplayPopUpIsLight = true;

        public bool NoteDisplayPopUpIsLight
        {
            get { return _noteDisplayPopUpIsLight; }
            set { _noteDisplayPopUpIsLight = value;
                OnPropertyChanged();
            }
        }


        private bool _noteDisplayPopUpIsOpen = false;
        public bool NoteDisplayPopUpIsOpen
        {
            get { return _noteDisplayPopUpIsOpen; }
            set
            {
                _noteDisplayPopUpIsOpen = value;
                if(_noteDisplayPopUpIsOpen == false)
                {
                    IsCloseAutoSave();
                }
                OnPropertyChanged();
            }
        }



        private double _noteContentPopUpHeight;
        public double NoteContentPopUpHeight
        {
            get { return _noteContentPopUpHeight; }
            set
            {
                _noteContentPopUpHeight = value;
                OnPropertyChanged();
            }
        }



        private double _noteContentPopUpWidth;
        public double NoteContentPopUpWidth
        {
            get { return _noteContentPopUpWidth; }
            set
            {
                _noteContentPopUpWidth = value;
                OnPropertyChanged();
            }
        }

        public void NoteDisplayPopUpOpened()
        {
            //  PopOut.Stop();
            //PopIn.Begin();
            NoteDisplayPopUpIsOpen = true;
        }

        private delegate void DelUserControlMethod(object sender, object e);

        private void NoteDisplayPopUpClosed(object sender, object e)
        {

            NoteContentPopUp.UsersToShare = null; 
            if (NoteContentPopUp._dispatcherTimer != null)
            {
                    if(NoteContentPopUp.isDeleted)
                {
                    NoteContentPopUp.DispatcherTimerStop(NoteContentPopUp._dispatcherTimer);
                }
                    else
                {
                    NoteContentPopUp.DispatcherTimer_Tick(sender, e);
                    NoteContentPopUp.DispatcherTimerStop(NoteContentPopUp._dispatcherTimer);

                }




                //   NoteContentPopUp.ContentOfNoteText
            }
            if(NoteContentPopUp.isDeleted)
            {
                int i = NotesDataItemSource.IndexOf(selectedNoteFromDisplay);
                int j = SubSearchItemSource.IndexOf(selectedNoteFromDisplay);
                if(i !=-1 )
                NotesDataItemSource.RemoveAt(i);
                if( j != -1 )
                SubSearchItemSource.RemoveAt(j);
            }
                if (NoteContentPopUp.isDeleted == false && NoteContentPopUp.isModified)
            {

                var found = SubSearchItemSource.FirstOrDefault(x => x.noteId == selectedNoteFromDisplay.noteId);
                int i = NotesDataItemSource.IndexOf(selectedNoteFromDisplay);
                int j = SubSearchItemSource.IndexOf(found);
                Note note = NotesDataItemSource[i];
                NotesDataItemSource.RemoveAt(i);
                NotesDataItemSource.Insert(0, note);
               SubSearchItemSource[j].content = note.content = NoteContentPopUp.ContentOfNoteText;
                SubSearchItemSource[j].title = note.title = NoteContentPopUp.TitleOfNoteText;
                SubSearchItemSource[j].modifiedDay = note.modifiedDay = NoteContentPopUp.currentDay;
            }
            /*Remove(collection.Where(i => i.Id == instance.Id).Single());*/
            NoteDisplayPopUpIsOpen = false;
           
        }

        private void NoteEditOptions_EditOptions(string btnName)
        {
 
            switch(btnName)
            {
                case "FontBackground": FontBackgroundClick(null, null); break;
                case "FontIncrease": FontIncreaseClick(null, null); break;
                case "FontDecrease": FontDecreaseClick(null, null); break;
                case "SmallCaps": SmallCapsClick(null, null); break;
                case "AllCaps": AllCapsClick(null, null); break;
                case "Strikethrough": StrikethroughClick(null, null); break;
                default: return;
            }
                
        }

    








        //private delegate void NoteDisplayPopUpClosed(bool value);
        //private void NoteDisplayPopUpClosed(object sender, EventArgs e)
        //{
        //    NoteDisplayPopUpIsOpen = (bool)e;
        //}

        //   public delegate void NoteDisplayPopUpClosed(object sender, EventArgs e);




    }

}







//private void AlignOptionsSelectionChanged(object sender, SelectionChangedEventArgs e)
//{
//    Windows.UI.Text.ITextSelection selectedText1 = TitleOfNewNote.Document.Selection;

//    Windows.UI.Text.ITextSelection selectedText2 = ContentOfNewNote.Document.Selection;
//    if (selectedText2 != null)
//    {

//        Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText2.CharacterFormat;
//        //  charFormatting. = FontAlignment.
//        //  charFormatting.
//        ListBox box = (ListBox)sender;
//       ListBoxItem item = (ListBoxItem) box.SelectedItem;
//        TextAlignment alignment;
//        switch(item.Name)
//        {
//            case "Left": alignment = TextAlignment.Left; break;

//            case "Center": alignment = TextAlignment.Center; break;

//            case "Right": alignment = TextAlignment.Right; break;
//                default: alignment = TextAlignment.Left; break; 
//        }
//        charFormatting.Size -= 1;

//        //RichTextBox.Alignment = HorizontalAlignment.Right;
//    }

//}



//{
//    if (gdChild.ActualWidth == 0 && gdChild.ActualHeight == 0)
//    {
//        return;
//    }

//    var coordinates = NoteDisplayPopUp.TransformToVisual(Window.Current.Content).TransformPoint(new Point(0, 0));

//    double ActualHorizontalOffset = NoteDisplayPopUp.HorizontalOffset;
//    double ActualVerticalOffset = NoteDisplayPopUp.VerticalOffset;

//    double NewHorizontalOffset = ((Window.Current.Bounds.Width - gdChild.ActualWidth) / 2) - coordinates.X;
//    double NewVerticalOffset = ((Window.Current.Bounds.Height - gdChild.ActualHeight) / 2) - coordinates.Y;

//    if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
//    {
//        this.NoteDisplayPopUp.HorizontalOffset = NewHorizontalOffset;
//        this.NoteDisplayPopUp.VerticalOffset = NewVerticalOffset;
//    }


//private void FontColor_Click(object sender, RoutedEventArgs e)
//{
//    Windows.UI.Text.ITextSelection selectedText1 = TitleOfNewNote.Document.Selection;

//    Windows.UI.Text.ITextSelection selectedText2 = ContentOfNewNote.Document.Selection;
//    if (selectedText2 != null)
//    {
//        Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText2.CharacterFormat;
//        charFormatting.ForegroundColor = Windows.UI.Color.FromArgb(0, 255, 0, 0);

//        selectedText2.CharacterFormat = charFormatting;
//    }
//}