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
    public class YearsDAL
    {
        string _YearsDAL = "YearsDAL";
        public List<Years> GetYears(YearsRequest argYearsRequest)
        {
            List<Years> yearlist = null;
            Years years = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetYears", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argYearsRequest.Id);
                    sqlCommand.Parameters.AddWithValue("@YearName", argYearsRequest.YearName);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        yearlist = new List<Years>();
                        while (sqlDataReader.Read())
                        {
                            years = new Years();
                            years.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            years.YearName = sqlDataReader["YearName"].ToString();
                            yearlist.Add(years);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetYears", _YearsDAL, "Years", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return yearlist;
        }
    }
}