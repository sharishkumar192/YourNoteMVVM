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
            sharedTableName = "ShareTable",
        recentSearchesTableName = "RecentSearchTable";
        public static void UserTableCreation()
        {
            // SQLiteConnection sqlite_conn = CreateConnection();
            CreateUserTable();
            //  sqlite_conn.Close();
        }

         public static void NotesTableCreation()
        {
            //  SQLiteConnection sqlite_conn = CreateConnection();
            CreateNotesTable();
            //  sqlite_conn.Close();

        }

        // Creates an object of SQLiteConnection 
        public static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
      
            // Create a new database connection:
            try
            {
                sqlite_conn = new SQLiteConnection("Data Source=C:\\Users\\harish-pt6263\\AppData\\Local\\Packages\\f370c6dd-350c-4488-8938-3a4de12a2669_ekzxxtgcwg1pc\\LocalState\\database.db; Version = 3; New = True; Compress = True; ");

                // Open the connection:


                return sqlite_conn;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }


            return null;
        }

        //Creates The User Table 
        public static void CreateUserTable()
        {
            try 
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                string table =
        $"CREATE TABLE if not exists {userTableName}(name VARCHAR(10000)," +
        $" emailId VARCHAR(10000) PRIMARY KEY," +
        $"password VARCHAR(10000)," +
        $"loginCount INTEGER )";
                //  create table Newsh1(uid integer primary key, ch varchar(10000));
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = table;
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

         
        }

        //Creates The Notes Table 

         public static void CreateNotesTable()
        {
            try 
            {

                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                // create table newsh(id integer, FOREIGN KEY(id) REFERENCES Newsh1(uid));
                SQLiteCommand sqlite_cmd;
                string table =
  $"CREATE TABLE if not exists {DBCreation.notesTableName}" +
  $"(userId VARCHAR(10000)," +
  $"noteId INTEGER PRIMARY KEY AUTOINCREMENT," +
  $"title VARCHAR(10000)," +
  $"content VARCHAR(1000), " +
  $"FOREIGN KEY(userId) REFERENCES {DBCreation.userTableName}(emailId))";
                              sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = table;
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        //Creates The Shared Table 

        public static void SharedNotesTableCreation()
        {
            try 
            {

                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                string table =
      $"CREATE TABLE if not exists {sharedTableName}" +
      $"(ownerId VARCHAR(10000)," +
      $"sharedUserId VARCHAR(10000)  " +
      $",sharedNoteId Integer," +
      $"PRIMARY KEY (ownerId, sharedUserId, sharedNoteId)" +
      $" FOREIGN KEY(ownerId) REFERENCES {userTableName}(emailId)" +
      $" FOREIGN KEY(sharedUserId) REFERENCES {userTableName}(emailId)" +
        $" FOREIGN KEY(sharedNoteId) REFERENCES {notesTableName}(noteId))";

                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = table;
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n");
            }
         
        }


        public static void RecentSearchesTableCreation()
        {
            SQLiteConnection conn = DBCreation.CreateConnection();
            conn.Open();
            SQLiteCommand sqlite_cmd;
            try
            {
                string table =
  $"CREATE TABLE if not exists {DBCreation.recentSearchesTableName}" +
  $"(noteId INTEGER PRIMARY KEY," +
  $"count INTEGER," +
  $"FOREIGN KEY(noteId) REFERENCES {DBCreation.notesTableName}(noteId))";
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = table;
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch(Exception)
            { }
            finally
              {
                conn.Close();

            }
            }
    }
}
