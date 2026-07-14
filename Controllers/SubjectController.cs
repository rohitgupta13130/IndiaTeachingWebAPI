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
    public class SubjectController : ApiController
    {
        //GET : api/Subject
        string _SubjectController = "SubjectController";

        [HttpGet]
        public HttpResponseMessage GetSubjects([FromUri] SubjectRequest subjectRequest)
        {
            Log.Information("Entered GetSubjects method in SubjectController");
            try
            {
                
                List<Subject> subjects = new SubjectDAL().GetSubjectList(subjectRequest ?? new SubjectRequest());
                if (subjects == null)
                {
                    subjects = new List<Subject>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, subjects);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetSubject", _SubjectController, "Subject", ex.Message, DateTime.Now.ToString());

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/Subject/5
        [HttpGet]
        [Route("api/Subject/{subjectId:int}")]
        public IHttpActionResult GetSubject(int subjectId)
        {
            Log.Information($"Entered GetSubject method. SubjectId: {subjectId}");

            try
            {
                if (subjectId <= 0)
                {
                    return BadRequest("Invalid Subject Id.");
                }

                var subjectRequest = new SubjectRequest
                {
                    ID = subjectId
                };

                Subject subject = new SubjectDAL().GetSubject(subjectRequest);

                if (subject == null)
                {
                    return NotFound();
                }

                return Ok(subject);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs(
                    "GetSubject",
                    _SubjectController,
                    "Subject",
                    ex.ToString(),
                    DateTime.Now.ToString());

                return InternalServerError(ex);
            }
        }

        //post : api/Subject
        [HttpPost]
        public HttpResponseMessage SaveSubject([FromBody] Subject subject)
        {
            Log.Information("Entered SaveSubject method in SubjectController");
            try
            {
                int subjectId = new SubjectDAL().SaveSubject(subject);
                return Request.CreateResponse(HttpStatusCode.OK, subjectId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT: api/Subject
        [HttpPut]
        public HttpResponseMessage UpdateSubject([FromBody] Subject subject)
        {
            Log.Information("Entered Update method in SubjectController");

            try
            {
                if (subject == null || subject.ID <= 0)
                {
                    return Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "Invalid subject data. Subject ID is required.");
                }

                int subjectId = new SubjectDAL().SaveSubject(subject);

                if (subjectId <= 0)
                {
                    return Request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError,
                        "Failed to update subject.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, subject);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs(
                    "PutSubject",
                    _SubjectController,
                    "Subject",
                    ex.Message,
                    DateTime.Now.ToString());

                return Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.Message);
            }
        }

        //Delete : api/Subject?Id=5
        [HttpDelete]
        public HttpResponseMessage DeleteSubject([FromBody] SubjectRequest subjectRequest)
        {
            Log.Information("Entered Delete method in SubjectController");
            try
            {
                if (subjectRequest == null || subjectRequest.ID <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Subject request.");
                }

               
                bool isDeleted = new SubjectDAL().DeleteSubject(subjectRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Subject deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Subject not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
