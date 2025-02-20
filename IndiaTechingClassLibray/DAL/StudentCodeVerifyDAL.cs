using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace India_Teaching.DAL
{
    public class StudentCodeVerifyDAL
    {
        string _StudentCodeVerifyDAL = "StudentCodeVerifyDAL";
        public int SaveStudentCodeVerify(StudentCodeVerify argStudentCodeVerify)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveStudentCodeVerify", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@studentId", argStudentCodeVerify.Id);
                    sqlCommand.Parameters.AddWithValue("@currentStatus", argStudentCodeVerify.CurrentStatus);
                    sqlCommand.Parameters.AddWithValue("@code", argStudentCodeVerify.Code);
                   

                    SqlParameter outputParam = sqlCommand.Parameters.Add("@studentIdToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argStudentCodeVerify.Id = Convert.ToInt32(sqlCommand.Parameters["@studentIdToReturn"].Value);
                    rs = argStudentCodeVerify.Id;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveStudentCodeVerify", _StudentCodeVerifyDAL, "StudentCodeVerify", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }


        public StudentCodeVerify GetStudentCodeVerify(StudentCodeVerifyRequest argStudentCodeVerifyRequest)
        {
            StudentCodeVerify studentCodeVerify = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetStudentCodeVerify", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@studentId", argStudentCodeVerifyRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            studentCodeVerify = new StudentCodeVerify();
                            studentCodeVerify.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            studentCodeVerify.CurrentStatus = Convert.ToInt32(sqlDataReader["CurrentStatus"]);
                            studentCodeVerify.Code = sqlDataReader["Code"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudentCodeVerify", _StudentCodeVerifyDAL, "StudentCodeVerify", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return studentCodeVerify;
        }



        public List<StudentCodeVerify> GetStudentCodeVerifyList(StudentCodeVerifyRequest argCodeVerifyRequest)
        {
            List<StudentCodeVerify> studentCodeVerifyList = null;
            StudentCodeVerify studentCodeVerify = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;


            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetStudentCodeVerify", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();


                    if (sqlDataReader.HasRows)
                    {
                        studentCodeVerifyList = new List<StudentCodeVerify>();
                        while (sqlDataReader.Read())
                        {
                            studentCodeVerify = new StudentCodeVerify();
                            studentCodeVerify.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            studentCodeVerify.CurrentStatus = Convert.ToInt32(sqlDataReader["CurrentStatus"]);
                            studentCodeVerify.Code = sqlDataReader["Code"].ToString();

                            studentCodeVerifyList.Add(studentCodeVerify);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudentCodeVerifyList", _StudentCodeVerifyDAL, "StudentCodeVerify", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return studentCodeVerifyList;
        }

    }
}