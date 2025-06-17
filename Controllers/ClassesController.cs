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
        string _ClassesController = "ClassesController";

        [HttpGet]
        public HttpResponseMessage GetClasses([FromUri] ClassRequest classRequest)
        {
            try
            {
               
                List<Classes> classes = new ClassesDAL().GetClassesList(classRequest ?? new ClassRequest());
                if (classes == null)
                {
                    classes = new List<Classes>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, classes);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetClasses", _ClassesController, "Classes", ex.Message, DateTime.Now.ToString());

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // GET: api/Classes?ClassId=5
        [HttpGet]
        [Route("api/Classes")]
        public HttpResponseMessage GetClasse([FromUri] ClassRequest classRequest)
        {
            try
            {
                if (classRequest == null || classRequest.ClassId <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid class request");
                }

                Classes classes = new ClassesDAL().GetClasses(classRequest);
                if (classes == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Class not found");
                }
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
        [Route("api/Classes")]
        // PUT: api/Classes?ClassId=5
        public HttpResponseMessage Put( [FromBody] Classes classes)
        {

            try
            {
                if (classes == null || classes.ClassId <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid class data.");
                }
                int classId = new ClassesDAL().SaveClass(classes);

                if (classId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to update class.");
                }
                return Request.CreateResponse(HttpStatusCode.OK, classes);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Delete/5
        [HttpDelete]
        [Route("api/Classes")]
        public HttpResponseMessage Delete([FromBody] ClassRequest classRequest)
        {
            try
            {
                if (classRequest == null|| classRequest.ClassId <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Class Request.");
                }

               
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
