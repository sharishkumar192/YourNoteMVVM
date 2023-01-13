using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Graphics.Printing.PrintTicket;
using Windows.System;
using YourNoteUWP;
using YourNoteUWP.Models;
    
namespace YourNoteUWP { 
    public class DBUpdation
    {
        
        //Updates the login count of the currentUser
        public static void UpdateLoginCount(string tableName, Models.User user)
        {
            string query = $"UPDATE {tableName} SET loginCount = loginCount+1 where userId = '{user.userId}' ; ";
            try
            {

                using (SQLiteConnection conn = DBCreation.OpenConnection()) 
                {

                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    SQLiteParameter[] parameters = new SQLiteParameter[2];
                    parameters[0] = new SQLiteParameter("@notesTableName", DBCreation.notesTableName);
                    parameters[1] = new SQLiteParameter("@userTableName", DBCreation.userTableName);
                    command.Parameters.Add(parameters[0]);
                    command.Parameters.Add(parameters[1]);
                    command.ExecuteNonQuery();


                }

            }
            catch (Exception)
            {
                Console.WriteLine("The User Already Exists");
            }

        }


        //Updates the recent search count of the note
        public static void UpdateRecentSearchedCount(string tableName, Note note)
        {
            SQLiteConnection conn = DBCreation.OpenConnection();
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

                using(SQLiteConnection conn = DBCreation.OpenConnection())
                {
                    //DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                    //DbCommandBuilder commandBuilder = factory.CreateCommandBuilder();
                    //string sanitizedTableName = commandBuilder.QuoteIdentifier(DBCreation.userTableName);
                    SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
                    string query = $"INSERT INTO " + sqlCommandBuilder.QuoteIdentifier(DBCreation.userTableName) + " (name, userId, password) VALUES ( @name, @userId, @password);";


                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    SQLiteParameter[] parameters = new SQLiteParameter[3];
                    //parameters[0] = new SQLiteParameter("@userTableName", DBCreation.userTableName);
                    parameters[0] = new SQLiteParameter("@name",user.name);
                    parameters[1] = new SQLiteParameter("@userId", user.userId);
                    parameters[2] = new SQLiteParameter("@password", user.password);
                    command.Parameters.Add(parameters[0]);
                    command.Parameters.Add(parameters[1]);
                    command.Parameters.Add(parameters[2]);
                   // command.Parameters.Add(parameters[3]);
                    command.ExecuteNonQuery();


                }



            }
            catch (Exception )
            {
                
            }
         
            //sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.userTableName}(UserId, Password,Name) VALUES ('{currentUser.Userid}' , ' " + { currentUser.Password} + "','" + currentUser.Name + "');";

        }

        //Creates new note 
         public static void InsertNewNote(Note newNote)
        {
            SQLiteConnection conn = DBCreation.OpenConnection();
            conn.Open();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            try
            {
                  sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.notesTableName} (userId, title, content,noteColor,) VALUES ('{newNote.userId}','{newNote.title}','{newNote.content}', '{newNote.noteColor}');";
                sqlite_cmd.ExecuteNonQuery();

             
              conn.Close();
                }
            catch (Exception)
            {
             
            }
            finally
            {
                conn.Close();
            }
  
        }

        //Creates a new entry for the shared note
         public static bool  InsertSharedNote(string ownerId, string shareduserId, long noteId)
        {
            bool isNoteShared = true;
            try 
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
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
            catch (Exception)
            {
            }

            return isNoteShared;
        }

        //Updation of the Note
        public static void UpdateNote(string tableName, Note changedNote)
        {
            try
            {
                SQLiteConnection conn = DBCreation.OpenConnection();
                conn.Open();
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"UPDATE '{tableName}' SET title='{changedNote.title}', content='{changedNote.content}' where noteId = '{changedNote.noteId}';";
                sqlite_cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception )
            {
            }
        }
    
        //Delete the Note
        public static void DeleteNote(string notesTableName, string sharedTableName, Note noteToDelete)
        {  
            
            
            SQLiteConnection conn = DBCreation.OpenConnection();
            conn.Open();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            try
            {
                

                sqlite_cmd.CommandText = $"DELETE FROM {notesTableName} WHERE userId = \"{noteToDelete.userId}\" and noteId = {noteToDelete.noteId};";
                sqlite_cmd.ExecuteNonQuery();

          
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = $"DELETE FROM {sharedTableName} WHERE ownerId = \"{noteToDelete.userId}\" and sharedNoteId={noteToDelete.noteId};" ;
                sqlite_cmd.ExecuteNonQuery();


                conn.Close();


            }
            catch (Exception)
            {
                
            }
            finally
            {
                conn.Close();

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