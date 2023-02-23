using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using YourNoteUWP;
using YourNoteUWP.Models;
namespace YourNoteUWP
{
    public class DBCreation
    {
        public static string userTableName = "UserTable",
            notesTableName = "NotesTable",
            sharedTableName = "ShareTable";
        public static void UserTableCreation()
        {
            CreateUserTable();
        }

        public static void NotesTableCreation()
        {
            CreateNotesTable();
        }

        // Creates an object of SQLiteConnection 

        private static SQLiteConnection _sQLiteConnection = null;
        public static SQLiteConnection SQLiteConnection
        {

            get
            {
                if (_sQLiteConnection == null)
                {
                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    _sQLiteConnection = new SQLiteConnection("Data Source=" + localFolder.Path + "\\database.db; Version = 3; New = True; Compress = True; ");

                }
                return _sQLiteConnection;

            }

        }
        public static SQLiteConnection OpenConnection()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            SQLiteConnection sqlite_conn = SQLiteConnection;
            sqlite_conn.Open();
            return sqlite_conn;
        }


        //Creates The User Table 
        public static void CreateUserTable()
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            string query =
            $"CREATE TABLE IF NOT EXISTS " + sqlCommandBuilder.QuoteIdentifier(DBCreation.userTableName) + " (NAME VARCHAR(10000)," +
            $" USERID VARCHAR(10000) PRIMARY KEY," +
            $" PASSWORD VARCHAR(10000)," +
            $" LOGINCOUNT INTEGER DEFAULT 0 )";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
           catch(Exception e) { Debug.WriteLine(e.Message); }

            finally
            {
                conn.Close();

            }

        }

        //Creates The Notes Table 

        public static void CreateNotesTable()
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            string query = $"CREATE TABLE IF NOT EXISTS " + sqlCommandBuilder.QuoteIdentifier(DBCreation.notesTableName) +
      $"(USERID VARCHAR(10000)," +
      $"NOTEID INTEGER PRIMARY KEY AUTOINCREMENT," +
      $"TITLE VARCHAR(10000)," +
      $"CONTENT VARCHAR(1000), " +
      $"NOTECOLOR INTEGER DEFAULT 0 ,  " +
      $"SEARCHCOUNT INTEGER DEFAULT 0  ,  " +
      $"CREATIONDAY VARCHAR(27)  ,  " +
      $"MODIFIEDDAY VARCHAR(27)  ,  " +
      $"FOREIGN KEY(USERID) REFERENCES " + sqlCommandBuilder.QuoteIdentifier(DBCreation.userTableName) + "(USERID) ON DELETE CASCADE)" ;
            SQLiteConnection conn = OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();


            }
           catch(Exception e) { Debug.WriteLine(e.Message); }

            finally
            {
                conn.Close();

            }

        }

        //Creates The Shared Table 

        public static void SharedNotesTableCreation()
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            string query = $"CREATE TABLE IF NOT EXISTS " + sqlCommandBuilder.QuoteIdentifier(DBCreation.sharedTableName) +
     $"(SHAREDUSERID VARCHAR(10000) ,  " +
     $"SHAREDNOTEID INTEGER ," +
     $"PRIMARY KEY (SHAREDUSERID, SHAREDNOTEID)" +
     $" FOREIGN KEY(SHAREDUSERID) REFERENCES" + sqlCommandBuilder.QuoteIdentifier(DBCreation.userTableName) + "(USERID) ON DELETE CASCADE" +
       $" FOREIGN KEY(SHAREDNOTEID) REFERENCES " + sqlCommandBuilder.QuoteIdentifier(DBCreation.notesTableName) + "(NOTEID) ON DELETE CASCADE)";
            SQLiteConnection conn = OpenConnection();
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();

            }

        }
    }
}

