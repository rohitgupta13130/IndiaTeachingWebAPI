using India_Teaching.Models;
using India_Teaching.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.SqlServer;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Models;

namespace India_Teaching.DAL
{
    public class TeacherDAL
    {
        string _TeacherDAL = "TeacherDAL";
        public int SaveTeacherPost(Teacher argTeacher, HttpPostedFileBase file, HttpPostedFileBase videoFile)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveTeacher", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@TeacherID", argTeacher.TeacherID);
                    sqlCommand.Parameters.AddWithValue("@Fullname", argTeacher.Fullname);
                    sqlCommand.Parameters.AddWithValue("@DateofBirth", argTeacher.DateofBirth);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", argTeacher.MobileNumber);
                    sqlCommand.Parameters.AddWithValue("@Address", argTeacher.Address);
                    sqlCommand.Parameters.AddWithValue("@Qualification", argTeacher.Qualification);
                    sqlCommand.Parameters.AddWithValue("@Married", argTeacher.Married);
                    sqlCommand.Parameters.AddWithValue("@ProfileLink", argTeacher.ProfileLink);
                    sqlCommand.Parameters.AddWithValue("@VideoLink", argTeacher.VideoLink);
                    sqlCommand.Parameters.AddWithValue("@IsActive", argTeacher.IsActive);

                    string selectedSkillIds = string.Empty;
                    foreach (int i in argTeacher.SelectedSkillIds)
                    {
                        selectedSkillIds = selectedSkillIds + "," + i.ToString();

                    }
                    selectedSkillIds = selectedSkillIds.Substring(1);

                    sqlCommand.Parameters.AddWithValue("@SkillId", selectedSkillIds);

                    SqlParameter outputParam = sqlCommand.Parameters.Add("@TeacherIdToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argTeacher.TeacherID = Convert.ToInt32(sqlCommand.Parameters["@TeacherIdToReturn"].Value);
                    rs = argTeacher.TeacherID;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveTeacher",_TeacherDAL,"Teacher",ex.Message,DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }
        public Teacher GetTeacher(TeacherRequest argTeacherRequest)
        {
            Teacher teacher = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetTeacher", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@TeacherID", argTeacherRequest.TeacherID);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            teacher = new Teacher();
                            teacher.TeacherID = Convert.ToInt32(sqlDataReader["TeacherId"]);
                            teacher.Fullname = sqlDataReader["Fullname"].ToString();
                            teacher.DateofBirth = Convert.ToDateTime(sqlDataReader["DateofBirth"]);
                            teacher.MobileNumber = sqlDataReader["MobileNumber"].ToString();
                            teacher.Address = sqlDataReader["Address"].ToString();
                            teacher.Qualification = sqlDataReader["Qualification"].ToString();
                            teacher.Married = sqlDataReader["Married"].ToString();
                            //teacher.ProfileLink = Constants.Constants.ProfilePicPath + sqlDataReader["ProfileLink"].ToString();
                            //teacher.VideoLink = Constants.Constants.VideoLinkPath + sqlDataReader["VideoLink"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetTeacher", _TeacherDAL, "Teacher", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return teacher;
        }
        public List<Teacher> GetTeacherList(TeacherRequest argTeacherRequest)
        {
            List<Teacher> teacherList = null;
            Teacher teacher = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetTeacher", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Fullname", argTeacherRequest.Fullname);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        teacherList = new List<Teacher>();
                        while (sqlDataReader.Read())
                        {
                            teacher = new Teacher();
                            teacher.TeacherID = Convert.ToInt32(sqlDataReader["TeacherId"]);
                            teacher.Fullname = sqlDataReader["Fullname"].ToString();
                            teacher.DateofBirth = Convert.ToDateTime(sqlDataReader["DateofBirth"]);
                            teacher.MobileNumber = sqlDataReader["MobileNumber"].ToString();
                            teacher.Address = sqlDataReader["Address"].ToString();
                            teacher.Qualification = sqlDataReader["Qualification"].ToString();
                            teacher.Married = sqlDataReader["Married"].ToString();
                            //teacher.ProfileLink = Constants.Constants.ProfilePicPath + sqlDataReader["ProfileLink"].ToString();
                            //teacher.VideoLink = Constants.Constants.VideoLinkPath + sqlDataReader["VideoLink"].ToString();

                            teacherList.Add(teacher);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetTeacherList", _TeacherDAL, "Teacher", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return teacherList;
        }
        public List<Skill> GetSkillsByTeacherId(int argTeacherId)
        {
            List<Skill> skills = null;
            Skill skill = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetSkillNameByTeacherId", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@teacherId", argTeacherId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        skills = new List<Skill>();
                        while (sqlDataReader.Read())
                        {
                            skill = new Skill();
                            skill.SkillName = sqlDataReader["SkillName"].ToString();
                            skill.SkillId = Convert.ToInt32(sqlDataReader["SkillId"]);
                            skills.Add(skill);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetSkillsByTeacherId", _TeacherDAL, "Teacher", ex.Message, DateTime.Now.ToString());
            }
            return skills;
        }

        public List<int> GetTeacherBySearch(string teacherSearch)
        {
            List<int> teacherIds = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            string mainconn = ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetStudentIdForSearch", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Fullname", teacherSearch);

                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        teacherIds = new List<int>();
                        while (sqlDataReader.Read())
                        {
                            if (int.TryParse(sqlDataReader["TeacherID"].ToString(), out int ignoreMe))
                            {
                                teacherIds.Add(Convert.ToInt32(sqlDataReader["TeacherID"]));
                            }
                            else
                            {
                                teacherIds.Add(0);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions
                // You can log the exception and/or rethrow it
                throw new Exception("An error occurred while accessing the database.", sqlEx);
            }
            catch (Exception ex)
            {
                // Handle other types of exceptions
                // You can log the exception and/or rethrow it
                throw new Exception("An error occurred while processing your request.", ex);
            }

            return teacherIds;
        }

        public bool DeleteTeacher(TeacherRequest argTeacherRequest)
        {

            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {

                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteTeacher", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@teacherId", argTeacherRequest.TeacherID);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    isSuccess = true;



                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("DeleteTeacher", _TeacherDAL, "Teacher", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();

            }

            return isSuccess;

        }

    }
}