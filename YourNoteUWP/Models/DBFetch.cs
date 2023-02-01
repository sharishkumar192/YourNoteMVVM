using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using Windows.Security.Cryptography.Core;
using Windows.System;
using Windows.UI.Xaml.Controls.Primitives;
using YourNoteUWP;
using YourNoteUWP.Models;
namespace YourNoteUWP
{
    public class DBFetch
    {
        private static int _loginCount = 2;




        // ----------------------------------------SIGN UP PAGE DB FETCHES----------------------------------------

        //Checks the email Id which the currentUser enters already exists or not 
        public static bool CheckValidEmail(string userTableName, string userId)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            bool check = false;
            string query = $"SELECT * FROM " + sqlCommandBuilder.QuoteIdentifier(userTableName) + " WHERE userId= @userId ";
            try
            {
                using (SQLiteConnection conn = DBCreation.OpenConnection())
                {
                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    SQLiteParameter parameters = new SQLiteParameter("@userId", userId);
                    command.Parameters.Add(parameters);
                    using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                    {
                        while (sqlite_datareader.Read())
                        {
                            check = true;

                        }

                        sqlite_datareader.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception)
            {
            }

            return check;

        }



        //  ----------------------------------------LOGIN IN PAGE DB FETCHES----------------------------------------


        // It prints all the data of the currentUser 
        public static ObservableCollection<Models.User> FrequentLoggedUsers(string userTableName)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();

            string query = $"SELECT * FROM " + sqlCommandBuilder.QuoteIdentifier(userTableName) + " where loginCount >= @count order by loginCount DESC; ";
            ObservableCollection<Models.User> users = null;
            try
            {
                using (SQLiteConnection conn = DBCreation.OpenConnection())
                {

                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    SQLiteParameter parameters = new SQLiteParameter("@count", _loginCount);
                    command.Parameters.Add(parameters);
                    using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                    {
                        while (sqlite_datareader.Read())
                        {
                            if (users == null)
                                users = new ObservableCollection<Models.User>();
                            Models.User user = new Models.User(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1), sqlite_datareader.GetString(2), (long)sqlite_datareader.GetValue(3));
                            users.Add(user);
                        }

                        sqlite_datareader.Close();
                    }
                    conn.Close();
                }
            }

            catch (Exception)
            {

            }
            return users;
        }



        // Checks if the currentUser's id and password is present in the table or not, updates the login count of the user if the user exists 
        public static Tuple<Models.User, bool> ValidateUser(string tableName, string loggedUserId, string loggedPassword) //Needed
        {

            bool isExist = false;
            Models.User userDetails = null;
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            string query1 = $"SELECT * FROM " + sqlCommandBuilder.QuoteIdentifier(tableName) +
            " where userId = @userId  and password = @password ; ";

            string query2 = $"UPDATE " + sqlCommandBuilder.QuoteIdentifier(tableName) + " SET _loginCount = _loginCount+1  where userId = @userId ; ";

            try
            {
                using (SQLiteConnection conn = DBCreation.OpenConnection())
                {
                    SQLiteCommand command = new SQLiteCommand(query1, conn);
                    SQLiteParameter[] parameters = new SQLiteParameter[2];
                    parameters[0] = new SQLiteParameter("@userId", loggedUserId);
                    parameters[1] = new SQLiteParameter("@password", loggedPassword);

                    command.Parameters.Add(parameters[0]);
                    command.Parameters.Add(parameters[1]);
                    using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                    {
                        while (sqlite_datareader.Read())
                        {
                            string name = sqlite_datareader.GetString(0);
                            string userId = sqlite_datareader.GetString(1);
                            string password = sqlite_datareader.GetString(2);
                            long loginCount = (long)sqlite_datareader.GetValue(3);
                            userDetails = new Models.User(name, userId, password, loginCount);
                            isExist = true;
                        }


                        sqlite_datareader.Close();
                    }

                    command.CommandText = query2;
                    command.Parameters.Remove(parameters[1]);
                    command.ExecuteNonQuery();
                    conn.Close();

                }
            }
            catch (Exception)
            {

            }

            Tuple<Models.User, bool> validate = new Tuple<Models.User, bool>(userDetails, isExist);
            return validate;

        }




        //  ----------------------------------------ACCOUNT PAGE DB FETCHES----------------------------------------  

        //Get Personal Notes

        public static ObservableCollection<Note> GetPersonalNotes(string noteTableName, Models.User loggedUser)// Needed
        {
            ObservableCollection<Note> notes = null;
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();


            string query = $"SELECT * FROM " + sqlCommandBuilder.QuoteIdentifier(noteTableName) + " where userId = @userId ORDER BY searchCount DESC  ";

            try
            {
                using (SQLiteConnection conn = DBCreation.OpenConnection())
                {
                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    SQLiteParameter parameters = new SQLiteParameter("@userId", loggedUser.userId);
                    command.Parameters.Add(parameters);
                    using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                    {
                        while (sqlite_datareader.Read())
                        {
                            if (notes == null)
                                notes = new ObservableCollection<Note>();
                            Note note = new Note("", 0, "", "", "", 0);

                            note.userId = sqlite_datareader.GetString(0);
                            note.noteId = (long)sqlite_datareader.GetValue(1);
                            note.title = sqlite_datareader.GetString(2);
                            note.content = sqlite_datareader.GetString(3);
                            note.noteColor = sqlite_datareader.GetString(4);
                            note.searchCount = (long)sqlite_datareader.GetValue(5);
                            notes.Add(note);

                        }

                        sqlite_datareader.Close();
                    }

                    conn.Close();
                }



            }
            catch (Exception)
            {

            }
            return notes;
        }

        //Get Shared Notes
        public static ObservableCollection<Note> GetSharedNotes(string notesTableName, string sharedTableName, Models.User loggedUser)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            ObservableCollection<Note> sharedNotes = null;
            string query = $"Select * from " + sqlCommandBuilder.QuoteIdentifier(notesTableName) + ", " + sqlCommandBuilder.QuoteIdentifier(sharedTableName) + " where noteId = sharedNoteId and sharedUserId = @userId  ORDER BY searchCount DESC ; ";

            try
            {
                using (SQLiteConnection conn = DBCreation.OpenConnection())
                {
                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    SQLiteParameter parameters = new SQLiteParameter("@userId", loggedUser.userId);
                    command.Parameters.Add(parameters);
                    Note note = new Note("", 0, "", "", "", 0);
                    using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                    {
                        while (sqlite_datareader.Read())
                        {
                            if (sharedNotes == null)
                                sharedNotes = new ObservableCollection<Note>();
                            note.userId = sqlite_datareader.GetString(0);
                            note.noteId = sqlite_datareader.GetInt64(1);
                            note.title = sqlite_datareader.GetString(2);
                            note.content = sqlite_datareader.GetString(3);
                            note.noteColor = sqlite_datareader.GetString(4);
                            {
                                if (sharedNotes == null)
                                    sharedNotes = new ObservableCollection<Note>();
                                sharedNotes.Add(note);


                            }


                        }
                        sqlite_datareader.Close();


                    }
                    conn.Close();
                }
            }
            catch (Exception)
            {

            }
            return sharedNotes;
        }



        // ----------------------------------------NOTE DISPLAY PAGE DB FETCH----------------------------------------


        //It prints all the available users to whom we can share the note 
        public static ObservableCollection<YourNoteUWP.Models.User> ValidUsersToShare(string userTableName, string sharedTableName, Models.User owner, long noteId)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            ObservableCollection<YourNoteUWP.Models.User> userToShare = null;
            string query = "Select * from " + sqlCommandBuilder.QuoteIdentifier(userTableName) + " where not exists (Select sharedUserId from " + sqlCommandBuilder.QuoteIdentifier(sharedTableName) + " where sharedNoteId = @noteId ) ; ";

            try
            {

                using (SQLiteConnection conn = DBCreation.OpenConnection())
                {
                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    SQLiteParameter parameters = new SQLiteParameter("@noteId", noteId);
                    command.Parameters.Add(parameters);
                    using (SQLiteDataReader sqlite_datareader = command.ExecuteReader())
                    {
                        while (sqlite_datareader.Read())
                        {
                            if (userToShare == null)
                                userToShare = new ObservableCollection<Models.User>();
                            Models.User user = new Models.User(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1),
                                sqlite_datareader.GetString(2), (long)sqlite_datareader.GetValue(3));
                            userToShare.Add(user);

                        }

                        sqlite_datareader.Close();
                    }
                    conn.Close();
                }


            }
            catch (Exception)
            {
            }



            return userToShare;
        }


    }
}















//Used in the AutoSuggestionBox where it gets all the data from the both shared and personal notes of that user



//It reads all the notes details and displays it in the Grid View 


// It reads the content from a particular note using note id 
//public static Note ReadNotesData(string ownerId, string sharedUserId, string noteId, string tablename)
//{
//    Note note = new Note(" ", " ", " ", " ");

//    try
//    {
//        SQLiteConnection conn = DBCreation.OpenConnection();
//        conn.Open();
//        SQLiteCommand sqlite_cmd;
//        SQLiteDataReader sqlite_datareader;

//        sqlite_cmd = conn.CreateCommand();
//        sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";

//        sqlite_datareader = sqlite_cmd.ExecuteReader();

//        while (sqlite_datareader.Read())
//        {
//            string userId = sqlite_datareader.GetString(0);
//            string nId = sqlite_datareader.GetString(1);
//            //  Console.WriteLine("CHECK : " + userId+ " " + ownerId + " " + nId + " " + noteId );
//            if (ownerId == userId && nId == noteId)
//            {
//                //  sharedUserId = ownerId + "_" + sharedUserId;
//                note = new Note(sharedUserId, sqlite_datareader.GetString(2), sqlite_datareader.GetString(3), sqlite_datareader.GetString(4));
//                //    Console.WriteLine("INSIDE READING OF DATA and CHANGING THE USER ID "+note.user_Note_Id+ " " + ownerId + " " + sharedUserId + " " + noteId );
//                break;
//            }

//        }
//        sqlite_datareader.Close();
//        conn.Close();



//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }


//    return note;
//}

////It prints all the data of the currentUser except the given currentUser 
//public static Dictionary<string, Models.User> ReadAllUserData(string tableName, Models.User owner)
//{
//    Dictionary<string, Models.User> users = new Dictionary<string, Models.User>();

//    try
//    {
//        SQLiteConnection conn = DBCreation.OpenConnection();
//        conn.Open();
//        SQLiteCommand sqlite_cmd;
//        SQLiteDataReader sqlite_datareader;

//        sqlite_cmd = conn.CreateCommand();
//        sqlite_cmd.CommandText = $"SELECT * FROM {tableName} where userId!='{owner.userId}' ";
//        sqlite_datareader = sqlite_cmd.ExecuteReader();
//        while (sqlite_datareader.Read())
//        {
//            long count = (long)sqlite_datareader.GetValue(3);
//            YourNoteUWP.Models.User user = new Models.User(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1), "", count);
//            users[user.userId] = user;
//        }
//        sqlite_datareader.Close();
//        conn.Close();
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }



//    return users;
//}





//// It gets all the details of the notes 
//public static bool NoteDetails(string userId, long noteId, string tablename)
//{
//    try
//    {
//        SQLiteConnection conn = DBCreation.OpenConnection();
//        conn.Open();

//        SQLiteCommand sqlite_cmd;
//        SQLiteDataReader sqlite_datareader;

//        sqlite_cmd = conn.CreateCommand();
//        sqlite_cmd.CommandText = $"SELECT * FROM {tablename}";
//        sqlite_datareader = sqlite_cmd.ExecuteReader();


//        long col = DBFetch.GetColumnNumber(tablename);
//        while (sqlite_datareader.Read())
//        {
//            string ownerId = sqlite_datareader.GetString(0);
//            long nId = (long)sqlite_datareader.GetValue(1);
//            string title = sqlite_datareader.GetString(2);
//            string content = sqlite_datareader.GetString(3);


//            if (nId == noteId && userId == ownerId)
//            {

//                Console.Write("\t\t\t Note Id : ");
//                Console.WriteLine($"\t {noteId} \t\t\t");
//                Console.Write("\t\t\t  Title  : ");
//                Console.WriteLine($"\t {title}");
//                Console.WriteLine("\t\t\t  Content  : \t\t\t");
//                Console.WriteLine($"\t\t\t {content} \t\t\t");

//                sqlite_datareader.Close();
//                conn.Close();
//                return true;
//            }
//        }

//        sqlite_datareader.Close();
//        conn.Close();

//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }
//    return false;

//}




//// It gets no of columns in a table 
//public static long GetColumnNumber(string tablename)
//{
//    long noOfColumns = 0;
//    try
//    {
//        SQLiteConnection conn = DBCreation.OpenConnection();
//        conn.Open();
//        string columncount = $"SELECT COUNT(*) FROM pragma_table_info('{tablename}');";
//        SQLiteCommand sqlite_cmd;
//        sqlite_cmd = conn.CreateCommand();
//        sqlite_cmd.CommandText = columncount;
//        noOfColumns = (Int64)sqlite_cmd.ExecuteScalar();
//        conn.Close();

//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }

//    return noOfColumns;
//}