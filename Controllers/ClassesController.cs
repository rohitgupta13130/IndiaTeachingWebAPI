using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Request;
using Serilog;
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
            Log.Information("Entered GetClasses method in ClassesController");

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


        // GET: api/Classes/5
        [HttpGet]
        [Route("api/Classes/{classId:int}")]
        public IHttpActionResult GetClasse(int classId)
        {
            Log.Information($"Entered GetClasse method. ClassId: {classId}");

            try
            {
                if (classId <= 0)
                {
                    return BadRequest("Invalid Class Id.");
                }

                var classRequest = new ClassRequest
                {
                    ClassId = classId
                };

                Classes classes = new ClassesDAL().GetClasses(classRequest);

                if (classes == null)
                {
                    return NotFound();
                }

                return Ok(classes);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs(
                    "GetClasse",
                    _ClassesController,
                    "Classes",
                    ex.ToString(),
                    DateTime.Now.ToString());

                return InternalServerError(ex);
            }
        }

        [HttpPost]
        // POST: api/Classes
        public HttpResponseMessage SaveClasses([FromBody] Classes classes)
        {
            Log.Information("Entered SaveClasses method in ClassesController");
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
            Log.Information("Entered (Update) method in ClassesController");
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
            Log.Information("Entered Delete method in ClassesController");
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
