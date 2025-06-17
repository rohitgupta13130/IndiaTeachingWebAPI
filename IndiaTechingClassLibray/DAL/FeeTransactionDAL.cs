using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace India_Teaching.DAL
{
    public class FeeTransactionDAL
    {
        string _FeeTransacionDAL = "FeeTransactionDAL";
        public int SaveFeeTransaction(FeeTransaction argFeeTransaction)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveFeeTransaction", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@FeetransactionId", argFeeTransaction.FeetransactionId);
                    sqlCommand.Parameters.AddWithValue("@MonthId", argFeeTransaction.MonthId);
                    sqlCommand.Parameters.AddWithValue("@FeeId", argFeeTransaction.FeeId);
                    sqlCommand.Parameters.AddWithValue("@StudentId", argFeeTransaction.StudentId);
                    sqlCommand.Parameters.AddWithValue("@YearId", argFeeTransaction.Year);
                    sqlCommand.Parameters.AddWithValue("@PaidAmount", argFeeTransaction.PaidAmount);
                    sqlCommand.Parameters.AddWithValue("@BalanceAmount", argFeeTransaction.BalanceAmount);
                    sqlCommand.Parameters.AddWithValue("@PreBalanceAmount", argFeeTransaction.PreBalanceAmount);
                    sqlCommand.Parameters.AddWithValue("@EntryDate", argFeeTransaction.EntryDate);
                    sqlCommand.Parameters.AddWithValue("@UserId", argFeeTransaction.UserId);
                    sqlCommand.Parameters.AddWithValue("@BatchId", argFeeTransaction.BatchId);
                    SqlParameter outputParam = sqlCommand.Parameters.Add("@FeetransactionIdToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argFeeTransaction.FeetransactionId = Convert.ToInt32(sqlCommand.Parameters["@FeetransactionIdToReturn"].Value);
                    rs = argFeeTransaction.FeetransactionId;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveFeeTransaction", _FeeTransacionDAL, "FeeTransaction", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }
        public FeeTransaction GetFeeTransaction(FeeTransactionRequest argFeeTransactionRequest)
        {
            FeeTransaction feeTransaction = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetFeeTransaction", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@FeetransactionId", argFeeTransactionRequest.FeetransactionId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            feeTransaction = new FeeTransaction();
                            feeTransaction.FeetransactionId = Convert.ToInt32(sqlDataReader["FeetransactionId"]);
                            feeTransaction.MonthId = Convert.ToInt32(sqlDataReader["MonthId"]);
                            feeTransaction.FeeId = Convert.ToInt32(sqlDataReader["FeeId"]);
                            feeTransaction.StudentId = Convert.ToInt32(sqlDataReader["StudentId"]);



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetFeeTransaction", _FeeTransacionDAL, "FeeTransaction", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return feeTransaction;
        }
        public List<FeeTransaction> GetFeeTransactionList(FeeTransactionRequest argFeeTransactionRequest)
        {
            List<FeeTransaction> feeTransactionList = null;
            FeeTransaction feeTransaction = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetFeeTransaction", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    //sqlCommand.Parameters.AddWithValue("@StudentId", argFeeTransactionRequest.StudentId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        feeTransactionList = new List<FeeTransaction>();
                        while (sqlDataReader.Read())
                        {
                            feeTransaction = new FeeTransaction();
                            feeTransaction.FeetransactionId = Convert.ToInt32(sqlDataReader["FeetransactionId"]);
                            feeTransaction.MonthId = Convert.ToInt32(sqlDataReader["MonthId"]);
                            feeTransaction.FeeId = Convert.ToInt32(sqlDataReader["FeeId"]);
                            feeTransaction.StudentId = Convert.ToInt32(sqlDataReader["StudentId"]);
                            feeTransaction.Year = Convert.ToInt32(sqlDataReader["YearId"]);
                            feeTransaction.PaidAmount = Convert.ToInt32(sqlDataReader["PaidAmount"]);
                            feeTransaction.BalanceAmount = Convert.ToInt32(sqlDataReader["BalanceAmount"]);
                            feeTransaction.PreBalanceAmount = Convert.ToInt32(sqlDataReader["PreBalanceAmount"]);
                            feeTransaction.EntryDate = Convert.ToDateTime(sqlDataReader["EntryDate"]);
                            feeTransaction.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                            feeTransactionList.Add(feeTransaction);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetFeeTransactionList", _FeeTransacionDAL, "FeeTransaction", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return feeTransactionList;
        }

        public List<FeeTransaction> GetFeeTransactionForStudent(int argStudentId, int argFeeTransactionId)
        {
            List<FeeTransaction> feeTransactionList = null;
            FeeTransaction feeTransaction = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetFeesByStudentId", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@studentId", argStudentId);
                    sqlCommand.Parameters.AddWithValue("@FeetransactionId", argFeeTransactionId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        feeTransactionList = new List<FeeTransaction>();
                        while (sqlDataReader.Read())
                        {
                            feeTransaction = new FeeTransaction();
                            feeTransaction.FeetransactionId = Convert.ToInt32(sqlDataReader["FeetransactionId"]);
                            feeTransaction.StudentId = Convert.ToInt32(sqlDataReader["StudentId"]);
                            feeTransaction.MonthsName = sqlDataReader["MonthsName"].ToString();
                            feeTransaction.PreBalanceAmount = Convert.ToInt32(sqlDataReader["PreBalanceAmount"]);
                            feeTransaction.FeeAmount = Convert.ToInt32(sqlDataReader["FeeAmount"]);
                            feeTransaction.PaidAmount = Convert.ToInt32(sqlDataReader["PaidAmount"]);
                            feeTransaction.BalanceAmount = Convert.ToInt32(sqlDataReader["BalanceAmount"]);
                            feeTransaction.EntryDate = Convert.ToDateTime(sqlDataReader["EntryDate"]);
                            feeTransaction.UserName = sqlDataReader["UserName"].ToString();
                            feeTransactionList.Add(feeTransaction);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetFeeTransactionForStudent", _FeeTransacionDAL, "FeeTransaction", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return feeTransactionList;
        }

        public List<FeeTransaction> GetDataForReceipts()
        {
            List<FeeTransaction> feeTransactionList = null;
            FeeTransaction feeTransaction = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetDataForReceipts", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        feeTransactionList = new List<FeeTransaction>();
                        while (sqlDataReader.Read())
                        {
                            feeTransaction = new FeeTransaction();
                            feeTransaction.FeetransactionId = Convert.ToInt32(sqlDataReader["FeetransactionId"]);
                            feeTransaction.StudentId = Convert.ToInt32(sqlDataReader["StudentId"]);
                            feeTransaction.MonthsName = sqlDataReader["MonthsName"].ToString();
                            feeTransaction.PreBalanceAmount = Convert.ToInt32(sqlDataReader["PreBalanceAmount"]);
                            feeTransaction.FeeAmount = Convert.ToInt32(sqlDataReader["FeeAmount"]);
                            feeTransaction.PaidAmount = Convert.ToInt32(sqlDataReader["PaidAmount"]);
                            feeTransaction.BalanceAmount = Convert.ToInt32(sqlDataReader["BalanceAmount"]);
                            feeTransaction.EntryDate = Convert.ToDateTime(sqlDataReader["EntryDate"]);
                            feeTransaction.UserName = sqlDataReader["UserName"].ToString();
                            feeTransaction.BatchId = Convert.ToInt32(sqlDataReader["batchId"]);
                            feeTransactionList.Add(feeTransaction);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetDataForReceipts", _FeeTransacionDAL, "FeeTransaction", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return feeTransactionList;
        }


        
    }
}
