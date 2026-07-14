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
using Serilog;

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class SkillController : ApiController
    {
        string _SkillController = "SkillController";

        // GET: api/Skill
        [HttpGet]
        public HttpResponseMessage GetSkills([FromUri] SkillRequest skillRequest)
        {
            Log.Information("Entered GetSkills method in SkillController");
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

        // GET: api/Skill/5
        [HttpGet]
        [Route("api/Skill/{skillId:int}")]
        public IHttpActionResult GetSkill(int skillId)
        {
            Log.Information($"Entered GetSkill method. SkillId: {skillId}");

            try
            {
                if (skillId <= 0)
                {
                    return BadRequest("Invalid Skill Id.");
                }

                var skillRequest = new SkillRequest
                {
                    SkillId = skillId
                };

                Skill skill = new SkillDAL().GetSkill(skillRequest);

                if (skill == null)
                {
                    return NotFound();
                }

                return Ok(skill);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs(
                    "GetSkill",
                    _SkillController,
                    "Skill",
                    ex.ToString(),
                    DateTime.Now.ToString());

                return InternalServerError(ex);
            }
        }

        // POST: api/Skill
        [HttpPost]
        public HttpResponseMessage SaveSkill([FromBody] Skill skill)
        {
            Log.Information("Entered SaveSkill method in SkillController");
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

        // PUT: api/Skill
        [HttpPut]
        public HttpResponseMessage UpdateSkill([FromBody] Skill skill)
        {
            Log.Information("Entered Update method in SkillController");
            try
            {
                if (skill == null || skill.SkillId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid skill data. SkillId is required.");
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
        public HttpResponseMessage DeleteSkill([FromBody] SkillRequest skillRequest)
        {
            Log.Information("Entered Delete method");
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