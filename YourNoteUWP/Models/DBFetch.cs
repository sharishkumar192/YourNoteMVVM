using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Numerics;
using Windows.Security.Cryptography.Core;
using Windows.System;
using YourNoteUWP.Models;
namespace YourNoteUWP
{
    public class DBFetch
    {
        private static int loginCount = 2;

        //Get the User's Name
        public static string GetName(string tableName, Models.User owner)
        {
            string name = "";
            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT name FROM {tableName} where userId='{owner.userId}'";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    name = sqlite_datareader.GetString(0);

                }
                sqlite_datareader.Close();

                conn.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return name;
        }

        // Checks if the currentUser's id and password is present in the table or not 
        public static bool CheckUser(string tablename, Models.User credentials)
        {
            bool isExist = false;
            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    if (credentials.userId == sqlite_datareader.GetString(1) && credentials.password == sqlite_datareader.GetString(2))
                    {
                        isExist = true;
                        break;
                    }

                }
                sqlite_datareader.Close();

                conn.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return isExist;
        }

        // It gets no of columns in a table 
        public static long GetColumnNumber(string tablename)
        {
            long noOfColumns = 0;
            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                string columncount = $"SELECT COUNT(*) FROM pragma_table_info('{tablename}');";
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = columncount;
                noOfColumns = (Int64)sqlite_cmd.ExecuteScalar();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return noOfColumns;
        }


        //Get the Note if the Note Id is present in the Shared Note Id -> Denoting that the note is been shared to the user
        public static ObservableCollection<Note> ReadAllNotes(string tableName, Dictionary<long, bool> sharedNoteIds)
        {

            ObservableCollection<Note> notes = new ObservableCollection<Note>();

            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;


                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName} ";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                notes = new ObservableCollection<Models.Note>();
                while (sqlite_datareader.Read())
                {
                    string userId = sqlite_datareader.GetString(0);
                    long noteId = (long)sqlite_datareader.GetValue(1);
                    string title = sqlite_datareader.GetString(2);
                    string content = sqlite_datareader.GetString(3);
                    string noteColor = sqlite_datareader.GetString(4);
                    if (sharedNoteIds.ContainsKey(noteId) == true)
                        notes.Add(new Note(userId, noteId, title, content, noteColor));
                    //  Console.WriteLine("CHECK : " + userId+ " " + ownerId + " " + nId + " " + noteId );
                }
                sqlite_datareader.Close();
                conn.Close();



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return notes;

        }


        //Used in the AutoSuggestionBox where it gets all the data from the both shared and personal notes of that user
        public static ObservableCollection<Note> ReadAllNotesOfUser(string notesTableName, string sharedTableName, Models.User user)
        {
            ObservableCollection<Note> notes = new ObservableCollection<Note>();
            ObservableCollection<Note> pnotes;
            ObservableCollection<Note> snotes;
            pnotes = Note.GetPersonalNotes(user);
            snotes = Note.GetSharedNotes(user);
            foreach (Note n in pnotes)
                notes.Add(n);
            foreach (Note n in snotes)
                notes.Add(n);
            return notes;
        }


        //It reads all the notes details and displays it in the Grid View 
        public static ObservableCollection<Note> ReadAllNotes(string tableName, Models.User user)
        {
            ObservableCollection<Note> notes = null;

            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;


                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName} where userId = '{user.userId}' ";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                notes = new ObservableCollection<Models.Note>();
                while (sqlite_datareader.Read())
                {
                    string userId = sqlite_datareader.GetString(0);
                    long noteId = (long)sqlite_datareader.GetValue(1);
                    string title = sqlite_datareader.GetString(2);
                    string content = sqlite_datareader.GetString(3);
                    string noteColor = sqlite_datareader.GetString(4);
                    notes.Add(new Note(userId, noteId, title, content, noteColor));
        
                }
                sqlite_datareader.Close();
                conn.Close();



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return notes;
        }

        // It reads the content from a particular note using note id 
        public static Note ReadNotesData(string ownerId, string sharedUserId, string noteId, string tablename)
        {
            Note note = new Note(" ", " ", " ", " ");

            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    string userId = sqlite_datareader.GetString(0);
                    string nId = sqlite_datareader.GetString(1);
                    //  Console.WriteLine("CHECK : " + userId+ " " + ownerId + " " + nId + " " + noteId );
                    if (ownerId == userId && nId == noteId)
                    {
                        //  sharedUserId = ownerId + "_" + sharedUserId;
                        note = new Note(sharedUserId, sqlite_datareader.GetString(2), sqlite_datareader.GetString(3), sqlite_datareader.GetString(4));
                        //    Console.WriteLine("INSIDE READING OF DATA and CHANGING THE USER ID "+note.user_Note_Id+ " " + ownerId + " " + sharedUserId + " " + noteId );
                        break;
                    }

                }
                sqlite_datareader.Close();
                conn.Close();



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return note;
        }

        //It prints all the data of the currentUser except the given currentUser 
        public static Dictionary<string, Models.User> ReadAllUserData(string tableName, Models.User owner)
        {

            Dictionary<string, Models.User> users = new Dictionary<string, Models.User>();

            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName} where userId!='{owner.userId}' ";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    long count = (long)sqlite_datareader.GetValue(3);
                    YourNoteUWP.Models.User user = new Models.User(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1), "", count);
                    users[user.userId] = user;
                }
                sqlite_datareader.Close();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return users;
        }

        //It prints all the available users to whom we can share the note 
        public static ObservableCollection<YourNoteUWP.Models.User> ValidUsersToShare(string tableName, Models.User owner, long noteId)
        {

            ObservableCollection<YourNoteUWP.Models.User> users = new ObservableCollection<YourNoteUWP.Models.User>();
            Dictionary<string, bool> notValidUsers = new Dictionary<string, bool>();
            Dictionary<string, Models.User> validUsers;

            try
            {

                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;
                sqlite_cmd = conn.CreateCommand();

                sqlite_cmd.CommandText = $"SELECT sharedUserId FROM {tableName} where sharedNoteId=='{noteId}' ";
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    notValidUsers[sqlite_datareader.GetString(0)] = true;
                }
                sqlite_datareader.Close();
                conn.Close();

                validUsers = ReadAllUserData(DBCreation.userTableName, owner);

                foreach (KeyValuePair<string, Models.User> valid in validUsers)
                {

                    if (!notValidUsers.ContainsKey(valid.Key))
                        users.Add(valid.Value);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return users;
        }


        // It prints all the data of the currentUser 
        public static ObservableCollection<YourNoteUWP.Models.User> ReadAllUserData(string tablename)
        {

            ObservableCollection<YourNoteUWP.Models.User> users = null;
            try

            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename} where loginCount >= '{loginCount}' order by loginCount DESC ";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                users = new ObservableCollection<YourNoteUWP.Models.User>();
                while (sqlite_datareader.Read())
                { long count = (long)sqlite_datareader.GetValue(3);
                    YourNoteUWP.Models.User user = new Models.User(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1), "", count);
                    users.Add(user);
                }
                sqlite_datareader.Close();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return users;
        }


        // It gets all the details of the notes 
        public static bool NoteDetails(string userId, long noteId, string tablename)
        {
            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();

                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";
                sqlite_datareader = sqlite_cmd.ExecuteReader();


                long col = DBFetch.GetColumnNumber(tablename);
                while (sqlite_datareader.Read())
                {
                    string ownerId = sqlite_datareader.GetString(0);
                    long nId = (long)sqlite_datareader.GetValue(1);
                    string title = sqlite_datareader.GetString(2);
                    string content = sqlite_datareader.GetString(3);


                    if (nId == noteId && userId == ownerId)
                    {

                        Console.Write("\t\t\t Note Id : ");
                        Console.WriteLine($"\t {noteId} \t\t\t");
                        Console.Write("\t\t\t  Title  : ");
                        Console.WriteLine($"\t {title}");
                        Console.WriteLine("\t\t\t  Content  : \t\t\t");
                        Console.WriteLine($"\t\t\t {content} \t\t\t");

                        sqlite_datareader.Close();
                        conn.Close();
                        return true;
                    }
                }

                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;

        }

        // It reads the data from the Shared Table currentUser -> shared owner 
        public static void ReadSharedToNotes(string userId, string tablename)
        {
            try
            {
                //  Console.WriteLine("\nSharedToUser Details : \n");
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    string owner = "";
                    string myreader = "";

                    myreader += sqlite_datareader.GetString(1) + " " + sqlite_datareader.GetString(2);
                    owner = sqlite_datareader.GetString(0);

                    if (owner == userId)
                        Console.WriteLine(myreader);
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        //It checks whether a note has been already shared or not to a currentUser
        public static Dictionary<long, bool> ReadAllSharedNotes(string tableName, string userId)
        {
            Dictionary<long, bool> sharedNoteIds = new Dictionary<long, bool>();
            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName} where sharedUserId='{userId}'";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    sharedNoteIds[(long)sqlite_datareader.GetValue(2)] = true;
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return sharedNoteIds; }

        // Check if the Note is present in the Shared Table 
        public static bool CheckNoteShared(string userId, string tableName, long noteId)
        {

            bool isNoteShared = true;
            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM '{tableName}' where sharedUserId='{userId}' and sharedNoteId = '{noteId}' ";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    isNoteShared = false;

                }
                sqlite_datareader.Close();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return isNoteShared;

        }

        //Checks the email Id which the currentUser enters already exists or not 
        public static bool CheckValidEmail(string tableName, string emailId)
        {
            bool check = false;
            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName} where userId=\"{emailId}\" ";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    check = true;
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return check;

        }

        //Getting the NoteId for the Note
        //public static long GetNoteId(string tableName)
        //{
        //    long noteId = 0;
        //    try
        //    {

        //        SQLiteConnection conn = DBCreation.OpenConnection();
        //        conn.Open();
        //        SQLiteCommand sqlite_cmd;
        //        SQLiteDataReader sqlite_datareader;

        //        sqlite_cmd = conn.CreateCommand();
        //        sqlite_cmd.CommandText = $"SELECT COUNT(*) FROM {tableName}";

        //        sqlite_datareader = sqlite_cmd.ExecuteReader();
        //        while (sqlite_datareader.Read())
        //        {
        //            noteId = (long)sqlite_datareader.GetValue(0);


        //        }
        //        sqlite_datareader.Close();
        //        conn.Close();

        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return noteId;
        //}

        //Getting the Notes for the NoteId
        public static ObservableCollection<Note> GetNotes(string tableName, List<long> noteIds)
        {
            ObservableCollection<Note> notes = null;
            SQLiteConnection conn = DBCreation.OpenConnection();
            conn.Open();
            try
            {
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;
                notes = new ObservableCollection<Note>();
                sqlite_cmd = conn.CreateCommand();
                foreach (long id in noteIds)
                {
                    sqlite_cmd.CommandText = $"SELECT * from {tableName} where noteId={id}";
                    sqlite_datareader = sqlite_cmd.ExecuteReader();
                    
                    while (sqlite_datareader.Read())
                    {
                        string userId = sqlite_datareader.GetString(0);
                        long noteId = (long)sqlite_datareader.GetValue(1);
                        string title = sqlite_datareader.GetString(2);
                        string content = sqlite_datareader.GetString(3);
                        string noteColor = sqlite_datareader.GetString(4);
                        notes.Add(new Note(userId, noteId, title, content, noteColor));
                    }
                    sqlite_datareader.Close();
                }


                conn.Close();

            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();

            }

            return notes;
        }

        //Getting the recently searched notes based on the number of times its been used
        public static ObservableCollection<Note> GetRecentlySearchedNotes(string tableName)
        {
            ObservableCollection<Note> recentlySearchedNotes = null;
            List<long> noteIds = null;
            SQLiteConnection conn = DBCreation.OpenConnection();
            conn.Open();
            try
            {

                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName} order by count DESC LIMIT 5";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                noteIds = new List<long>();
                while (sqlite_datareader.Read())
                {
                    long recentsNoteId = sqlite_datareader.GetInt64(0);
                    noteIds.Add(recentsNoteId);
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception)
            { }
            finally
            {
                conn.Close();

            }

            recentlySearchedNotes = DBFetch.GetNotes(DBCreation.notesTableName, noteIds);

            return recentlySearchedNotes;
        }



        //Updating the count of the recently searched notes
        public static bool CheckNoteExists(string tableName, long noteId)
            {
            bool isExist = false;
            SQLiteConnection conn = DBCreation.OpenConnection();
            conn.Open();
            try
            {
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();

                    sqlite_cmd.CommandText = $"SELECT * from {tableName} where noteId={noteId}";
                    sqlite_datareader = sqlite_cmd.ExecuteReader();
                    while (sqlite_datareader.Read())
                    {
                       isExist = true;
                       
                    }
                    sqlite_datareader.Close();
                


                conn.Close();

            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();

            }

            return isExist;
        }


        //Get the color of the note to set in in the grid item background
        public static string GetNoteColor(string tableName, long noteId)
        {
            string color = "";
           SQLiteConnection conn = DBCreation.OpenConnection();
            conn.Open();
            try
            {

                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"SELECT noteColor FROM {tableName} where noteId={noteId}";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    color = sqlite_datareader.GetString(0);
           
                }
                sqlite_datareader.Close();
                conn.Close();

            }
            catch (Exception)
            { }
            finally
            {
                conn.Close();

            }

            return color;

        }

    }
}
