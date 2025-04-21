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
        public HttpResponseMessage GetSkills(string argSkillName)
        {
            try
            {
                SkillRequest skillRequest = new SkillRequest() { SkillName = argSkillName };
                List<Skill> skills = new SkillDAL().GetSkillList(skillRequest);

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



        // GET: api/Skill/5
        [HttpGet]
        public HttpResponseMessage GetSkill(int id)
        {
            try
            {
                Skill skill = new SkillDAL().GetSkill(new SkillRequest() { SkillId = id });
                return Request.CreateResponse(HttpStatusCode.OK,skill);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        // POST: api/Skill
        public HttpResponseMessage SaveSkill([FromBody]Skill skill)
        {
            try
            {
                int skillId  = new SkillDAL().SaveSkill(skill);
                return Request.CreateResponse(HttpStatusCode.OK, skillId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        // PUT: api/Skill/5
        public HttpResponseMessage Put(int id, [FromBody] Skill skill)
        {
            
            try
            {
                if (skill == null || skill.SkillId != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }
                int skillId = new SkillDAL().SaveSkill(skill);
                return Request.CreateResponse(HttpStatusCode.OK, skill);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Skill/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                SkillRequest skillRequest = new SkillRequest { SkillId = id };
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
