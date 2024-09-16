using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaTechingClassLibray.DAL
{
    public class LogsDAL
    {
        public int SaveLogs(string methodName, string className, string folderName, string errorMessage, string createdDate)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("Savelogs", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@MethodName", methodName);
                    sqlCommand.Parameters.AddWithValue("@ClassName", className);
                    sqlCommand.Parameters.AddWithValue("@FolderName", folderName);
                    sqlCommand.Parameters.AddWithValue("@ErrorMessage", errorMessage);
                    sqlCommand.Parameters.AddWithValue("@CreatedDate", createdDate);
                    SqlParameter outputParam = sqlCommand.Parameters.Add("@LogsIdToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    rs = Convert.ToInt32(outputParam.Value);
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
    }
}
