using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;


namespace India_Teaching.DAL
{
    public class BatchesDAL
    {
        string _BatchesDAL = "BatchesDAL";
        public int SaveBatches(Batches argBatches)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveBatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@batchId", argBatches.Id);
                    sqlCommand.Parameters.AddWithValue("@batchName", argBatches.BatchName);
                    sqlCommand.Parameters.AddWithValue("@teacherId", argBatches.TeacherId);
                    sqlCommand.Parameters.AddWithValue("@subjectId", argBatches.SubjectId);
                    sqlCommand.Parameters.AddWithValue("@batchStartTime", argBatches.BatchStartTime);
                    sqlCommand.Parameters.AddWithValue("@batchEndTime", argBatches.BatchEndTime);
                    sqlCommand.Parameters.AddWithValue("@IsActive", argBatches.IsActive);

                    SqlParameter outputParam = sqlCommand.Parameters.Add("@batchIdToReturn", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argBatches.Id = Convert.ToInt32(sqlCommand.Parameters["@batchIdToReturn"].Value);
                    rs = argBatches.Id;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveBatches", _BatchesDAL, "Batches", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }

        public Batches GetBatches(BatchesRequest argBatchesRequest)
        {
            Batches batches = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetBatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@batchId", argBatchesRequest.Id);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            batches = new Batches();
                            //batches.Teacher = new Teacher();
                            //batches.Subject = new Subject();
                            batches.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            batches.BatchName = sqlDataReader["BatchName"].ToString();
                            //batches.Teacher.Fullname = sqlDataReader["Fullname"].ToString();
                            //batches.Subject.SubjectName = sqlDataReader["SubjectName"].ToString();
                            batches.TeacherId = Convert.ToInt32(sqlDataReader["TeacherId"]);
                            batches.SubjectId = Convert.ToInt32(sqlDataReader["SubjectId"]);
                            batches.BatchStartTime = Convert.ToDateTime(sqlDataReader["BatchStartTime"]);
                            batches.BatchEndTime = Convert.ToDateTime(sqlDataReader["BatchEndTime"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetBatches", _BatchesDAL, "Batches", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                
            }
            return batches;
        }

        public List<Batches> GetBatchesList(BatchesRequest argBatchesRequest)
        {
            List<Batches> batchesList = null;
            Batches batches = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetBatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BatchName", argBatchesRequest.BatchName);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        batchesList = new List<Batches>();
                        while (sqlDataReader.Read())
                        {
                            batches = new Batches();
                            //batches.Teacher = new Teacher();
                            //batches.Subject = new Subject();
                            batches.Id = Convert.ToInt32(sqlDataReader["Id"]);
                            batches.BatchName = sqlDataReader["BatchName"].ToString();
                            //batches.Teacher.Fullname = sqlDataReader["Fullname"].ToString();
                            //batches.Subject.SubjectName = sqlDataReader["SubjectName"].ToString();
                            batches.TeacherId = Convert.ToInt32(sqlDataReader["TeacherId"]);
                            batches.SubjectId = Convert.ToInt32(sqlDataReader["SubjectId"]);
                            batches.BatchStartTime = Convert.ToDateTime(sqlDataReader["BatchStartTime"]);
                            batches.BatchEndTime = Convert.ToDateTime(sqlDataReader["BatchEndTime"]);
                            batchesList.Add(batches);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetBatchesList", _BatchesDAL, "Batches", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return batchesList;
        }

        public bool DeleteBatches(BatchesRequest argBatchesRequest)
        {

            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {

                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteBatches", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argBatchesRequest.Id);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    isSuccess = true;



                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("DeleteBatches", _BatchesDAL, "Batches", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();

            }

            return isSuccess;

        }


        //public List<Student> GetStudentBySearch(string studentSearch)
        //{
        //    List<Student> students = new List<Student>();

        //    string mainconn = ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString;
        //    using (SqlConnection sqlConnection = new SqlConnection(mainconn))
        //    {
        //        string sqlquery = "SELECT Id, FirstName, LastName, Class, Country, School, FatherName, MotherName FROM [dbo].[Student] WHERE FirstName LIKE '%' + @SearchTerm + '%' OR LastName LIKE '%' + @SearchTerm + '%'";
        //        SqlCommand sqlCommand = new SqlCommand(sqlquery, sqlConnection);

        //        sqlCommand.Parameters.AddWithValue("@SearchTerm", "%" + studentSearch + "%");

        //        sqlConnection.Open();
        //        SqlDataAdapter sqlData = new SqlDataAdapter(sqlCommand);
        //        DataSet dataSet = new DataSet();
        //        sqlData.Fill(dataSet);

        //        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
        //        {
        //            students.Add(new Student
        //            {
        //                Id = Convert.ToInt32(dataRow["Id"]),
        //                FirstName = dataRow["FirstName"].ToString(),
        //                LastName = dataRow["LastName"].ToString(),
        //                Class = dataRow["Class"].ToString(),
        //                Country = dataRow["Country"].ToString(),
        //                School = dataRow["School"].ToString(),
        //                FatherName = dataRow["FatherName"].ToString(),
        //                MotherName = dataRow["MotherName"].ToString()
        //            });
        //        }
        //    }

        //    return students;
        //}

    }
}