using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.System;
using YourNoteUWP;
using YourNoteUWP.Models;
    
namespace YourNoteUWP { 
    public class DBUpdation
    {
        
        //Updates the login count of the currentUser
        public static void UpdateLoginCount(string tableName, Models.User user)
        {
            try
            {

                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"UPDATE {tableName} SET loginCount = loginCount+1 where emailId = '{user.emailId}' ; ";
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {
                Console.WriteLine("The User Already Exists");
            }

        }


        //Updates the recent search count of the note
        public static void UpdateLoginCount(string tableName, Note note)
        {
            SQLiteConnection conn = DBCreation.CreateConnection();
            conn.Open();
            try
            {


                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                if (DBFetch.CheckNoteExists(tableName, note.noteId) == true)
                {
                    sqlite_cmd.CommandText = $"UPDATE {tableName} SET count = count+1 where noteId = '{note.noteId}' ; ";

                }
                else
                {
                    sqlite_cmd.CommandText = $"INSERT INTO {tableName}(noteId, count) VALUES ('{note.noteId}', '{1}');";
                }
                sqlite_cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }

        }


        //Creates new currentUser 
        public static void InsertNewUser(Models.User user)
        {
            try 
            {

                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.userTableName}(name, emailId, password, loginCount) VALUES ('{user.name}', '{user.emailId}','{user.password}', '{0}');";
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception )
            {
                Console.WriteLine("The User Already Exists");
            }
         
            //sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.userTableName}(UserId, Password,Name) VALUES ('{currentUser.Userid}' , ' " + { currentUser.Password} + "','" + currentUser.Name + "');";

        }

        //Creates new note 
         public static void InsertNewNote(Note newnote)
        {
            try
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.notesTableName} (userId, title, content) VALUES ('{newnote.userId}','{newnote.title}','{newnote.content}');";
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception)
            {
                Console.WriteLine("The Note already exists");
            }

  
        }

        //Creates a new entry for the shared note
         public static bool  InsertSharedNote(string ownerId, string shareduserId, long noteId)
        {
            bool isNoteShared = true;
            try 
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                if (DBFetch.CheckNoteShared(shareduserId, DBCreation.sharedTableName, noteId) == true)
                {
                    sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.sharedTableName} (ownerId , sharedUserId, sharedNoteId) VALUES ('{ownerId}','{shareduserId}','{noteId}');";
                    sqlite_cmd.ExecuteNonQuery();
                    conn.Close();

                }
                else
                {
                    isNoteShared = false; 
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Notes Already Shared");
            }

            return isNoteShared;
        }

        //Updation of the Note
        public static void UpdateNote(string tableName, Note changedNote)
        {
            try
            {
                SQLiteConnection conn = DBCreation.CreateConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"UPDATE '{tableName}' SET title='{changedNote.title}', content='{changedNote.content}' where noteId = '{changedNote.noteId}';";
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Notes Already Shared");
            }
        }
    }
}


// To make a SQLQuery return a value
//int yourValue = 0;
//string sql = "select day from time_limit where priority_level='p1'";
//using (SqlConnection conn = new SqlConnection(connString))
//{
//    SqlCommand cmd = new SqlCommand(sql, conn);
//    conn.Open();
//    yourValue = (Int32)cmd.ExecuteScalar();
//}


// To check if the table exists in the database or not 
//SELECT name FROM sqlite_master WHERE type='table' AND name='{table_name}';
//create table if not exists TableName(col1 typ1, ..., colN typN)