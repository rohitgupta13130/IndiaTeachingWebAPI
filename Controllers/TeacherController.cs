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
using System.Web;
using System.Web.Http;

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class TeacherController : ApiController
    {

        //GET: api/Teacher
        string _TeacherController = "TeacherController";

        [HttpGet]
        public HttpResponseMessage GetTeachers([FromUri] TeacherRequest teacherRequest)
        {
            Log.Information("Entered GetTeachers method in TeacherController");
            try
            {
                
                List<Teacher> teachers = new TeacherDAL().GetTeacherList(teacherRequest ?? new TeacherRequest());
                if (teachers == null)
                {
                    teachers = new List<Teacher>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, teachers);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetTeacher", _TeacherController, "Teacher", ex.Message, DateTime.Now.ToString());

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/Teacher/5
        [HttpGet]
        [Route("api/Teacher/{teacherId:int}")]
        public IHttpActionResult GetTeacher(int teacherId)
        {
            Log.Information($"Entered GetTeacher method. TeacherId: {teacherId}");

            try
            {
                if (teacherId <= 0)
                {
                    return BadRequest("Invalid Teacher Id.");
                }

                var teacherRequest = new TeacherRequest
                {
                    TeacherID = teacherId
                };

                Teacher teacher = new TeacherDAL().GetTeacher(teacherRequest);

                if (teacher == null)
                {
                    return NotFound();
                }

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs(
                    "GetTeacher",
                    _TeacherController,
                    "Teacher",
                    ex.ToString(),
                    DateTime.Now.ToString());

                return InternalServerError(ex);
            }
        }


        [HttpPost]
        public HttpResponseMessage SaveTeacher()
        {
            Log.Information("Entered SaveTeacher method in TeacherController");

            try
            {
                var request = HttpContext.Current.Request;

                // Get JSON data
                string teacherJson = request.Form["teacher"];

                Teacher teacher = Newtonsoft.Json.JsonConvert
                                        .DeserializeObject<Teacher>(teacherJson);

                // Get uploaded files
                HttpPostedFile profileFile = request.Files["profileFile"];
                HttpPostedFile videoFile = request.Files["videoFile"];

                int id = new TeacherDAL().SaveTeacherPost(teacher, profileFile, videoFile);

                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in SaveTeacher");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        // PUT: api/Teacher
        [HttpPut]
        public HttpResponseMessage UpdateTeacher()
        {
            Log.Information("Entered Update method in TeacherController");

            try
            {
                var request = HttpContext.Current.Request;

                string teacherJson = request.Form["teacher"];

                if (string.IsNullOrEmpty(teacherJson))
                {
                    return Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "Teacher data is required.");
                }

                Teacher teacher = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<Teacher>(teacherJson);

                if (teacher == null || teacher.TeacherID <= 0)
                {
                    return Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "Invalid teacher data. TeacherID is required.");
                }

                HttpPostedFile profileFile = request.Files["profileFile"];
                HttpPostedFile videoFile = request.Files["videoFile"];

                int teacherId = new TeacherDAL()
                    .SaveTeacherPost(teacher, profileFile, videoFile);

                if (teacherId <= 0)
                {
                    return Request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError,
                        "Failed to update teacher.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, teacher);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs(
                    "UpdateTeacher",
                    _TeacherController,
                    "Teacher",
                    ex.Message,
                    DateTime.Now.ToString());

                return Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    ex.Message);
            }
        }




        [HttpDelete]
        public HttpResponseMessage DeleteTeacher([FromBody] TeacherRequest teacherRequest)
        {
            Log.Information("Entered Delete method in TeacherController");
            try
            {
                if (teacherRequest == null || teacherRequest.TeacherID <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid teacher request.");
                }

               
                bool isDeleted = new TeacherDAL().DeleteTeacher(teacherRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Teacher deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Teacher not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
