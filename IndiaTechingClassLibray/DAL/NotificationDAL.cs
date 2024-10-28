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
    public class NotificationDAL
    {

        public int SaveNotification(Notification argNotification)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveNotification", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argNotification.Id);
                    sqlCommand.Parameters.AddWithValue("@TeacherId", argNotification.TeacherId);
                    sqlCommand.Parameters.AddWithValue("@BatchId", argNotification.BatchId);
                    sqlCommand.Parameters.AddWithValue("@NotificationText", argNotification.NotificationText);
                    sqlCommand.Parameters.AddWithValue("@CreatedDateTime", argNotification.CreatedDateTime);
                    sqlCommand.Parameters.AddWithValue("@IsActive", argNotification.IsActive);
                    SqlParameter outputParam = sqlCommand.Parameters.Add("@IdToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argNotification.Id = Convert.ToInt32(sqlCommand.Parameters["@IdToReturn"].Value);
                    rs = argNotification.Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return rs;
        }

        public List<Student> GetStudentListForNotify(int argBatchId, int argTeacherId)
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
                    sqlCommand = new SqlCommand("GetStudentForNotify", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@batchId", argBatchId);
                    sqlCommand.Parameters.AddWithValue("@teacherId", argTeacherId);
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
                            student.Country = sqlDataReader["Country"].ToString();
                            student.School = sqlDataReader["School"].ToString();
                            student.FatherName = sqlDataReader["FatherName"].ToString();
                            student.MotherName = sqlDataReader["MotherName"].ToString();
                            student.Enrollmentdate = Convert.ToDateTime(sqlDataReader["Enrollmentdate"]);
                            student.ClassName = sqlDataReader["Class"].ToString();
                            //student.Email = sqlDataReader["Email"].ToString();

                            studentList.Add(student);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudentListForNotify", "NotificationDAL", "DAL", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return studentList;
        }

        public Notification GetNotification(NotificationRequest argNotificationRequest)
        {
            Notification notification = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetNotification", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argNotificationRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            notification = new Notification();
                            notification.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            notification.TeacherId = Convert.ToInt32(sqlDataReader["TeacherId"]);
                            notification.NotificationText = sqlDataReader["NotificationText"].ToString();
                            notification.CreatedDateTime = Convert.ToDateTime(sqlDataReader["CreatedDateTime"]);
                            notification.BatchId= Convert.ToInt32(sqlDataReader["BatchId"]);
                            notification.BatchName = sqlDataReader["BatchName"].ToString();
                            notification.TeacherName = sqlDataReader["Fullname"].ToString();
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
            return notification;
        }

        public List<Notification> GetNotificationList(NotificationRequest argNotificationRequest)
        {
            List<Notification> lstnotification = null;
            Notification notification = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetNotification", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@TeacherId", argNotificationRequest.TeacherId);
                    sqlCommand.Parameters.AddWithValue("@BatchId", argNotificationRequest.BatchId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        lstnotification = new List<Notification>();
                        while (sqlDataReader.Read())
                        {
                            notification = new Notification();
                            notification.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            notification.TeacherId = Convert.ToInt32(sqlDataReader["TeacherId"]);
                            notification.NotificationText = sqlDataReader["NotificationText"].ToString();
                            notification.CreatedDateTime = Convert.ToDateTime(sqlDataReader["CreatedDateTime"]);
                            notification.BatchId = Convert.ToInt32(sqlDataReader["BatchId"]);
                            notification.BatchName = sqlDataReader["BatchName"].ToString();
                            notification.TeacherName = sqlDataReader["Fullname"].ToString();
                            lstnotification.Add(notification);
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
            return lstnotification;
        }

        public bool DeleteNotification(NotificationRequest argNotificationRequest)
        {

            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {

                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteNotification", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argNotificationRequest.Id);

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
                connection.Close();

            }

            return isSuccess;

        }
    }
}