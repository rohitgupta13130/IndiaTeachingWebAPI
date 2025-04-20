using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class ClassesController : ApiController
    {
        // GET: api/Classes
        [HttpGet]
        public HttpResponseMessage GetClasses(string argClassName)
        {
            try
            {
                ClassRequest classRequest = new ClassRequest() { ClassName = argClassName };
                List<Classes> classes = new ClassesDAL().GetClassesList(classRequest);
                if (classes == null)
                {
                    classes = new List<Classes>();
                }
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
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                ClassRequest classRequest = new ClassRequest { ClassId = id };
                bool isDeleted = new ClassesDAL().DeleteClass(classRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Class deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Class not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
