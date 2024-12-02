using India_Teaching.Models;
using India_Teaching.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using India_Teaching.Enums;
using IndiaTechingClassLibray.DAL;

namespace India_Teaching.DAL
{
    public class ClassesDAL
    {
        string _ClassesDAL = "ClassesDAL";



        public int SaveClass(Classes argClasses)
        {
            int rs = 0;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            try
            {
                using (sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveClass", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argClasses.ClassId);
                    sqlCommand.Parameters.AddWithValue("@ClassName", argClasses.ClassName);
                    sqlCommand.Parameters.AddWithValue("@Section", argClasses.SectionId);


                    SqlParameter outputParam = sqlCommand.Parameters.Add("@IdToReturn", System.Data.SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    argClasses.ClassId = Convert.ToInt32(sqlCommand.Parameters["@IdToReturn"].Value);
                    rs = argClasses.ClassId;
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

        public Classes GetClasses(ClassRequest argClassRequest)
        {
            Classes classes = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetClass", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argClassRequest.ClassId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {

                        while (sqlDataReader.Read())
                        {
                            classes = new Classes();
                            classes.ClassId = Convert.ToInt32(sqlDataReader["Id"]);
                            classes.ClassName = sqlDataReader["ClassName"].ToString();
                            classes.SectionId = Convert.ToInt32(sqlDataReader["Section"]);
                            classes.SectionName = (EnumSection)classes.SectionId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetClasses", _ClassesDAL, "Classes", ex.Message, DateTime.Now.ToString());
            }
            return classes;
        }


        public List<Classes> GetClassesList(ClassRequest argClassRequest)
        {
            List<Classes> classeslst = null;
            Classes classes = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetClass", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ClassName", argClassRequest.ClassName);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        classeslst = new List<Classes>();
                        while (sqlDataReader.Read())
                        {
                            classes = new Classes();
                            classes.ClassId = Convert.ToInt32(sqlDataReader["Id"]);
                            classes.ClassName = sqlDataReader["ClassName"].ToString();
                            classes.SectionId = Convert.ToInt32(sqlDataReader["Section"]);
                            classes.SectionName = (EnumSection)classes.SectionId;
                            classeslst.Add(classes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetClasses", _ClassesDAL, "Classes", ex.Message, DateTime.Now.ToString());
            }
            return classeslst;
        }



        public bool DeleteClass(ClassRequest argClassRequest)
        {

            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            try
            {

                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteClass", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", argClassRequest.ClassId);

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




        public List<Classes> GetClasses()
        {
            List<Classes> classeslst = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetClass", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        classeslst = new List<Classes>();
                        while (sqlDataReader.Read())
                        {
                            Classes classes1 = new Classes();
                            classes1.ClassId = Convert.ToInt32(sqlDataReader["Id"]);
                            classes1.ClassName = sqlDataReader["ClassName"].ToString();
                            classeslst.Add(classes1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetClasses", _ClassesDAL, "Classes", ex.Message, DateTime.Now.ToString());
            }
            return classeslst;
        }

    }
}