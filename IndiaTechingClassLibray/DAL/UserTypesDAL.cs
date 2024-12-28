using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace India_Teaching.DAL
{
    public class UserTypesDAL
    {
        string _UserTypesDAL = "UserTypesDAL";
        public List<UserTypes> GetUsers(UserTypesRequest argUserTypesRequest)
        {
            List<UserTypes> lst = null;
            UserTypes userTypes = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetUserTypes", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@userTypeId", argUserTypesRequest.UserTypeId);
                    sqlCommand.Parameters.AddWithValue("@userTypeName", argUserTypesRequest.UserTypeName);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        lst = new List<UserTypes>();
                        while (sqlDataReader.Read())
                        {
                            userTypes = new UserTypes();
                            userTypes.UserTypeId = Convert.ToInt32(sqlDataReader["Id"]);
                            userTypes.UserTypeName = sqlDataReader["TypeName"].ToString();
                            lst.Add(userTypes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetUsers", _UserTypesDAL, "Users", ex.Message, DateTime.Now.ToString());
            }
            return lst;
        }
    }
}