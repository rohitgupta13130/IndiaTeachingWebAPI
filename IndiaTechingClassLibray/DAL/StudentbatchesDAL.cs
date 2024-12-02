
using India_Teaching.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using India_Teaching.Enums;
using IndiaTechingClassLibray.Request;

namespace India_Teaching.DAL
{
    public class StudentbatchesDAL
    {
        string _StudentbatchesDAL = "StudentbatchesDAL";
        public int SaveStudentbatches(Studentbatches argStudentbatches)
          {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveStudentbatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argStudentbatches.Id);
                    sqlCommand.Parameters.AddWithValue("@StudentId", argStudentbatches.StudentId);
                    sqlCommand.Parameters.AddWithValue("@BatchId", argStudentbatches.BatchId);
                    sqlCommand.Parameters.AddWithValue("@IsActive", argStudentbatches.IsActive);
                    SqlParameter outputParam = sqlCommand.Parameters.Add("@IdToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argStudentbatches.Id = Convert.ToInt32(sqlCommand.Parameters["@IdToReturn"].Value);
                    rs = argStudentbatches.Id;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveStudentbatches", _StudentbatchesDAL, "Studentbatches", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }
        public Studentbatches GetStudentbatches(StudentbatchesRequest argStudentbatchesRequest)
        {
            Studentbatches studentbatches = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetStudentbatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argStudentbatchesRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            studentbatches = new Studentbatches();
                            studentbatches.Student = new Student();
                            studentbatches.Batches = new Batches();
                            studentbatches.Subject = new Subject();
                            studentbatches.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            studentbatches.BatchId = Convert.ToInt32(sqlDataReader["BatchId"]);
                            studentbatches.Student = new StudentDAL().GetStudent(new StudentRequest() { Id = Convert.ToInt32(sqlDataReader["StudentId"]) });
                            studentbatches.Subject.SubjectName = sqlDataReader["SubjectName"].ToString();
                            studentbatches.Batches.BatchName = sqlDataReader["BatchName"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudentbatches", _StudentbatchesDAL, "Studentbatches", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return studentbatches;
        }

        public List<Studentbatches> GetStudentbatchesList(StudentbatchesRequest argStudentbatchesRequest)
        {
            List<Studentbatches> studentbatchesList = null;
            Studentbatches studentbatches = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetStudentbatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@batchId", argStudentbatchesRequest.BatchId);
                    sqlCommand.Parameters.AddWithValue("@firtName", argStudentbatchesRequest.FirstName);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        studentbatchesList = new List<Studentbatches>();
                        while (sqlDataReader.Read())
                        {
                            studentbatches = new Studentbatches();
                            studentbatches.Student = new Student();
                            studentbatches.Batches = new Batches();
                            studentbatches.Subject = new Subject();
                            studentbatches.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            studentbatches.BatchId = Convert.ToInt32(sqlDataReader["BatchId"]);
                            studentbatches.Student = new StudentDAL().GetStudent(new StudentRequest() { Id = Convert.ToInt32(sqlDataReader["StudentId"]) });
                            studentbatches.Subject.SubjectName = sqlDataReader["SubjectName"].ToString();
                            studentbatches.Batches.BatchName = sqlDataReader["BatchName"].ToString();
                            studentbatchesList.Add(studentbatches);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudentbatchesList", _StudentbatchesDAL, "Studentbatches", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return studentbatchesList;
        }

        public List<Student> GetStudentsBySearch(string searchTerm)
        {
            List<Student> studentList = new List<Student>();
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    string query = "SELECT * FROM Students WHERE FirstName LIKE '%' + @SearchTerm + '%' OR LastName LIKE '%' + @SearchTerm + '%'";
                    sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Student student = new Student
                        {
                            Id = Convert.ToInt32(sqlDataReader["Id"]),
                            FirstName = sqlDataReader["FirstName"].ToString(),
                            LastName = sqlDataReader["LastName"].ToString(),
                            Class = sqlDataReader["Class"].ToString(),
                            // Country = sqlDataReader["Country"].ToString(),
                            Country = (EnumCountry)Convert.ToInt32(sqlDataReader["Country"]),
                            School = sqlDataReader["School"].ToString(),
                            FatherName = sqlDataReader["FatherName"].ToString(),
                            MotherName = sqlDataReader["MotherName"].ToString()
                        };
                        studentList.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudentsBySearch", _StudentbatchesDAL, "Studentbatches", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }

            return studentList;
        }

        public int EnrollStudentInBatch(int batchId, int studentId)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString);
                connection.Open();

                sqlCommand = new SqlCommand("SaveStudentbatches", connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@BatchId", batchId));
                sqlCommand.Parameters.Add(new SqlParameter("@StudentId", studentId));
                SqlParameter outputParam = sqlCommand.Parameters.Add("@IdToReturn", SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;
                sqlCommand.ExecuteNonQuery();
                rs = Convert.ToInt32(outputParam.Value);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("EnrollStudentInBatch", _StudentbatchesDAL, "Studentbatches", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                if (sqlCommand != null)
                {
                    sqlCommand.Dispose();
                }
            }
            return rs;
        }


        public bool Delete(StudentbatchesRequest argStudentbatchesRequest)
        {
            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("deletestudentbatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argStudentbatchesRequest.Id);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("Delete", _StudentbatchesDAL, "Studentbatches", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
    }
}











