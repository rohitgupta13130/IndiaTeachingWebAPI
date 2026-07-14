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
using Serilog;

namespace India_Teaching.DAL
{
    public class TeacherDAL
    {
        string _TeacherDAL = "TeacherDAL";
        public int SaveTeacherPost(
    Teacher argTeacher,
    HttpPostedFile profileFile,
    HttpPostedFile videoFile)
        {
            int rs = 0;

            try
            {
                // Save Profile Image
                if (profileFile != null && profileFile.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid() +
                                      Path.GetExtension(profileFile.FileName);

                    string filePath = HttpContext.Current.Server.MapPath(
                        "~/Uploads/ProfileImages/" + fileName);

                    profileFile.SaveAs(filePath);

                    argTeacher.ProfileLink = "/Uploads/ProfileImages/" + fileName;
                }

                // Save Video
                if (videoFile != null && videoFile.ContentLength > 0)
                {
                    string videoName = Guid.NewGuid() +
                                       Path.GetExtension(videoFile.FileName);

                    string videoPath = HttpContext.Current.Server.MapPath(
                        "~/Uploads/Videos/" + videoName);

                    videoFile.SaveAs(videoPath);

                    argTeacher.VideoLink = "/Uploads/Videos/" + videoName;
                }

                using (SqlConnection connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SaveTeacher", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@TeacherID",argTeacher.TeacherID == 0 ? 0 : argTeacher.TeacherID);
                    sqlCommand.Parameters.AddWithValue("@Fullname", argTeacher.Fullname);
                    sqlCommand.Parameters.AddWithValue("@DateofBirth", argTeacher.DateofBirth);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", argTeacher.MobileNumber);
                    sqlCommand.Parameters.AddWithValue("@Address", argTeacher.Address);
                    sqlCommand.Parameters.AddWithValue("@Qualification", argTeacher.Qualification);
                    sqlCommand.Parameters.AddWithValue("@Married", (object)argTeacher.Married ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@ProfileLink", (object)argTeacher.ProfileLink ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@VideoLink", (object)argTeacher.VideoLink ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@IsActive", argTeacher.IsActive);
                    sqlCommand.Parameters.AddWithValue("@Share_Percentage", argTeacher.SharePercentage);
                    sqlCommand.Parameters.AddWithValue("@ClassTeacher", argTeacher.ClassTeacher);
                    string selectedSkillIds = argTeacher.SelectedSkillIds != null
                      ? string.Join(",", argTeacher.SelectedSkillIds) : "";
                    sqlCommand.Parameters.AddWithValue("@SkillId", selectedSkillIds);

                    SqlParameter outputParam = sqlCommand.Parameters.Add(
                        "@TeacherIdToReturn", SqlDbType.Int);

                    outputParam.Direction = ParameterDirection.Output;

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();

                    rs = Convert.ToInt32(outputParam.Value);
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs(
                    "SaveTeacher",
                    "_TeacherDAL",
                    "Teacher",
                    ex.Message,
                    DateTime.Now.ToString());
            }

            return rs;
        }

        public Teacher GetTeacher(TeacherRequest argTeacherRequest)
        {
            Log.Information("Entered GetTeacher method in TeacherDAL.");

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
                            teacher.TeacherID = Convert.ToInt32(sqlDataReader["TeacherID"]);
                            teacher.Fullname = sqlDataReader["Fullname"].ToString();
                            teacher.DateofBirth = Convert.ToDateTime(sqlDataReader["DateofBirth"]);
                            teacher.MobileNumber = sqlDataReader["MobileNumber"].ToString();
                            teacher.Address = sqlDataReader["Address"].ToString();
                            teacher.Qualification = sqlDataReader["Qualification"].ToString();
                            //teacher.Married = ((EnumYesNo)Convert.ToInt32(sqlDataReader["Married"])).ToString();
                            teacher.Married = Convert.ToString(sqlDataReader["Married"]);
                            teacher.ProfileLink =  sqlDataReader["ProfileLink"].ToString();
                            //teacher.ProfileLink = sqlDataReader["ProfileLink"] != DBNull.Value ? Constants.Constants.ProfilePicPath + sqlDataReader["ProfileLink"].ToString() : null;
                            teacher.VideoLink =  sqlDataReader["VideoLink"].ToString();
                            teacher.SharePercentage = sqlDataReader["Share_Percentage"] is int sharePercentage ? sharePercentage : default;
                            teacher.SkillNames = sqlDataReader["SkillNames"] != DBNull.Value ? sqlDataReader["SkillNames"].ToString()
    : "";
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
            Log.Information("Entered GetTeacherList method in TeacherDAL.");

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
                            teacher.TeacherID = Convert.ToInt32(sqlDataReader["TeacherID"]);
                            teacher.Fullname = sqlDataReader["Fullname"].ToString();
                            teacher.DateofBirth = Convert.ToDateTime(sqlDataReader["DateofBirth"]);
                            teacher.MobileNumber = sqlDataReader["MobileNumber"].ToString();
                            teacher.Address = sqlDataReader["Address"].ToString();
                            teacher.Qualification = sqlDataReader["Qualification"].ToString();
                            //teacher.Married = ((EnumYesNo)Convert.ToInt32(sqlDataReader["Married"])).ToString();
                            teacher.Married = Convert.ToString(sqlDataReader["Married"]);
                            teacher.ProfileLink =  sqlDataReader["ProfileLink"].ToString();
                            //teacher.ProfileLink = sqlDataReader["ProfileLink"] != DBNull.Value ? Constants.Constants.ProfilePicPath + sqlDataReader["ProfileLink"].ToString() : null;
                            teacher.VideoLink = sqlDataReader["VideoLink"].ToString();
                            teacher.SharePercentage = sqlDataReader["Share_Percentage"] is int sharePercentage ? sharePercentage : default;
                            teacher.SkillNames = sqlDataReader["SkillNames"] != DBNull.Value ? sqlDataReader["SkillNames"].ToString(): "";

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
            Log.Information("Entered GetSkillsByTeacherId method in TeacherDAL.");

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
            Log.Information("Entered GetTeacherBySearch method in TeacherDAL.");

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
            Log.Information("Entered DeleteTeacher method in TeacherDAL.");

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