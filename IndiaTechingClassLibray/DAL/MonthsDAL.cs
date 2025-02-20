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
    public class MonthsDAL
    {
        string _MonthsDAL = "MonthsDAL";
        public List<Months> GetMonths(MonthsRequest argMonthsRequest)
        {
            List<Months> monthslist = null;
            Months months = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetMonths", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argMonthsRequest.Id);
                    sqlCommand.Parameters.AddWithValue("@MonthsName", argMonthsRequest.MonthsName);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        monthslist = new List<Months>();
                        while (sqlDataReader.Read())
                        {
                            months = new Months();
                            months.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            months.MonthsName = sqlDataReader["MonthsName"].ToString();
                            monthslist.Add(months);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetMonths", _MonthsDAL, "Months", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return monthslist;
        }
    }
}