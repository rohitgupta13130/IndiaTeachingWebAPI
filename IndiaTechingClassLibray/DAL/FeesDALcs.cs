using India_Teaching.Models;
using India_Teaching.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace India_Teaching.DAL
{
    public class FeesDAL
    {
        public int SaveFees(Fees argFees)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlcommand = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlcommand = new SqlCommand("Savefees", connection);
                    sqlcommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlcommand.Parameters.AddWithValue("Id", argFees.Id);
                    sqlcommand.Parameters.AddWithValue("FeeAmount", argFees.FeeAmount);
                    sqlcommand.Parameters.AddWithValue("IsActive", argFees.IsActive);

                    SqlParameter outputParam = sqlcommand.Parameters.Add("@IdToReturn", System.Data.SqlDbType.Int);
                    outputParam.Direction = System.Data.ParameterDirection.Output;
                    connection.Open();
                    sqlcommand.ExecuteNonQuery();
                    argFees.Id = Convert.ToInt32(sqlcommand.Parameters["@IdToReturn"].Value);
                    rs = argFees.Id;
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

        public Fees GetFees(FeesRequest argFeesRequest)
        {
            Fees fees = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetFees", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argFeesRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            fees = new Fees();
                            fees.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            fees.FeeAmount = Convert.ToInt32(sqlDataReader["FeeAmount"]);
                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return fees;
        }

        public List<Fees> GetFeesList(FeesRequest argFeesRequest)
        {
            List<Fees> feesList = null;
            Fees fees = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetFees", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@FeeAmount", argFeesRequest.FeeAmount);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        feesList = new List<Fees>();
                        while (sqlDataReader.Read())
                        {
                            fees = new Fees();
                            fees.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            fees.FeeAmount = Convert.ToInt32(sqlDataReader["FeeAmount"]);
                            feesList.Add(fees);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return feesList;
        }

        public bool DeleteFees(FeesRequest argFeesRequest)
        {
            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteFees", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argFeesRequest.Id);

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