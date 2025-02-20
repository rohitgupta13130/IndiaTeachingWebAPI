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
    public class StudentInfoDAL
    {
        string _StudentInfoDAL = "StudentInfoDAL";
        public int SaveStudentInfo(StudentInfo argStudentInfo)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveStudentInfo", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@studentId", argStudentInfo.Id);
                    sqlCommand.Parameters.AddWithValue("@contactNo", argStudentInfo.ContactNo);
                    sqlCommand.Parameters.AddWithValue("@emailId", argStudentInfo.Email);
                    sqlCommand.Parameters.AddWithValue("@userName", argStudentInfo.UserName);
                    sqlCommand.Parameters.AddWithValue("@profileImagePath", argStudentInfo.ProfileImagePath);
                    

                    SqlParameter outputParam = sqlCommand.Parameters.Add("@studentIdToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argStudentInfo.Id = Convert.ToInt32(sqlCommand.Parameters["@studentIdToReturn"].Value);
                    rs = argStudentInfo.Id;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveStudentInfo", _StudentInfoDAL, "StudentInfo", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }





        public StudentInfo GetStudentInfo(StudentInfoRequest argStudentInfoRequest)
        {
            StudentInfo studentInfo = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetStudentInfo", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@studentId", argStudentInfoRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            studentInfo = new StudentInfo();
                            studentInfo.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            studentInfo.ContactNo = sqlDataReader["ContactNo"].ToString();
                            studentInfo.Email = sqlDataReader["EmailId"].ToString();
                            studentInfo.UserName = sqlDataReader["UserName"].ToString();
                            studentInfo.ProfileImagePath = sqlDataReader["ProfileImagePath"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudentInfo", _StudentInfoDAL, "StudentInfo", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return studentInfo;
        }



        public List<StudentInfo> GetStudentInfoList(StudentInfoRequest argStudentInfoRequest)
        {
            List<StudentInfo> studentInfoList = null;
            StudentInfo studentInfo = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;


            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetStudentInfo", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();


                    if (sqlDataReader.HasRows)
                    {
                        studentInfoList = new List<StudentInfo>();
                        while (sqlDataReader.Read())
                        {
                            studentInfo = new StudentInfo();
                            studentInfo.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            studentInfo.ContactNo = sqlDataReader["ContactNo"].ToString();
                            studentInfo.Email = sqlDataReader["EmailId"].ToString();
                            studentInfo.UserName = sqlDataReader["UserName"].ToString();
                            studentInfo.ProfileImagePath = sqlDataReader["ProfileImagePath"].ToString();

                            studentInfoList.Add(studentInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudentInfoList", _StudentInfoDAL, "StudentInfo", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return studentInfoList;
        }


    }
}