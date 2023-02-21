using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.AI.MachineLearning;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
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
        private Tuple<ObservableCollection<Note>, ObservableCollection<Note>, ObservableCollection<Note>> _searchNotes;

        private Frame _frame;

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

            Tuple<Frame, Models.User> tuple = (Tuple<Frame, Models.User>)e.Parameter;
            _frame = tuple.Item1;
            LoggedUser = tuple.Item2;
            _accountPageViewModel = new AccountPageViewModel();
            _searchNotes = _accountPageViewModel.GetSearchNotes(LoggedUser);
            if (_searchNotes.Item1 != null)

                NotesDataItemSource = _searchNotes.Item1;
            if (_searchNotes.Item2 != null)
            {
                SuggestionContentItemSource = _searchNotes.Item2;
                SubSearchItemSource = _searchNotes.Item2;

            }
            //  if(PersonalContentIsSelected == true)
            //          MainMenuOptions()


            if (_searchNotes.Item3 != null && _searchNotes.Item3.Count > 0)
            {
                //  RecentlySearchedVisibility = Visibility.Visible;
                //  SuggestionContentVisibility = Visibility.Collapsed;
                RecentlySearchedItemSource = _searchNotes.Item3;
            }
            //_accountPageViewModel = new AccountPageViewModel(tuple);
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

      public static  SolidColorBrush GetSolidColorBrush(long value)
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

        private bool _personalContentIsSelected = true;
        public bool PersonalContentIsSelected
        {
            get { return _personalContentIsSelected; }
            set
            {
                _personalContentIsSelected = value;
                OnPropertyChanged();
                
            }
        }

        private Visibility _newNoteVisibility;

        public Visibility NewNoteVisibility
        {
            get { return _newNoteVisibility; }
            set { _newNoteVisibility = value; }
        }



        private bool _sharedContentIsSelected = false;
        public bool SharedContentIsSelected
        {
            get { return _sharedContentIsSelected; }
            set
            {
                _sharedContentIsSelected = value;
                OnPropertyChanged();
            }
        }




        private bool _noteCreationIsSelected = false;
        public bool NoteCreationIsSelected
        {
            get { return _noteCreationIsSelected; }
            set
            {
                _noteCreationIsSelected = value;
                OnPropertyChanged();
            }
        }

        private int _mainMenuOptionsSelectedIndex = 0 ;

        public int MainMenuOptionsSelectedIndex
        {
            get { return _mainMenuOptionsSelectedIndex  ; }
            set { _mainMenuOptionsSelectedIndex = value;
                OnPropertyChanged();
            }
        }


        public void MainMenuOptionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // var lb = (ListBox)sender;
            // var li = (ListBoxItem)lb.SelectedItem;

            //  string name = li.Name;

            if (PersonalContentIsSelected == true)
            {
                MainMenuOptionsSelectedIndex = 0;

                //  PersonalContentIsSelected = true ;
                SharedContentIsSelected = false;
                NoteCreationIsSelected = false;
      NotesDataItemSource = AccountPageViewModel.GetPersonalNotes(LoggedUser);
          
                SearchTextBoxText = "";
                _selectedNote = new Note("", "", "", 0);
                SearchPopupIsOpen = false;

            }
            else if (SharedContentIsSelected == true)
            {

                MainMenuOptionsSelectedIndex = 1;
                PersonalContentIsSelected = false;
                //   SharedContentIsSelected = true;
                NoteCreationIsSelected = false;

                NotesDataItemSource = AccountPageViewModel.GetSharedNotes(LoggedUser);
                _selectedNote = new Note("", "", "", 0);
                SearchTextBoxText = "";
                SearchPopupIsOpen = false;
            }
            else if (NoteCreationIsSelected == true)
            {
            //    MainMenuOptionsSelectedIndex = 2;
            //    PersonalContentIsSelected = false;
            //    SharedContentIsSelected = false;
            //    //   NoteCreationIsSelected = true;

            //    SearchPopupIsOpen = false;
            //    Random random = new Random();
            //    int r = random.Next(0, 4);
            //    List<string> l = new List<string>()
            //{
            //    "#c6e8b7","#c3e9fd","#f8bec5","#fdefad",
            //};

            //    Note note = new Note(LoggedUser.userId, "Owner : " + LoggedUser.userId, "No Content", 0);
            //   // DBUpdation.InsertNewNote(note);


            //    //selectedNote.noteId = DBFetch.GetNoteId(DBCreation.notesTableName);
            //   NoteDisplayPopUpIsOpen = true;


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
            if (ChangeVar())
            {
                SearchPopupIsOpen = true;
                TextBox contentOfTextBox = (TextBox)sender;
                if (contentOfTextBox.Text.Length > 2)
                {
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



        public void RecentlySearchedSelectionChanged()
        {
            Note selectedNote = RecentlySearchedSelectedItem;
            //    SearchTextBoxText = selectedNote.title;
            SearchPopupIsOpen = true;
            _selectedNote = selectedNote;
            //   selectedNote.searchCount++;

            NoteDisplayPopUpIsOpen = true;

             




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




        private Note _recentlySearchedSelectedItem;
        public Note RecentlySearchedSelectedItem
        {
            get { return _recentlySearchedSelectedItem; }
            set { _recentlySearchedSelectedItem = value; }
        }





        //----------------------------Search -> Suggestion List View ---------------------------------------------------

        public void SuggestionContainerItemClick(object sender, ItemClickEventArgs e)
        {
            Note selectedNote = (Note)e.ClickedItem;
            selectedNote.searchCount++;
            NoteContentPopUp.DisplayContent(selectedNote.noteId, selectedNote.title, selectedNote.content, selectedNote.searchCount, selectedNote.noteColor);

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


        private bool _inFocus = false;

        private void CommonTappedFunctionality()
        {
            //_inFocus = true;
            //TitleOfNewNoteVisibility = Visibility.Visible;
            //NoteStyleButtonVisibility = Visibility.Visible;

        }
        //-----------New Note Container
 

      

      //  private bool inFocus = false;

        private void SaveOrClose(string title, string content)
        {
            if (title!=null && content!=null && title.Length == 0 && content.Length == 0)
                CreationButtonContent = "Close";
            else
                CreationButtonContent = "Save";
                
        }

        private void InFocus()
        {
            _inFocus = true;
            TitleOfNewNoteVisibility = Visibility.Visible;
            NoteStyleButtonVisibility = Visibility.Visible;

        }


       

        //--Title---
        private string _titleOfNewNoteText = "";

        public string TitleOfNewNoteText 
        {
            get { return _titleOfNewNoteText; }
            set { _titleOfNewNoteText = value; }
        }

     


        private Visibility _titleOfNewNoteVisibility = Visibility.Collapsed;

        public Visibility TitleOfNewNoteVisibility
        {
            get { return _titleOfNewNoteVisibility; }
            set { _titleOfNewNoteVisibility = value;
                OnPropertyChanged();
            }
        }
        private void TitleOfNewNoteGotFocus()
        {
            InFocus();
        }

        private void TitleOfNewNoteLostFocus()
        {
            _inFocus = false;
        }

    

        private void TitleOfNewNoteTextChanged(object sender, RoutedEventArgs e)
        {
            RichEditBox box = (RichEditBox)sender;
            string text;
            box.Document.GetText(Windows.UI.Text.TextGetOptions.None, out text);
            Windows.UI.Text.ITextRange range = box.Document.GetRange(0, text.Length - 1);
            TitleOfNewNoteText = range.Text;

            SaveOrClose(TitleOfNewNoteText, ContentOfNewNoteText);
        }


    

        //--Content---
        private string _contentOfNewNoteText = "";

        public string ContentOfNewNoteText
        {
            get { return _contentOfNewNoteText; }
            set { _contentOfNewNoteText = value; }
        }

        private void StringExtract ( RichEditBox box)
        {
           
   
        }
        private void ContentOfNewNoteTextChanged(object sender, RoutedEventArgs e)
        {
            RichEditBox box = (RichEditBox)sender;
            string x  = (string)box.DataContext;
            //box.Document.GetText; 
           /// StringExtract(box);
            Windows.UI.Text.ITextSelection selectedText = box.Document.Selection;
            Windows.UI.Text.RichEditTextDocument txt = (Windows.UI.Text.RichEditTextDocument)box.Document;
        
            Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;

           // tuple(Windows.UI.Text.ITextRange, Windows.UI.Text.ITextRange) x1  = (Windows.UI.Text.ITextRange)box.Document.GetRange(0);
            string text;
           box.Document.GetText(Windows.UI.Text.TextGetOptions.None, out text);
            Windows.UI.Text.ITextRange range = box.Document.GetRange(0, text.Length - 1);
            ContentOfNewNoteText = range.Text;
             SaveOrClose(TitleOfNewNoteText, ContentOfNewNoteText);

        }

        private void ContentOfNewNoteGotFocus()
        {
            InFocus();
        }

        private void ContentOfNewNoteLostFocus()
        {
            _inFocus = false;
        }


        //--Close Button---

        private string _creationButtonContent = "Close";

        public string CreationButtonContent
        {
            get { return _creationButtonContent; }
            set { _creationButtonContent  = value;
                OnPropertyChanged();
            }
        }

        //------------ NoteStyleOptions ------------------------
        private Visibility _noteStyleButtonVisibility = Visibility.Collapsed; 

        public Visibility NoteStyleButtonVisibility
        {
            get { return _noteStyleButtonVisibility; }
            set { _noteStyleButtonVisibility = value;
                OnPropertyChanged();
            }
        }


        //----Note Color Button
        private long _noteColor = 0 ;

        private int _colorOptionsSelectedIndex = 0;

        public int ColorOptionsSelectedIndex
        {
            get { return _colorOptionsSelectedIndex; }
            set { _colorOptionsSelectedIndex = value;
                OnPropertyChanged();
            }
        }


        private void ColorOptionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox box = (ListBox)sender;
            _noteColor = box.SelectedIndex;
            ColorOptionsSelectedIndex = box.SelectedIndex;
         //   ListBoxItem item = (ListBoxItem)box.SelectedItem;
         //   var brush = item.Foreground;
         //   var hexcolor = brush.ToString();
         //   NoteColorForeground = (SolidColorBrush)item.Foreground;

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


        //------------Creation/Close Button-------------------------
        private void CreationButtonClick()
        {
                if(CreationButtonContent == "Save")
                {
                _accountPageViewModel = new AccountPageViewModel();
                Note newNote = new Note(LoggedUser.userId, TitleOfNewNoteText, ContentOfNewNoteText, _noteColor);
                _accountPageViewModel.CreateNewNote(newNote);
                if(NotesDataItemSource ==null)
                {
                    NotesDataItemSource = new ObservableCollection<Note> ();

                }
                NotesDataItemSource.Add(newNote);
                ContentOfNewNoteText = "";
                TitleOfNewNoteText = "";
                ContentOfNewNote.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
                TitleOfNewNote.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
            }

            TitleOfNewNoteVisibility = Visibility.Collapsed;
                NoteStyleButtonVisibility = Visibility.Collapsed;

        }

        //----------------------------Note Grid View---------------------------------------------------

        private ObservableCollection<Note> _notesDataItemSource;
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
            Note selectedNote = (YourNoteUWP.Models.Note)e.ClickedItem;
            NoteContentPopUp.DisplayContent(selectedNote.noteId, selectedNote.title, selectedNote.content, selectedNote.noteColor);
            NoteDisplayPopUpOpened();
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
        private bool _noteDisplayPopUpIsOpen = false;
        public bool NoteDisplayPopUpIsOpen
        {
            get { return _noteDisplayPopUpIsOpen; }
            set
            {
                _noteDisplayPopUpIsOpen = value;
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

        public  void NoteDisplayPopUpOpened()
        {
          //  PopOut.Stop();
            //PopIn.Begin();
            NoteDisplayPopUpIsOpen = true;
        }
        public  void NoteDisplayPopUpClosed()
        {
            //PopOut.Begin();

        //    PopIn.Stop();
            
            NoteDisplayPopUpIsOpen = false;
        }

        private void NoteDisplayPopUp_Opened(object sender, object e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NoteDisplayPopUpOpened();
        }

        private void FontColor_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText1 = TitleOfNewNote.Document.Selection;

            Windows.UI.Text.ITextSelection selectedText2 = ContentOfNewNote.Document.Selection;
            if (selectedText2 != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText2.CharacterFormat;
                charFormatting.ForegroundColor = Windows.UI.Color.FromArgb(0, 255, 0, 0);

                selectedText2.CharacterFormat = charFormatting;
            }
        }

        private void FontDecreaseClick(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText1 = TitleOfNewNote.Document.Selection;

            Windows.UI.Text.ITextSelection selectedText2 = ContentOfNewNote.Document.Selection;
            if (selectedText2 != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting1 = selectedText2.CharacterFormat;
                float size = charFormatting1.Size;
                if (size > 7.5)
                {
                    size = (float)(size - 0.5);
                    charFormatting1.Size = size;
                }

            }
            if (selectedText1 != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting1 = selectedText1.CharacterFormat;
                float size = charFormatting1.Size;
                if (size > 10.5)
                {
                    size = (float)(size - 0.5);
                    charFormatting1.Size = size;

                }
            }
        }

        private void FontIncreaseClick(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText1 = TitleOfNewNote.Document.Selection;

            Windows.UI.Text.ITextSelection selectedText2 = ContentOfNewNote.Document.Selection;
            if (selectedText2 != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting1 = selectedText2.CharacterFormat;
                float size = charFormatting1.Size;
                if (size < 10.5)

                {
                    size = (float)(size + 0.5);
                    charFormatting1.Size = size;

                }
            }
            if (selectedText1 != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting1 = selectedText1.CharacterFormat;
                float size = charFormatting1.Size;
                if (size < 13.5)
                {
                    size = (float)(size + 0.5);
                    charFormatting1.Size = size;
                }

            }
        }



        private void FontBackgroundClick(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText1 = TitleOfNewNote.Document.Selection;

            Windows.UI.Text.ITextSelection selectedText2 = ContentOfNewNote.Document.Selection;
            if (selectedText2 != null)
            {
                
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText2.CharacterFormat;
                if(charFormatting.BackgroundColor.R == 0 && charFormatting.BackgroundColor.G == 0 && charFormatting.BackgroundColor.B == 0)
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

 

        private void StrikethroughClick(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText1 = TitleOfNewNote.Document.Selection;
            Windows.UI.Text.ITextSelection selectedText2 = ContentOfNewNote.Document.Selection;
            if (selectedText2 != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText2.CharacterFormat;
                charFormatting.Strikethrough = Windows.UI.Text.FormatEffect.Toggle;
            }

            if (selectedText1 != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText1.CharacterFormat;
                charFormatting.Strikethrough = Windows.UI.Text.FormatEffect.Toggle;
            }
        }

        private void AllCapsClick(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText1 = TitleOfNewNote.Document.Selection;

            Windows.UI.Text.ITextSelection selectedText2 = ContentOfNewNote.Document.Selection;
            if (selectedText2 != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting1 = selectedText2.CharacterFormat;
                   charFormatting1.AllCaps = Windows.UI.Text.FormatEffect.Toggle;
                charFormatting1.SmallCaps = Windows.UI.Text.FormatEffect.Toggle;
              //  charFormatting1.Text = charFormatting1.LinkType; 

            }
             
        }
        private void SmallCapsClick(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText1 = TitleOfNewNote.Document.Selection;

            Windows.UI.Text.ITextSelection selectedText2 = ContentOfNewNote.Document.Selection;
            if (selectedText2 != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting1 = selectedText2.CharacterFormat;
                charFormatting1.SmallCaps = Windows.UI.Text.FormatEffect.Toggle;
                //  charFormatting1.Text = charFormatting1.LinkType; 
                float size = charFormatting1.Size;
                if (size > 10)
                    size--;
                charFormatting1.Size = size;
            }

        }


      

    

        private void Menu_Opening(object sender, object e)
        {
            CommandBarFlyout myFlyout = sender as CommandBarFlyout;
            if (myFlyout.Target == ContentOfNewNote)
            {
                AppBarButton myButton = new AppBarButton();
                myButton.Command = new StandardUICommand(StandardUICommandKind.Share);

           //     AppBarButton bold = new AppBarButton();
           //     bold.Command = new StandardUICommand();

                myFlyout.PrimaryCommands.Add(myButton);
              //  myFlyout.PrimaryCommands.Remove((ICommandBarElement)Bold);
            }
        }

        private void REBCustom_Loaded(object sender, RoutedEventArgs e)
        {
            //ContentOfNewNote.SelectionFlyout.Opening += Menu_Opening;
            //ContentOfNewNote.ContextFlyout.Opening += Menu_Opening;
        }

        private void REBCustom_Unloaded(object sender, RoutedEventArgs e)
        {
          //  ContentOfNewNote.SelectionFlyout.Opening -= Menu_Opening;
          //  ContentOfNewNote.ContextFlyout.Opening -= Menu_Opening;
        }

   





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
    }
}





