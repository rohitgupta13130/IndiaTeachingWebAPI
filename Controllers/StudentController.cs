using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Models;
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
    public class StudentController : ApiController
    {
        //Get : api/Student
        string _StudentController = "StudentController";

        [HttpGet]
        public HttpResponseMessage GetStudents([FromUri] StudentRequest studentRequest)
        {
            Log.Information("Entered GetStudents method in StudentController");
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

        // GET: api/Student/5
        [HttpGet]
        [Route("api/Student/{id:int}")]
        public IHttpActionResult GetStudent(int id)
        {
            Log.Information($"Entered GetStudent method. Id: {id}");

            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Student Id.");
                }

                var studentRequest = new StudentRequest
                {
                    Id = id
                };

                Student student = new StudentDAL().GetStudent(studentRequest);

                if (student == null)
                {
                    return NotFound();
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs(
                    "GetStudent",
                    _StudentController,
                    "Student",
                    ex.ToString(),
                    DateTime.Now.ToString());

                return InternalServerError(ex);
            }
        }

        //Post : api/Student
        [HttpPost]
        public HttpResponseMessage SaveStudent([FromBody] Student student)
        {
            Log.Information("Entered SaveStudent method StudentController");
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

        // PUT: api/Student
        [HttpPut]
        public HttpResponseMessage UpdateStudent([FromBody] Student student)
        {
            Log.Information("Entered Update method in StudentController");

            try
            {
                if (student == null || student.Id <= 0)
                {
                    return Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "Invalid student data. Id is required.");
                }

                int studentId = new StudentDAL().SaveStudent(student);

                if (studentId <= 0)
                {
                    return Request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError,
                        "Failed to update student.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs(
                    "PutStudent",
                    _StudentController,
                    "Student",
                    ex.Message,
                    DateTime.Now.ToString());

                return Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.Message);
            }
        }

        // DELETE: api/Student
        [HttpDelete]
        public HttpResponseMessage DeleteStudent([FromBody] StudentRequest studentRequest)
        {
            Log.Information("Entered Delete method in StudentController");

            try
            {
                if (studentRequest == null || studentRequest.Id <= 0)
                {
                    return Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "Invalid student request.");
                }

                bool isDeleted = new StudentDAL().DeleteStudent(studentRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(
                        HttpStatusCode.OK,
                        "Student deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(
                        HttpStatusCode.NotFound,
                        "Student not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs(
                    "DeleteStudent",
                    _StudentController,
                    "Student",
                    ex.Message,
                    DateTime.Now.ToString());

                return Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.Message);
            }
        }


    }
}
