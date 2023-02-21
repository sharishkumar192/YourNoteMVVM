using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YourNoteUWP.Models;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace YourNoteUWP
{
    public sealed partial class NotesDataTemplate : UserControl, INotifyPropertyChanged
    {
        // private static bool loadOnce = false;
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static string NotePreview(string title, string content)
        {
            string text = "";
            if(title != "")
            {
                text = text + title + "\r\n";
              

            }
            if(content != "")
            {
                text += content;
            }
            if(title == "")
                text = "(Empty Note)";
            return text;
        }


        private SolidColorBrush _noteContentBackground ;

        public SolidColorBrush NoteContentBackground
        {
            get { return _noteContentBackground; }
            set { _noteContentBackground = value;
                OnPropertyChanged();
            }
        }


        public void SetColor()
        {
            if (this.DataContext != null)
                NoteContentBackground = GetSolidColorBrush(notesTemplate.noteColor);
        }
        public Note notesTemplate
        {


            get
            {
                // loadOnce = true;
                // SetColor();
                return this.DataContext as Note;



            }
        }


        public NotesDataTemplate()
        {
            this.InitializeComponent();

            //if (notesTemplate != null)
            //{
            //          
            //}




            this.DataContextChanged += NotesDataTemplate_DataContextChanged;
            //if (loadOnce == true)
            //{

            //   
            //}


        }

        private void NotesDataTemplate_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (notesTemplate != null)
                NoteContentBackground = GetSolidColorBrush(notesTemplate.noteColor);
            Bindings.Update();
        }

        private static SolidColorBrush GetSolidColorBrush(long value)
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
    }
}
