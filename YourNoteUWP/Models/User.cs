﻿using System;
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
        public User(string name, string userId, string password, long loginCount)
        {
            this.name = name;
            this.userId = userId;
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

        public User(string name, string userId, string password)
        {
            this.name = name;
            this.userId = userId;
            this.password = password;
        }

        public User( string userId, string password)
        {
            this.userId = userId;
            this.password = password;
        }

        public static bool IsValidEmail(string userId)
        {
            Regex userIdRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
            return userIdRegex.IsMatch(userId);
        }
    }
}
