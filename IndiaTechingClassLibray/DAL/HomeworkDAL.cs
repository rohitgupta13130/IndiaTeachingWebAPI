using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using India_Teaching.Models;
using India_Teaching.Request;

namespace India_Teaching.DAL
{
    public class HomeworkDAL
    {


        public int SaveHomeWork(HomeWork argHomeWork)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveHomework", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argHomeWork.Id);
                    sqlCommand.Parameters.AddWithValue("@ClassId", argHomeWork.ClassId);
                    sqlCommand.Parameters.AddWithValue("@SubjectId", argHomeWork.SubjectId);
                    sqlCommand.Parameters.AddWithValue("@TeacherId", argHomeWork.TeacherId);
                    sqlCommand.Parameters.AddWithValue("@Homework", argHomeWork.Homework);


                    SqlParameter outputParam = sqlCommand.Parameters.Add("@IdToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argHomeWork.Id = Convert.ToInt32(sqlCommand.Parameters["@IdToReturn"].Value);
                    rs = argHomeWork.Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return rs;
        }


        public HomeWork GetHomeWork(HomeWorkRequest argHomeWorkRequest)
        {
            HomeWork homeWork = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetHomework", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id",argHomeWorkRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {

                        while (sqlDataReader.Read())
                        {
                            homeWork = new HomeWork();
                            homeWork.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            homeWork.ClassId = Convert.ToInt32(sqlDataReader["ClassId"]);
                            homeWork.ClassName = sqlDataReader["ClassName"].ToString();
                            homeWork.SubjectId = Convert.ToInt32(sqlDataReader["SubjectId"]);
                            homeWork.SubjectName = sqlDataReader["SubjectName"].ToString();
                            homeWork.TeacherId = Convert.ToInt32(sqlDataReader["TeacherId"]);
                            homeWork.TeacherName = sqlDataReader["Fullname"].ToString();
                            homeWork.Homework = sqlDataReader["Homework"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return homeWork;
        }

        public List<HomeWork> GetHomeWorkList(HomeWorkRequest argHomeWorkRequest)
        {
            List<HomeWork> homeWorkList = null;
            HomeWork homeWork = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetHomework", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Homework", argHomeWorkRequest.Homework);
                    //sqlCommand.Parameters.AddWithValue("@TeacherId", argHomeWorkRequest.TeacherId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        homeWorkList = new List<HomeWork>();
                        while (sqlDataReader.Read())
                        {
                            homeWork = new HomeWork();
                            homeWork.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            homeWork.ClassId = Convert.ToInt32(sqlDataReader["ClassId"]);
                            homeWork.ClassName = sqlDataReader["ClassName"].ToString();
                            homeWork.SubjectId = Convert.ToInt32(sqlDataReader["SubjectId"]);
                            homeWork.SubjectName = sqlDataReader["SubjectName"].ToString();
                            homeWork.TeacherId = Convert.ToInt32(sqlDataReader["TeacherId"]);
                            homeWork.TeacherName = sqlDataReader["Fullname"].ToString();
                            homeWork.Homework = sqlDataReader["Homework"].ToString();
                            homeWorkList.Add(homeWork);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return homeWorkList;
        }


        public bool Delete(HomeWorkRequest argHoWorkRequest)
        {
            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteHomework", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argHoWorkRequest.Id);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                
            }
            return isSuccess;
        }



    }
}