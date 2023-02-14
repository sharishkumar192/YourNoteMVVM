using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
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

        // ----------------------------------------SIGN UP PAGE DB UPDATION----------------------------------------

        //Creates new currentUser 
        public static void InsertNewUser(Models.User user) //Needed
        {
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
            {

               
                    //DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                    //DbCommandBuilder commandBuilder = factory.CreateCommandBuilder();
                    //string sanitizedTableName = commandBuilder.QuoteIdentifier(DBCreation.userTableName);
                    SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
                    string query = $"INSERT INTO " + sqlCommandBuilder.QuoteIdentifier(DBCreation.userTableName) + " (NAME, USERID, PASSWORD) VALUES ( @name, @userId, @password);";


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
                conn.Close();

                



            }
           catch(Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }

            //sqlite_cmd.CommandText = $"INSERT INTO {DBCreation.userTableName}(UserId, Password,Name) VALUES ('{currentUser.Userid}' , ' " + { currentUser.Password} + "','" + currentUser.Name + "');";

        }


        // ----------------------------------------ACCOUNT PAGE DB UPDATION----------------------------------------

        //Creates new note 
        public static void InsertNewNote(Note newNote)// Needed 
        {

            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            SQLiteConnection conn = DBCreation.OpenConnection();


            string query = $"INSERT INTO "+ sqlCommandBuilder.QuoteIdentifier(DBCreation.notesTableName)  + " (USERID, TITLE, CONTENT, NOTECOLOR) VALUES (@userId, @title, @content, @noteColor);";

            try
            {

              
                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    SQLiteParameter[] parameters = new SQLiteParameter[4];
                    parameters[0] = new SQLiteParameter("@userId", newNote.userId);
                    parameters[1] = new SQLiteParameter("@title", newNote.title);
                    parameters[2] = new SQLiteParameter("@content", newNote.content);
                    parameters[3] = new SQLiteParameter("@noteColor", newNote.noteColor);
                    command.Parameters.Add(parameters[0]);
                    command.Parameters.Add(parameters[1]);
                    command.Parameters.Add(parameters[2]);
                    command.Parameters.Add(parameters[3]);
                    command.ExecuteNonQuery();
                    conn.Close();

                
               
             
            
                }
           catch(Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }


        }


        // ----------------------------------------NOTE DISPLAY PAGE DB UPDATION----------------------------------------

        //Creates a new entry for the shared note
        public static void InsertSharedNote(string sharedTableName, string sharedUserId, long noteId)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
  string query = $"INSERT INTO " +  sqlCommandBuilder.QuoteIdentifier(sharedTableName)+ "  VALUES (@SHAREDUSERID, @NOTEID);";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
            {
                

                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    SQLiteParameter[] parameters = new SQLiteParameter[2];
                    parameters[0] = new SQLiteParameter("@sharedUserId", sharedUserId);
                    parameters[1] = new SQLiteParameter("@noteId", noteId);
                    command.Parameters.Add(parameters[0]);
                    command.Parameters.Add(parameters[1]);
                    command.ExecuteNonQuery();
                    conn.Close();

                




            }
           catch(Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }
        }
        //Updation of the Note
        public static void UpdateNote(string notesTableName, string title, string content, long noteId)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            string query = $"UPDATE " + sqlCommandBuilder.QuoteIdentifier(notesTableName) + " SET TITLE= @title , CONTENT= @content WHERE NOTEID = @noteId  ;";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[3];
                parameters[0] = new SQLiteParameter("@title", title);
                parameters[1] = new SQLiteParameter("@content",content);
                parameters[2] = new SQLiteParameter("@noteId", noteId);


                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.Parameters.Add(parameters[2]);
                command.ExecuteNonQuery();
                conn.Close();




            }
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }

        }
        //Updation of the Note Title
        public static void UpdateNoteTitle(string notesTableName, string title, long noteId)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            string query = $"UPDATE " + sqlCommandBuilder.QuoteIdentifier(notesTableName) + " SET TITLE= @title, NOTEID = @noteId  ;";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[2];
                parameters[0] = new SQLiteParameter("@title", title);
                parameters[1] = new SQLiteParameter("@noteId", noteId);


                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.ExecuteNonQuery();
                conn.Close();




            }
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }

        }

        public static void UpdateNoteCount(string notesTableName, long searchCount, long noteId)
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            string query = $"UPDATE " + sqlCommandBuilder.QuoteIdentifier(notesTableName) + " SET  SEARCHCOUNT = @count  WHERE NOTEID = @noteId  ;";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
            {

                SQLiteCommand command = new SQLiteCommand(query, conn);
                SQLiteParameter[] parameters = new SQLiteParameter[2];
                parameters[0] = new SQLiteParameter("@count", searchCount);
                parameters[1] = new SQLiteParameter("@noteId", noteId);


                command.Parameters.Add(parameters[0]);
                command.Parameters.Add(parameters[1]);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }


        }

        //Updation of the Note Content
        public static void UpdateNoteContent(string notesTableName, string content, long noteId)// Needed
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();
            string query  = $"UPDATE " + sqlCommandBuilder.QuoteIdentifier(notesTableName) + " SET  CONTENT= @content WHERE NOTEID = @noteId  ;";
            SQLiteConnection conn = DBCreation.OpenConnection();
            try
            {
              
                    SQLiteCommand command = new SQLiteCommand(query, conn);
                    SQLiteParameter[] parameters = new SQLiteParameter[2];
                    parameters[0] = new SQLiteParameter("@content", content);
                    parameters[1] = new SQLiteParameter("@noteId", noteId);


                    command.Parameters.Add(parameters[0]);
                    command.Parameters.Add(parameters[1]);
                    command.ExecuteNonQuery();
                    conn.Close();
            }
           catch(Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }

        }
    
        //Delete the Note
        public static void DeleteNote(string notesTableName, string sharedTableName, long noteId)// Needed 
        {
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder();


            string query1 = $"DELETE FROM " + sqlCommandBuilder.QuoteIdentifier(sharedTableName) + "WHERE SHAREDNOTEID  = @noteId ; ";
            string query2 = $"DELETE FROM " + sqlCommandBuilder.QuoteIdentifier(notesTableName) + "WHERE NOTEID  = @noteId ; ";
            SQLiteConnection conn = DBCreation.OpenConnection();

            try
            {
                
               
                    SQLiteCommand command = new SQLiteCommand(query1, conn);
                    SQLiteParameter parameters = new SQLiteParameter("@noteId", noteId);
                    command.Parameters.Add(parameters);
                    command.ExecuteNonQuery();

                    command.CommandText = query2;
                    command.ExecuteNonQuery();
                    conn.Close();

                
            }
           catch(Exception e) { Debug.WriteLine(e.Message); }
            finally
            {
                conn.Close();

            }

        }



      

    }
}
