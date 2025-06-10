using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Models;
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
    public class StudentController : ApiController
    {
        //Get : api/Student
        string _StudentController = "StudentController";

        [HttpGet]
        public HttpResponseMessage GetStudents([FromUri] StudentRequest studentRequest)
        {
            try
            {
                
                List<Student> students = new StudentDAL().GetStudentList(studentRequest ?? new StudentRequest());

                if (students == null)
                {
                    students = new List<Student>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, students);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudent", _StudentController, "Student", ex.Message, DateTime.Now.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Get : api/Student?Id=5
        [HttpGet]
        [Route("api/Student")]
        public HttpResponseMessage GetStudent([FromUri] StudentRequest studentRequest)
        {
            try
            {
                if (studentRequest == null || studentRequest.Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Student request");
                }

                Student student = new StudentDAL().GetStudent(studentRequest);

                if (student == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Student not Found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Post : api/Student
        [HttpPost]
        public HttpResponseMessage SaveStudent([FromBody] Student student)
        {
            try
            {
                int studentId = new StudentDAL().SaveStudent(student);
                return Request.CreateResponse(HttpStatusCode.OK, studentId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //PUT : api/Student?Id=5
        [HttpPut]
        [Route("api/Student")]
        public HttpResponseMessage Put([FromBody] Student student)
        {
            try
            {
                if (student == null || student.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Student Data");
                }
                int studentId = new StudentDAL().SaveStudent(student);

                if (studentId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failde to update student");
                }
                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        // DELETE: api/Student?Id=5
        [HttpDelete]
        [Route("api/Student")]
        public HttpResponseMessage Delete([FromBody] StudentRequest studentRequest)
        {
            try
            {
                if (studentRequest == null || studentRequest.Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Student request.");
                }

               
                bool isDeleted = new StudentDAL().DeleteStudent(studentRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Student deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
