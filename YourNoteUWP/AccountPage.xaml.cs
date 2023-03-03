using ColorCode.Common;
using Microsoft.Toolkit.Uwp.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
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
    public sealed partial class AccountPage : Page
    {
        
        private HomePageViewModel _accountPageViewModel;
        



      
        public AccountPage()
        {
            this.InitializeComponent();
            //this.SizeChanged += AccountPage_SizeChanged;
        }








        private void AccountPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            //     double parentWidth = Window.Current.Bounds.Width;
            //     double parentHeight = Window.Current.Bounds.Height;
            //     double popupWidth = NoteDisplayPopUp.ActualWidth;
            //     double popupHeight = NoteDisplayPopUp.ActualHeight;

            //     NoteDisplayPopUp.HorizontalOffset = (parentWidth - popupWidth) / 2;
            //     NoteDisplayPopUp.VerticalOffset = (parentHeight - popupHeight) / 2;


            ////     var windowBounds = Page.WidthProperty

            ////     NoteDisplayPopUp.HorizontalOffset = (windowBounds.Width - NoteDisplayPopUp.ActualWidth) / 2;
            ////     NoteDisplayPopUp.VerticalOffset = (windowBounds.Height - NoteDisplayPopUp.ActualHeight) / 2;

           // NoteContentPopUpHeight = Window.Current.Bounds.Height;
           // NoteContentPopUpWidth = Window.Current.Bounds.Width / 2;
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
//    if (NoteContentPopUp.ActualWidth == 0 && NoteContentPopUp.ActualHeight == 0)
//    {
//        return;
//    }

//    var coordinates = NoteDisplayPopUp.TransformToVisual(Window.Current.Content).TransformPoint(new Point(0, 0));

//    double ActualHorizontalOffset = NoteDisplayPopUp.HorizontalOffset;
//    double ActualVerticalOffset = NoteDisplayPopUp.VerticalOffset;

//    double NewHorizontalOffset = ((Window.Current.Bounds.Width - NoteContentPopUp.ActualWidth) / 2) - coordinates.X;
//    double NewVerticalOffset = ((Window.Current.Bounds.Height - NoteContentPopUp.ActualHeight) / 2) - coordinates.Y;

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