using India_Teaching.Models;
using IndiaTechingClassLibray.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace India_Teaching.DAL
{
    public class UserAccessDAL
    {
        string _UserTypesDAL = "UserTypesDAL";

        public List<UserAccess> GetUserAccess(int argUserType)
        {
            List<UserAccess> lst = null;
            UserAccess userAccess = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetMenusAndAccess", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserType", argUserType);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        lst = new List<UserAccess>();
                        while (sqlDataReader.Read())
                        {
                            userAccess = new UserAccess();
                            userAccess.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            userAccess.ControllerAction = sqlDataReader["ControllerAction"].ToString();
                            userAccess.Add = Convert.ToBoolean(sqlDataReader["AddAccess"]);
                            userAccess.Edit = Convert.ToBoolean(sqlDataReader["EditAccess"]);
                            userAccess.View = Convert.ToBoolean(sqlDataReader["ViewAccess"]);
                            userAccess.Search = Convert.ToBoolean(sqlDataReader["SearchAccess"]);
                            userAccess.MenuName = sqlDataReader["MenuName"].ToString();
                            userAccess.SpecialAccess = Convert.ToBoolean(sqlDataReader["SpecialAccess"]);
                            lst.Add(userAccess);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetUserAccess", _UserTypesDAL, "Users", ex.Message, DateTime.Now.ToString());
            }
            return lst;
        }
    }
}