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
    public class SubjectController : ApiController
    {
        //GET : api/Subject
        string _SubjectController = "SubjectController";

        [HttpGet]
        public HttpResponseMessage GetSubjects([FromUri] SubjectRequest subjectRequest)
        {
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

        //Get : api/Subject?Id=5
        [HttpGet]
        [Route("api/Subject")]
        public HttpResponseMessage GetSubject([FromUri] SubjectRequest subjectRequest)
        {
            try
            {
                if (subjectRequest == null || subjectRequest.ID <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Subject request.");
                }
                Subject subject = new SubjectDAL().GetSubject(subjectRequest);

                if (subject == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Subject not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, subject);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //post : api/Subject
        [HttpPost]
        public HttpResponseMessage SaveSubject([FromBody] Subject subject)
        {
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

        //PUT : api/Subject/2
        [HttpPut]
        [Route("api/Subject")]
        public HttpResponseMessage Put([FromBody] Subject subject)
        {
            try
            {
                if (subject == null || subject.ID <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Subject data.");
                }
                int subjectId = new SubjectDAL().SaveSubject(subject);

                if (subjectId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to update subject.");
                }
                return Request.CreateResponse(HttpStatusCode.OK, subject);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Delete : api/Subject?Id=5
        [HttpDelete]
        [Route("api/Subject")]
        public HttpResponseMessage Delete([FromBody] SubjectRequest subjectRequest)
        {
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
