using India_Teaching.Models;
using India_Teaching.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace India_Teaching.DAL
{
    public class feeBatchesDAL
    {

        public int SaveFeeBatches(FeeBatches argFeeBatches)
        {
            int rs = 0;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveFeeBatches", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argFeeBatches.Id);
                    sqlCommand.Parameters.AddWithValue("@FeeId", argFeeBatches.FeeId);
                    sqlCommand.Parameters.AddWithValue("@BatchId", argFeeBatches.batchId);
                    sqlCommand.Parameters.AddWithValue("@IsActive", argFeeBatches.IsActive);

                    SqlParameter outputParam = sqlCommand.Parameters.Add("@IdToReturn", System.Data.SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argFeeBatches.Id = Convert.ToInt32(sqlCommand.Parameters["@IdToReturn"].Value);
                    rs = argFeeBatches.Id;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConnection.Close();
            }
            return rs;
        }




        public List<FeeBatches> GetFeeBatchList(FeeBatchesRequest argBatchesRequest)
        {
            List<FeeBatches> feeBatchesList = null;
            FeeBatches feeBatches = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("getfeebatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@batchid", argBatchesRequest.batchId);
                    sqlCommand.Parameters.AddWithValue("@FeeId", argBatchesRequest.FeeId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        feeBatchesList = new List<FeeBatches>();
                        while (sqlDataReader.Read())
                        {

                            feeBatches = new FeeBatches();
                            feeBatches.Id = Convert.ToInt32(sqlDataReader["id"]);
                            feeBatches.FeeId = Convert.ToInt32(sqlDataReader["FeeId"]);
                            feeBatches.batchId = Convert.ToInt32(sqlDataReader["BatchId"]);
                            feeBatches.BatchName = sqlDataReader["BatchName"].ToString();
                            feeBatches.FeeAmount = Convert.ToInt32(sqlDataReader["FeeAmount"]);
                            feeBatchesList.Add(feeBatches);
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
            return feeBatchesList;
        }

        public FeeBatches GetFeeBatch(FeeBatchesRequest argBatchesRequest)
        {
            FeeBatches feeBatches = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("getfeebatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argBatchesRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            feeBatches = new FeeBatches();
                            feeBatches.Id = Convert.ToInt32(sqlDataReader["id"]);
                            feeBatches.FeeId = Convert.ToInt32(sqlDataReader["FeeId"]);
                            feeBatches.batchId = Convert.ToInt32(sqlDataReader["BatchId"]);
                            feeBatches.BatchName = sqlDataReader["BatchName"].ToString();
                            feeBatches.FeeAmount = Convert.ToInt32(sqlDataReader["FeeAmount"]);
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
            return feeBatches;
        }

        public bool DeleteFeeBatches(FeeBatchesRequest argFeeBatchesRequest)
        {
            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteFeeBatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argFeeBatchesRequest.Id);

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


        public List<FeeBatches> GetFeeBatchesList(FeeBatchesRequest argBatchesRequest)
        {
            List<FeeBatches> feeBatchesList = null;
            FeeBatches feeBatches = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetFeeAmountByBatchId", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@batchid", argBatchesRequest.batchId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        feeBatchesList = new List<FeeBatches>();
                        while (sqlDataReader.Read())
                        {
                            feeBatches = new FeeBatches();
                            feeBatches.Id = Convert.ToInt32(sqlDataReader["FeeBatchesId"]);
                            feeBatches.FeeId = Convert.ToInt32(sqlDataReader["FeeId"]);
                            feeBatches.batchId = Convert.ToInt32(sqlDataReader["BatchId"]);
                            feeBatches.FeeAmount = Convert.ToInt32(sqlDataReader["FeeAmount"]);
                            feeBatchesList.Add(feeBatches);
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
            return feeBatchesList;
        }

        public FeeBatches GetFeeBatches(FeeBatchesRequest argBatchesRequest)
        {
            FeeBatches feeBatches = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetFeeAmountByBatchId", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@batchid", argBatchesRequest.batchId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            feeBatches = new FeeBatches();
                            feeBatches.Id = Convert.ToInt32(sqlDataReader["FeeBatchesId"]);
                            feeBatches.FeeId = Convert.ToInt32(sqlDataReader["FeeId"]);
                            feeBatches.batchId = Convert.ToInt32(sqlDataReader["BatchId"]);
                            feeBatches.FeeAmount = Convert.ToInt32(sqlDataReader["FeeAmount"]);
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
            return feeBatches;
        }






    }
}