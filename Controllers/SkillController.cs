using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Request;
using IndiaTechingClassLibray.Models;

namespace IndiaTeachingWebAPI.Controllers
{
    public class SkillController : ApiController
    {
        // GET: api/Skill
        public IEnumerable<Skill> Get()
        {
            List<Skill> skills = new SkillDAL().GetSkillList(new SkillRequest());
            return skills;
        }

        // GET: api/Skill/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Skill
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Skill/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Skill/5
        public void Delete(int id)
        {
        }
    }
}
