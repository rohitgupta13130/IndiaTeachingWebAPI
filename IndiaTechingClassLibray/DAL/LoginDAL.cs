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
    public class LoginDAL
    {
        string _LoginDAL = "LoginDAL";
        public Users GetUsers(LoginRequest argUsers)
        {
            Users users = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetUsers", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@userName", argUsers.UserName);
                    sqlCommand.Parameters.AddWithValue("@userPassword", argUsers.UserPassword);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        users = new Users();
                        while (sqlDataReader.Read())
                        {
                            users.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                            users.UserName = sqlDataReader["UserName"].ToString();
                            users.UserPassword = sqlDataReader["UserPassword"].ToString();
                            users.UserType = Convert.ToInt32(sqlDataReader["UserType"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetUsers", _LoginDAL, "Users", ex.Message, DateTime.Now.ToString());
            }
            return users;
        }

        public bool SaveUserSession(int argUserId, Guid argGuid)
        {
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("InsertIntoUserSession", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", argUserId);
                    sqlCommand.Parameters.AddWithValue("@SessionId ", argGuid);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveUserSession", _LoginDAL, "Users", ex.Message, DateTime.Now.ToString());
            }
            return false;
        }

        public bool IsAuthenticated(int argUserId,Guid guid)
        {
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("ValidateUser", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@userId", argUserId);
                    sqlCommand.Parameters.AddWithValue("@SessionId", guid);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            int rs= Convert.ToInt32(sqlDataReader["IsAuthenticate"]);
                            if (rs == 1)
                                return false;
                            else
                                return false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                new LogsDAL().SaveLogs("IsAuthenticated", _LoginDAL, "Users", ex.Message, DateTime.Now.ToString());
            }
            return false;
        }
    }
}