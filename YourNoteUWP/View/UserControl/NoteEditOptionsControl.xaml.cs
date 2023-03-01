using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
using YourNoteUWP.Util;
using YourNoteUWP.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace YourNoteUWP
{
    public sealed partial class NoteEditOptionsControl : UserControl, INotifyPropertyChanged
    {
        public event Action<string> EditOptions;

        public NoteEditOptionsControl()
        {
            this.InitializeComponent();
        }

      

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Delegate _delPageMethod;
        public Delegate CallingPageMethod
        {
            set { _delPageMethod = value; }
        }


        private string ButtonName(object sender)
        {
            Button button = (Button)sender;
            return button.Name;
        }

        //----Note Font Background

        private void FontBackgroundClick(object sender, RoutedEventArgs e)
        {

            EditOptions?.Invoke(ButtonName(sender));
        }

        //----Note Font Increase
        private void FontIncreaseClick(object sender, RoutedEventArgs e)
        {
            EditOptions?.Invoke(ButtonName(sender));
        }

        //----Note Font Decrease

        private void FontDecreaseClick(object sender, RoutedEventArgs e)
        {
            EditOptions?.Invoke(ButtonName(sender));
        }

        //----Note Small Caps
        private void SmallCapsClick(object sender, RoutedEventArgs e)
        {
            EditOptions?.Invoke(ButtonName(sender));
        }

        //----Note All Caps
        private void AllCapsClick(object sender, RoutedEventArgs e)
        {
            EditOptions?.Invoke(ButtonName(sender));
        }

        //----Note Strikethrough
        private void StrikethroughClick(object sender, RoutedEventArgs e)
        {
            EditOptions?.Invoke(ButtonName(sender));
        }

        //----Note Color Button

        private SolidColorBrush _noteColorForeground = NotesUtilities.GetSolidColorBrush(0);
        public SolidColorBrush NoteColorForeground
        {
            get { return _noteColorForeground; }
            set
            {
                _noteColorForeground = value;
                OnPropertyChanged();
            }
        }


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

            ColorOptionsSelectedIndex = box.SelectedIndex;
            NoteColorForeground = NotesUtilities.GetSolidColorBrush(box.SelectedIndex);

            EditOptions?.Invoke(box.Name);

        }


        public void NoteDeleteButtonClick(object sender, RoutedEventArgs e)
        { 
            EditOptions?.Invoke(ButtonName(sender));
        }
        public void NoteShareButtonClick(object sender, RoutedEventArgs e)
        {
            if (UsersToShare == null)
            {
                UsersToShare = (ObservableCollection<Models.User>)_delPageMethod.DynamicInvoke(null, null);
            }

            if (UsersToShare != null && UsersToShare.Count == 0)
                NoValidUsers();
        }



        private  void NoValidUsers()
        {
            EditOptions?.Invoke(ButtonName("NoValidUsers"));
        }


        private void UsersToShareView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListView view = (ListView)sender;
            //EditOptions?.Invoke(view.name,e);
        }

        private ObservableCollection<Models.User> _usersToShare;

        public ObservableCollection<Models.User> UsersToShare
        {
            get { return _usersToShare; }
            set
            {
                _usersToShare = value;
                OnPropertyChanged();
            }
        }

        private Visibility _noteShareButtonVisibility;

        public Visibility NoteShareButtonVisibility
        {
            get { return _noteShareButtonVisibility; }
            set { _noteShareButtonVisibility = value; }
        }

        private Visibility _noteDeleteButtonVisibility;

        public Visibility NoteDeleteButtonVisibility
        {
            get { return _noteDeleteButtonVisibility; }
            set { _noteDeleteButtonVisibility = value; }
        }


    }
}
