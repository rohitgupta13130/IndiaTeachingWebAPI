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
    public class ExamDAL
    {
        string _ExamDAL = "ExamDAL";
        public int SaveExam(Exam argExam)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveExam", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argExam.Id);
                    sqlCommand.Parameters.AddWithValue("@ExamName", argExam.ExamName);
                    sqlCommand.Parameters.AddWithValue("@ClassId", argExam.ClassId);
                    sqlCommand.Parameters.AddWithValue("@SubjectId", argExam.SubjectId);

                    SqlParameter outputParam = sqlCommand.Parameters.Add("@IdToReturn", System.Data.SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argExam.Id = Convert.ToInt32(sqlCommand.Parameters["@IdToReturn"].Value);
                    rs = argExam.Id;


                }

            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveExam", _ExamDAL, "Exam", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }

            return rs;
        }


        public Exam GetExam(ExamRequest argExamRequest)
        {
            Exam exam = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetExam", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argExamRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {

                            exam = new Exam();
                            exam.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            exam.ExamName = sqlDataReader["ExamName"].ToString();
                            exam.ClassId = Convert.ToInt32(sqlDataReader["ClassId"]);
                            exam.ClassName = sqlDataReader["ClassName"].ToString();
                            exam.SubjectId = Convert.ToInt32(sqlDataReader["SubjectId"]);
                            exam.SubjectName = sqlDataReader["SubjectName"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetExam", _ExamDAL, "Exam", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }

            return exam;
        }



        public List<Exam> GetExamList(ExamRequest argExamRequest)

        {
            List<Exam> examList = null;
            Exam exam = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetExam", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ExamName", argExamRequest.ExamName);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        examList = new List<Exam>();
                        while (sqlDataReader.Read())
                        {
                            exam = new Exam();

                            exam.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            exam.ExamName = sqlDataReader["ExamName"].ToString();
                            exam.ClassId = Convert.ToInt32(sqlDataReader["ClassId"]);
                            exam.ClassName = sqlDataReader["ClassName"].ToString();
                            exam.SubjectId = Convert.ToInt32(sqlDataReader["SubjectId"]);
                            exam.SubjectName = sqlDataReader["SubjectName"].ToString();
                            examList.Add(exam);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetExamList", _ExamDAL, "Exam", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }

            return examList;
        }




        public bool Delete(ExamRequest argExamRequest)
        {
            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteExam", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argExamRequest.Id);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    isSuccess = true;
                }

            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("Delete", _ExamDAL, "Exam", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }

            return isSuccess;
        }


    }
}