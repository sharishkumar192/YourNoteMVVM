using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
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
        private static int _logincount = 2;

        // ----------------------------------------SIGN UP PAGE DB FETCHES----------------------------------------

        //Checks the email Id which the currentUser enters already exists or not 
        public static bool CheckValidEmail(string userTableName, string userId)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            bool check = false;
            string query = $"SELECT * FROM " + sqlCommandBuilder.QuoteIdentifier(userTableName) + " WHERE USERID = @userId ";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
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
            catch (Exception e) { Debug.WriteLine(e.Message); }

            finally
            {
                conn.Close();

            }
            return check;

        }



        //  ----------------------------------------LOGIN IN PAGE DB FETCHES----------------------------------------


        // It prints all the data of the currentUser 
        public static ObservableCollection<Models.User> FrequentLoggedUsers(string userTableName)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();

            string query = $"SELECT * FROM " + sqlCommandBuilder.QuoteIdentifier(userTableName) + " WHERE LOGINCOUNT >= @count ORDER BY LOGINCOUNT DESC; ";
            ObservableCollection<Models.User> users = null;
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter parameters = new SQLiteParameter("@count", _logincount);
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

            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }

            return users;
        }



        // Checks if the currentUser's id and password is present in the table or not, updates the login count of the user if the user exists 
        public static Tuple<Models.User, bool> ValidateUser(string tableName, string loggedUserId, string loggedPassword) //Needed
        {

            bool isExist = false;
            Models.User userDetails = null;
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            string query1 = $"SELECT * FROM " + sqlCommandBuilder.QuoteIdentifier(tableName) + " WHERE USERID = @userId  AND PASSWORD = @password ; ";

            string query2 = $"UPDATE " + sqlCommandBuilder.QuoteIdentifier(tableName) + " SET LOGINCOUNT = LOGINCOUNT+1  WHERE USERID = @userId ; ";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
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
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

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


            string query = $"SELECT * FROM " + sqlCommandBuilder.QuoteIdentifier(noteTableName) + " WHERE USERID = @userId ORDER BY SEARCHCOUNT DESC  ";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
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



            
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }
            return notes;
        }

        //Get Shared Notes
        public static ObservableCollection<Note> GetSharedNotes(string notesTableName, string sharedTableName, Models.User loggedUser)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            ObservableCollection<Note> sharedNotes = null;
            string query = $"SELECT * FROM " + sqlCommandBuilder.QuoteIdentifier(notesTableName) + ", " + sqlCommandBuilder.QuoteIdentifier(sharedTableName) + " WHERE NOTEID = SHAREDNOTEID AND SHAREDUSERID = @userId  ORDER BY SEARCHCOUNT DESC ; ";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
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
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }

            return sharedNotes;
        }



        // ----------------------------------------NOTE DISPLAY PAGE DB FETCH----------------------------------------


        //It prints all the available users to whom we can share the note 
        public static ObservableCollection<YourNoteUWP.Models.User> ValidUsersToShare(string userTableName, string sharedTableName, Models.User owner, long noteId)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            ObservableCollection<YourNoteUWP.Models.User> userToShare = null;
            string query = "SELECT * FROM " + sqlCommandBuilder.QuoteIdentifier(userTableName) + " WHERE NOT EXISTS (SELECT SHAREDUSERID FROM " + sqlCommandBuilder.QuoteIdentifier(sharedTableName) + " WHERE SHAREDNOTEID = @noteId ) ; ";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
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
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }



            return userToShare;
        }


    }
}

