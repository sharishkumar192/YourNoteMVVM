using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace YourNoteUWP.ViewModels
{
    internal interface IMainView
    {
        UIElement Content { get; set; }
    }
}
