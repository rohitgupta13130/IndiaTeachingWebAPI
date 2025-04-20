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
        [HttpGet]
        public HttpResponseMessage GetSubject(string argSubjectName)
        {
            try
            {
                SubjectRequest subjectRequest = new SubjectRequest() { SubjectName =argSubjectName };
                List<Subject> subjects = new SubjectDAL().GetSubjectList(subjectRequest);
                if (subjects == null)
                {
                    subjects = new List<Subject>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, subjects);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Get : api/Subject/2
        [HttpGet]
        public HttpResponseMessage GetSubject(int id)
        {
            try
            {
                Subject subject = new SubjectDAL().GetSubject(new SubjectRequest() { ID = id });
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
        public HttpResponseMessage Put(int id, [FromBody] Subject subject)
        {
            try
            {
                if (subject == null || subject.ID != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or Id.");
                }
                int subjectId = new SubjectDAL().SaveSubject(subject);
                return Request.CreateResponse(HttpStatusCode.OK, subject);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Delete : api/Subject/2
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                SubjectRequest subjectRequest = new SubjectRequest { ID = id };
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
