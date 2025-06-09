using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Models;
using IndiaTechingClassLibray.Request;

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class ExamController : ApiController
    {

        // GET: api/Exam
        string _ExamController = "ExamController";

        [HttpGet]
        public HttpResponseMessage GetExams([FromUri] ExamRequest examRequest)
        {
            try
            {
                
                List<Exam> exam = new ExamDAL().GetExamList(examRequest ?? new ExamRequest());

                if (exam == null)
                {
                    exam = new List<Exam>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, exam);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetExam", _ExamController, "Exam", ex.Message, DateTime.Now.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/Exam?Id=5
        [HttpGet]
        [Route("api/Exam")]
        public HttpResponseMessage GetExam([FromUri] ExamRequest examRequest)
        {
            try
            {

                if (examRequest == null || examRequest.Id < 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid exam request.");
                }

                Exam exam = new ExamDAL().GetExam(examRequest);
                if (exam == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Exam not found.");
                }
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
        [Route("api/Exam")]
        // PUT: api/Exam?Id=5
        public HttpResponseMessage Put( [FromBody] Exam exam)
        {

            try
            {
                if (exam == null || exam.Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid exam Request.");
                }
                int Id = new ExamDAL().SaveExam(exam);
                if (Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to update exam.");
                }
                return Request.CreateResponse(HttpStatusCode.OK, exam);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Exam/5
        [HttpDelete]
        [Route("api/Exam")]
        public HttpResponseMessage Delete([FromBody] ExamRequest examRequest)
        {
            try
            {
                if (examRequest == null || examRequest.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Exam request.");
                }

               
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
