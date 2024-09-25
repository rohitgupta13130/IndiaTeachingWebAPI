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
    public class SubjectDAL
    {
        string _SubjectDAL = "SubjectDAL";
        public int SaveSubject(Subject argSubject)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveSubject", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ID", argSubject.ID);
                    sqlCommand.Parameters.AddWithValue("@SubjectName", argSubject.SubjectName);
                    sqlCommand.Parameters.AddWithValue("@IsActive", argSubject.IsActive);

                    SqlParameter outputParam = sqlCommand.Parameters.Add("@IDToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argSubject.ID = Convert.ToInt32(sqlCommand.Parameters["@IDToReturn"].Value);
                    rs = argSubject.ID;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveSubject", _SubjectDAL, "Subject", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }

        public Subject GetSubject(SubjectRequest argSubjectRequest)
        {
            Subject subject = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetSubject", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ID", argSubjectRequest.ID);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            subject = new Subject();
                            subject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                            subject.SubjectName = sqlDataReader["SubjectName"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetSubject", _SubjectDAL, "Subject", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return subject;
        }

        public List<Subject> GetSubjectList(SubjectRequest argSubjectRequest)
        {
            List<Subject> subjectList = null;
            Subject subject = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetSubject", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@SubjectName", argSubjectRequest.SubjectName);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        subjectList = new List<Subject>();
                        while (sqlDataReader.Read())
                        {
                            subject = new Subject();
                            subject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                            subject.SubjectName = sqlDataReader["SubjectName"].ToString();
                            subjectList.Add(subject);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetSubjectList", _SubjectDAL, "Subject", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return subjectList;
        }


        

            public bool DeleteSubject(SubjectRequest argSubjectRequest)
        {

            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {

                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteSkill", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argSubjectRequest.ID);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("DeleteSubject", _SubjectDAL, "Subject", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();

            }

            return isSuccess;

        }
    }
}