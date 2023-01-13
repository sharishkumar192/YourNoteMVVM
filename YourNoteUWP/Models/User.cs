using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YourNoteUWP.Models
{
   public class User
    {
        public string name;
        public string userId;
        public string password;

        public long loginCount;
        public User(string name, string email, string password, long loginCount)
        {
            this.name = name;
            this.userId = email;
            this.password = password;
            this.loginCount = loginCount;
        }

        public User(User newUser)
        {
            this.name = newUser.name;
            this.userId = newUser.userId;
            this.password = newUser.password;
            this.loginCount = newUser.loginCount;
        }

        public User(string name, string email, string password)
        {
            this.name = name;
            this.userId = email;
            this.password = password;
        }

        public User( string email, string password)
        {
            this.userId = email;
            this.password = password;
        }

        public static bool IsValidEmail(string email)
        {
            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
            return emailRegex.IsMatch(email);
        }
    }
}
