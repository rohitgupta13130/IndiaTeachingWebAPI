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
        [HttpGet]
        public HttpResponseMessage GetStudent(string firstName = null)
        {
            try
            {
                StudentRequest studentRequest = new StudentRequest() { FirstName = firstName };
                List<Student> students = new StudentDAL().GetStudentList(studentRequest);

                if (students == null)
                {
                    students = new List<Student>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, students);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Get : api/Student/2
        [HttpGet]
        public HttpResponseMessage GetStudent(int id)
        {
            try
            {
                Student student = new StudentDAL().GetStudent(new StudentRequest() { Id = id });
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

        //PUT : api/Student/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Student student)
        {
            try
            {
                if (student == null || student.Id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Data or ID");
                }
                int studentId = new StudentDAL().SaveStudent(student);
                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        // DELETE: api/Student/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                StudentRequest studentRequest = new StudentRequest { Id = id };
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
