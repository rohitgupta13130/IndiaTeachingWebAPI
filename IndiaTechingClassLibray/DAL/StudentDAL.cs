using India_Teaching.Enums;
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
    public class StudentDAL
    {
        string _StudentDAL = "StudentDAL";
        public int SaveStudent(Student argStudent)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveStudent", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argStudent.Id);
                    sqlCommand.Parameters.AddWithValue("@FirstName", argStudent.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", argStudent.LastName);
                    sqlCommand.Parameters.AddWithValue("@Class", Convert.ToInt32(argStudent.Class));
                    sqlCommand.Parameters.AddWithValue("@Country", argStudent.Country ?? (object)DBNull.Value);
                    //sqlCommand.Parameters.AddWithValue("@Country",argStudent.Country);
                    sqlCommand.Parameters.AddWithValue("@School", argStudent.School ?? (object)DBNull.Value);
                    //sqlCommand.Parameters.AddWithValue("@School", argStudent.School);
                    sqlCommand.Parameters.AddWithValue("@FatherName", argStudent.FatherName ?? (object)DBNull.Value);
                    //sqlCommand.Parameters.AddWithValue("@FatherName", argStudent.FatherName);
                    sqlCommand.Parameters.AddWithValue("@MotherName", argStudent.MotherName);
                    sqlCommand.Parameters.AddWithValue("@IsActive", argStudent.IsActive);
                    sqlCommand.Parameters.AddWithValue("@Email", argStudent.Email);


                    SqlParameter outputParam = sqlCommand.Parameters.Add("@IdToReturn", SqlDbType.Int, -1);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argStudent.Id = Convert.ToInt32(outputParam.Value);
                    rs = argStudent.Id;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveStudent", _StudentDAL, "Student", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }

        public Student GetStudent(StudentRequest argStudentRequest)
        {
            Student student = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetStudent", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", argStudentRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            student = new Student();
                            student.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            student.FirstName = sqlDataReader["FirstName"].ToString();
                            student.LastName = sqlDataReader["LastName"].ToString();
                            student.Class = sqlDataReader["Class"].ToString();
                            //student.Country = ((EnumCountry)Convert.ToInt32(sqlDataReader["Country"])).ToString();
                            student.Country = sqlDataReader["Country"] != DBNull.Value ? ((EnumCountry)Convert.ToInt32(sqlDataReader["Country"])).ToString() : null;
                            //student.Country = sqlDataReader["Country"].ToString();
                            student.School = sqlDataReader["School"] != DBNull.Value ? sqlDataReader["School"].ToString() : null;
                            //student.School = sqlDataReader["School"].ToString();
                            student.FatherName = sqlDataReader["FatherName"] != DBNull.Value ? sqlDataReader["FatherName"].ToString() : null;
                            //student.FatherName = sqlDataReader["FatherName"].ToString();
                            student.MotherName = sqlDataReader["MotherName"].ToString();
                            student.Enrollmentdate = Convert.ToDateTime(sqlDataReader["Enrollmentdate"]);
                            student.ClassName = sqlDataReader["ClassName"].ToString();
                            student.Email = sqlDataReader["Email"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudent", _StudentDAL, "Student", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return student;
        }

        public List<Student> GetStudentList(StudentRequest argStudentRequest)
        {
            List<Student> studentList = null;
            Student student = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetStudent", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@FirstName", argStudentRequest.FirstName);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        studentList = new List<Student>();
                        while (sqlDataReader.Read())
                        {
                            student = new Student();
                            student.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            student.FirstName = sqlDataReader["FirstName"].ToString();
                            student.LastName = sqlDataReader["LastName"].ToString();
                            student.Class = sqlDataReader["Class"].ToString();
                            //student.Country = ((EnumCountry)Convert.ToInt32(sqlDataReader["Country"])).ToString();
                            student.Country = sqlDataReader["Country"] != DBNull.Value ? ((EnumCountry)Convert.ToInt32(sqlDataReader["Country"])).ToString() : null;
                            //student.Country = sqlDataReader["Country"].ToString();
                            student.School = sqlDataReader["School"] != DBNull.Value ? sqlDataReader["School"].ToString() : null;
                            //student.School = sqlDataReader["School"].ToString();
                            student.FatherName = sqlDataReader["FatherName"] != DBNull.Value ? sqlDataReader["FatherName"].ToString() : null;
                            //student.FatherName = sqlDataReader["FatherName"].ToString();
                            student.MotherName = sqlDataReader["MotherName"].ToString();
                            student.Enrollmentdate = Convert.ToDateTime(sqlDataReader["Enrollmentdate"]);
                            student.ClassName = sqlDataReader["ClassName"].ToString();
                            student.Email = sqlDataReader["Email"].ToString();
                            studentList.Add(student);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudentList", _StudentDAL, "Student", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return studentList;
        }


        public bool DeleteStudent(StudentRequest argStudentRequest)
        {

            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {

                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteStudent", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argStudentRequest.Id);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    isSuccess = true;



                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("DeleteStudent", _StudentDAL, "Student", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();

            }

            return isSuccess;

        }

    }
}
