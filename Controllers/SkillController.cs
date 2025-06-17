using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Request;
using IndiaTechingClassLibray.Models;
using India_Teaching.CustomAuthenticationFilter;

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class SkillController : ApiController
    {
        // GET: api/Skill
        string _SkillController = "SkillController";

        [HttpGet]
        public HttpResponseMessage GetSkills([FromUri] SkillRequest skillRequest)
        {
            try
            {
                List<Skill> skills = new SkillDAL().GetSkillList(skillRequest ?? new SkillRequest());

                if (skills == null)
                {
                    skills = new List<Skill>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, skills);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetSkills", _SkillController, "Skill", ex.Message, DateTime.Now.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        // GET: api/Skill?SkillId=5
        [HttpGet]
        [Route("api/Skill")]
        public HttpResponseMessage GetSkill([FromUri] SkillRequest skillRequest)
        {
            try
            {
                if (skillRequest == null || skillRequest.SkillId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid skill request.");
                }

                Skill skill = new SkillDAL().GetSkill(skillRequest);

                if (skill == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Skill not found.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, skill);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetSkill", _SkillController, "Skill", ex.Message, DateTime.Now.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpPost]
        // POST: api/Skill
        public HttpResponseMessage SaveSkill([FromBody] Skill skill)
        {
            try
            {
                int skillId = new SkillDAL().SaveSkill(skill);
                return Request.CreateResponse(HttpStatusCode.OK, skillId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpPut]
        [Route("api/Skill")]
        public HttpResponseMessage Put([FromBody] Skill skill)
        {
            try
            {
                if (skill == null || skill.SkillId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid skill data.");
                }

                int skillId = new SkillDAL().SaveSkill(skill);

                if (skillId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to update skill.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, skill);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("PutSkill", _SkillController, "Skill", ex.Message, DateTime.Now.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // DELETE: api/Skill
        [HttpDelete]
        [Route("api/Skill")]
        public HttpResponseMessage Delete([FromBody] SkillRequest skillRequest)
        {
            try
            {
                if (skillRequest == null || skillRequest.SkillId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid skill request.");
                }

                bool isDeleted = new SkillDAL().DeleteSkill(skillRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Skill deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Skill not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
