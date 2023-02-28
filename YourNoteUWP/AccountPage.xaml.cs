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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YourNoteUWP.Models;
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



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            DelUserControlMethod delUserControlMethod = new DelUserControlMethod(NoteDisplayPopUpClosed);
            NoteContentPopUp.CallingPageMethod = delUserControlMethod;

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

        public static SolidColorBrush GetSolidColorBrush(long value)
        {
            int index = (int)value;
            List<string> color = new List<string>()
        { "#f8bec5", "#c6e8b7", "#fdefad", "#c3e9fd"};
            string hex = color[index];
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb((byte)255, r, g, b));
            return myBrush;
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

        public ObservableCollection<Note> ConvertRtfToText(ObservableCollection<Note> source)
        {
            ObservableCollection<Note> result = new ObservableCollection<Note>();
            if (source == null)
                return null;
           
            foreach(Note note in source)
            {
                RichEditBox editBox = new RichEditBox();
                string ans = note.title;

                editBox.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, ans);
                  Windows.UI.Text.ITextRange range = editBox.Document.GetRange(0, ans.Length - 1);
                string title = range.Text;
             
                ans = note.content;
                editBox.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, ans);
     range = editBox.Document.GetRange(0, title.Length - 1);
                string content = range.Text;
                Note nNote = new Note(note.noteId, title, content, note.noteColor, note.noteColor, note.modifiedDay);
                result.Add(nNote);  
            }
            return result;
        }


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
          //  Windows.UI.Text.ITextRange range = box.Document.GetRange(0, text.Length - 1);

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
            TitleOfNewNoteText = TextChangedFunction(TitleOfNewNote);
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
            Windows.UI.Text.ITextSelection selectedText = ContentOfNewNote.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                if (charFormatting.BackgroundColor.R == 0 && charFormatting.BackgroundColor.G == 0 && charFormatting.BackgroundColor.B == 0)
                {
                    charFormatting.BackgroundColor = Windows.UI.Color.FromArgb(255, 255, 255, 255);
                    charFormatting.ForegroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);
                }
                else
                {
                    charFormatting.BackgroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);
                    charFormatting.ForegroundColor = Windows.UI.Color.FromArgb(0, 255, 255, 255);
                }

            }
        }

        //----Note Font Increase
        private void FontIncreaseFunc(Windows.UI.Text.ITextSelection selectedText, float limit, float value)
        {
            Windows.UI.Text.ITextCharacterFormat charFormatting;
            if (selectedText != null)
            {
                charFormatting = selectedText.CharacterFormat;
                string text = selectedText.Text;
                float size = charFormatting.Size;
                if (!String.IsNullOrEmpty(text) && size < limit)
                {
                    charFormatting.Size += value;
                }
            }
        }
        private void FontIncreaseClick(object sender, RoutedEventArgs e)
        {
            FontIncreaseFunc(ContentOfNewNote.Document.Selection, (float)10.5, (float)0.5);
        }

        //----Note Font Decrease
        private void FontDecreaseFunc(Windows.UI.Text.ITextSelection selectedText, float limit, float value)
        {
            Windows.UI.Text.ITextCharacterFormat charFormatting;
            if (selectedText != null)
            {
                charFormatting = selectedText.CharacterFormat;
                string text = selectedText.Text;
                float size = charFormatting.Size;
                if (!String.IsNullOrEmpty(text) && size > limit)
                {
                    charFormatting.Size -= value;
                }
            }
        }

        private void FontDecreaseClick(object sender, RoutedEventArgs e)
        {
            FontDecreaseFunc(ContentOfNewNote.Document.Selection, (float)7.5, (float)0.5);
        }

        //----Note Small Caps
        private void SmallCapsClick(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = ContentOfNewNote.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.SmallCaps = Windows.UI.Text.FormatEffect.Toggle;
            }

        }

        //----Note All Caps
        private void AllCapsClick(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = ContentOfNewNote.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.AllCaps = Windows.UI.Text.FormatEffect.Toggle;
            }

        }

        //----Note Strikethrough
        private void StrikethroughClick(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = ContentOfNewNote.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Strikethrough = Windows.UI.Text.FormatEffect.Toggle;
            }
        }

        //----Note Color Button

        private SolidColorBrush _noteColorForeground = GetSolidColorBrush(0);
        public SolidColorBrush NoteColorForeground
        {
            get { return _noteColorForeground; }
            set
            {
                _noteColorForeground = value;
                OnPropertyChanged();
            }
        }


        private long _noteColor = 0;
        private int _colorOptionsSelectedIndex = 0;

        public int ColorOptionsSelectedIndex
        {
            get { return _colorOptionsSelectedIndex; }
            set
            {
                _colorOptionsSelectedIndex = value;
                OnPropertyChanged();
            }
        }


        private void ColorOptionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox box = (ListBox)sender;
            _noteColor = box.SelectedIndex;
            ColorOptionsSelectedIndex = box.SelectedIndex;
            NoteColorForeground = GetSolidColorBrush(box.SelectedIndex);

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

        private void CreationButtonClick()
        {



            if (CreationButtonContent == "Save")
            {
                string creationDay = DateTime.Now.ToString("MMM/dd/yyyy hh:mm:ss.fff tt");
               string x =  DateTime.Now.ToUniversalTime().ToString();
                _accountPageViewModel = new AccountPageViewModel();



                string y = DateTime.UtcNow.ToString();

                Note newNote = new Note(LoggedUser.userId, TitleOfNewNoteText, ContentOfNewNoteText, _noteColor, creationDay, creationDay);
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
                TitleOfNewNote.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
                SubSearchItemSource.Add(newNote);
            }

            TitleOfNewNoteVisibility = Visibility.Collapsed;
            NoteStyleOptionsVisibility = Visibility.Collapsed;
        }

        //----------------------------Note Grid View---------------------------------------------------

        private ObservableCollection<Note> notesForItemSource = null;

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