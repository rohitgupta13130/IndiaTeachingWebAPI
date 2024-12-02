using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Models;
using IndiaTechingClassLibray.Request;

namespace IndiaTeachingWebAPI.Controllers
{
    public class ExamController : ApiController
    {

        // GET: api/Exam
        [HttpGet]
        public HttpResponseMessage GetExam()
        {
            try
            {
                List<Exam> exam = new ExamDAL().GetExamList(new ExamRequest());

                return Request.CreateResponse(HttpStatusCode.OK, exam);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/Exam/5
        [HttpGet]
        public HttpResponseMessage GetExam(int id)
        {
            try
            {
                Exam exam = new ExamDAL().GetExam(new ExamRequest() { Id = id });
                return Request.CreateResponse(HttpStatusCode.OK, exam);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        // POST: api/Exam
        public HttpResponseMessage SaveExam([FromBody] Exam exam)
        {
            try
            {
                int Id = new ExamDAL().SaveExam(exam);
                return Request.CreateResponse(HttpStatusCode.OK, Id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        // PUT: api/Exam/4
        public HttpResponseMessage Put(int id, [FromBody] Exam exam)
        {

            try
            {
                if (exam == null || exam.Id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }
                int Id = new ExamDAL().SaveExam(exam);
                return Request.CreateResponse(HttpStatusCode.OK, exam);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Exam/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                ExamRequest examRequest = new ExamRequest { Id = id };
                bool isDeleted = new ExamDAL().Delete(examRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Exam deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Exam not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
