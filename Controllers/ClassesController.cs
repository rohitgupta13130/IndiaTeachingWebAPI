using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IndiaTeachingWebAPI.Controllers
{
    public class ClassesController : ApiController
    {
        // GET: api/Classes
        [HttpGet]
        public HttpResponseMessage GetClasses()
        {
            try
            {
                List<Classes> classes = new ClassesDAL().GetClassesList(new ClassRequest());

                return Request.CreateResponse(HttpStatusCode.OK, classes);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // GET: api/Classes/5
        [HttpGet]
        public HttpResponseMessage GetClasses(int id)
        {
            try
            {
                Classes classes = new ClassesDAL().GetClasses(new ClassRequest() { ClassId = id });
                return Request.CreateResponse(HttpStatusCode.OK, classes);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        // POST: api/Classes
        public HttpResponseMessage SaveClasses([FromBody] Classes classes)
        {
            try
            {
                int classId = new ClassesDAL().SaveClass(classes);
                return Request.CreateResponse(HttpStatusCode.OK, classId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpPut]
        // PUT: api/Classes/3
        public HttpResponseMessage Put(int id, [FromBody] Classes classes)
        {

            try
            {
                if (classes == null || classes.ClassId != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }
                int classId = new ClassesDAL().SaveClass(classes);
                return Request.CreateResponse(HttpStatusCode.OK, classes);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Delete/5
        public void Delete(int id)
        {
        }

    }
}
