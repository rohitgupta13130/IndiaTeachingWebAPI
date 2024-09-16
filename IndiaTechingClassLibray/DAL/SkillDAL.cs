using IndiaTechingClassLibray.Models;
using IndiaTechingClassLibray.Request;
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
    public class SkillDAL
    {
        string _SkillDAL = "SkillDAL";

        public int SaveSkill(Skill argSkill)
        {
            int rs = 0;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("SaveSkill", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@SkillId", argSkill.SkillId);
                    sqlCommand.Parameters.AddWithValue("@SkillName", argSkill.SkillName);
                    sqlCommand.Parameters.AddWithValue("@SkillLevel", argSkill.SkillLevel);
                    sqlCommand.Parameters.AddWithValue("@IsCertificate", Convert.ToInt32(argSkill.Iscertificate));
                    sqlCommand.Parameters.AddWithValue("@IsActive", argSkill.IsActive);
                    SqlParameter outputParam = new SqlParameter
                    {
                        ParameterName = "@SkillIdToReturn",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output,
                        Size = -1
                    };
                    sqlCommand.Parameters.Add(outputParam);
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    if (outputParam.Value != DBNull.Value)
                    {
                        argSkill.SkillId = Convert.ToInt32(outputParam.Value);
                        rs = argSkill.SkillId;
                    }
                    else
                    {
                        Console.WriteLine("Output parameter value is null.");
                    }
                    rs = argSkill.SkillId;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("SaveSkill", _SkillDAL, "Skill", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }

        public Skill GetSkill(SkillRequest argSkillRequest)
        {
            Skill skill = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;

            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetSkill", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@SkillId", argSkillRequest.SkillId);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            skill = new Skill();
                            skill.SkillId = Convert.ToInt32(sqlDataReader["SkillId"]);
                            skill.SkillName = sqlDataReader["SkillName"].ToString();
                            skill.SkillLevel = sqlDataReader["SkillLevel"].ToString();
                            skill.Iscertificate = sqlDataReader["Iscertificate"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetSkill", _SkillDAL, "Skill", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return skill;
        }

        public List<Skill> GetSkillList(SkillRequest argSkillRequest)
        {
            List<Skill> skillList = null;
            Skill skill = null;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("GetSkill", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@SkillName", argSkillRequest.SkillName);
                    connection.Open();
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        skillList = new List<Skill>();
                        while (sqlDataReader.Read())
                        {
                            skill = new Skill();
                            skill.SkillId = Convert.ToInt32(sqlDataReader["SkillId"]);
                            skill.SkillName = sqlDataReader["SkillName"].ToString();
                            skill.SkillLevel = sqlDataReader["SkillLevel"].ToString();
                            skill.Iscertificate = sqlDataReader["Iscertificate"].ToString();

                            skillList.Add(skill);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetSkillList", _SkillDAL, "Skill", ex.Message, DateTime.Now.ToString());
            }
            finally
            {

            }
            return skillList;
        }

        public bool DeleteSkill(SkillRequest argSkillRequest)
        {
            bool isSuccess = false;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString))
                {
                    sqlCommand = new SqlCommand("DeleteSkill", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@SkillId", argSkillRequest.SkillId);

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("DeleteSkill", _SkillDAL, "Skill", ex.Message, DateTime.Now.ToString());
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
    }
}
