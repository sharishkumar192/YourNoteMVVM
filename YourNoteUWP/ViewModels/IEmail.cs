using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourNoteUWP.ViewModels
{
    internal interface IEmail
    {
        string CheckNullOrEmpty(string email);
        string CheckValidEmail(string email);

    }
}
