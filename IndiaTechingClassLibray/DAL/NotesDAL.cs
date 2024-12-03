using India_Teaching.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;

namespace India_Teaching.DAL
{
    public class NotesDAL
    {
        string _NotesDAL = "NotesDAL";
        public int SaveNotes(Notes argNotes, HttpPostedFileBase file)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveNotes", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@notesId", argNotes.Id);
                    sqlCommand.Parameters.AddWithValue("@linkfortextfile", argNotes.LinkfortextFile);
                    sqlCommand.Parameters.AddWithValue("@Teacherid", argNotes.Teacherid);
                    sqlCommand.Parameters.AddWithValue("@title", argNotes.Title);
                    sqlCommand.Parameters.AddWithValue("@IsActive", argNotes.IsActive);
                    SqlParameter outputParam = sqlCommand.Parameters.Add("@notesIdToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argNotes.Id = Convert.ToInt32(sqlCommand.Parameters["@notesIdToReturn"].Value);
                    rs = argNotes.Id;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveNotes", _NotesDAL, "Notes", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }


        public Notes GetNotes(NotesRequest argNotesRequest)
        {
            Notes notes = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetNotes", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@notesId", argNotesRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            notes = new Notes();
                            notes.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            notes.LinkfortextFile = sqlDataReader["LinkfortextFile"].ToString();
                            notes.Teacherid = Convert.ToInt32(sqlDataReader["Teacherid"].ToString());
                            notes.Title = sqlDataReader["Title"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetNotes", _NotesDAL, "Notes", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return notes;
        }

        public List<Notes> GetNotesList(NotesRequest argNotesRequest)
        {
            List<Notes> notesList = null;
            Notes notes = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;


            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetNotes", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@title", argNotesRequest.Title);

                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();


                    if (sqlDataReader.HasRows)
                    {
                        notesList = new List<Notes>();
                        while (sqlDataReader.Read())
                        {
                            notes = new Notes();
                            notes.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            notes.LinkfortextFile = sqlDataReader["LinkfortextFile"].ToString();
                            notes.Teacherid = Convert.ToInt32(sqlDataReader["Teacherid"].ToString());
                            notes.Title = sqlDataReader["Title"].ToString();
                            notesList.Add(notes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetNotesList", _NotesDAL, "Notes", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return notesList;
        }


        public bool DeleteNotes(NotesRequest argNotesRequest)
        {

            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {

                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteNotes", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argNotesRequest.Id);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    isSuccess = true;



                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("DeleteNotes", _NotesDAL, "Notes", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();

            }

            return isSuccess;

        }





    }
}