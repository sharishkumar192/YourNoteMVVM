using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourNoteUWP.ViewModels
{
    internal interface IPassword  : INullorEmpty
    {
        string CheckLength(string email);
    }
}
