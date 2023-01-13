using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        public static SQLiteConnection OpenConnection()
        {
            // Create a new database connection:
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=C:\\Users\\harish-pt6263\\AppData\\Local\\Packages\\f370c6dd-350c-4488-8938-3a4de12a2669_ekzxxtgcwg1pc\\LocalState\\database.db; Version = 3; New = True; Compress = True; ");
            sqlite_conn.Open();
            return sqlite_conn;
        }

        //Creates The User Table 
        public static void CreateUserTable()
        {
            string query =
            $"CREATE TABLE  {userTableName} (name VARCHAR(10000)," +
            $" userId VARCHAR(10000) PRIMARY KEY," +
            $" password VARCHAR(10000)," +
            $" loginCount INTEGER DEFAULT 0 )";
            try
            {
                using (SQLiteConnection conn = DBCreation.OpenConnection())
                {

                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch (Exception)
            {

            }




        }

        //Creates The Notes Table 

        public static void CreateNotesTable()
        {
            string query = $"CREATE TABLE if not exists {notesTableName} " +
      $"(userId VARCHAR(10000)," +
      $"noteId INTEGER PRIMARY KEY AUTOINCREMENT," +
      $"title VARCHAR(10000)," +
      $"content VARCHAR(1000), " +
      $"noteColor VARCHAR(7) DEFAULT \"#c6e8b7\" ,  " +
      $"searchCount INTEGER DEFAULT 0  ,  " +
      $"FOREIGN KEY(userId) REFERENCES {userTableName} (userId))";
            try
            {

                using (SQLiteConnection conn = OpenConnection())
                {
                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }


            }
            catch (Exception)
            {

            }

        }

        //Creates The Shared Table 

        public static void SharedNotesTableCreation()
        {
            string query =
     $"CREATE TABLE if not exists {sharedTableName} " +
     $"(sharedUserId VARCHAR(10000) ,  " +
     $"sharedNoteId Integer ," +
     $"PRIMARY KEY (sharedUserId, sharedNoteId)" +
     $" FOREIGN KEY(sharedUserId) REFERENCES {userTableName} (userId)" +
       $" FOREIGN KEY(sharedNoteId) REFERENCES {notesTableName} (noteId))";
            try
            {

                using (SQLiteConnection conn = OpenConnection())
                {
                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {

            }

        }
    }
}

