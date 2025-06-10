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

        //GET: api/Teacher?TeacherId=5
        [HttpGet]
        [Route("api/Teacher")]
        public HttpResponseMessage GetTeacher([FromUri] TeacherRequest teacherRequest)
        {
            try
            {
                if (teacherRequest == null || teacherRequest.TeacherID <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Teacher request");
                }

                Teacher teacher = new TeacherDAL().GetTeacher(teacherRequest);

                if (teacher == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Teacher not found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, teacher);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpPost]
        // POST: api/Teacher
        public HttpResponseMessage SaveTeacher([FromBody] Teacher teacher, HttpPostedFileBase file, HttpPostedFileBase videoFile)
        {
            try
            {
                int Id = new TeacherDAL().SaveTeacherPost(teacher, file, videoFile);
                return Request.CreateResponse(HttpStatusCode.OK, Id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        [HttpPut]
        [Route("api/Teacher")]
        // PUT: api/Teacher?Id=5
        public HttpResponseMessage Put(int id, [FromBody] Teacher teacher, HttpPostedFileBase file, HttpPostedFileBase videoFile)
        {

            try
            {
                if (teacher == null || teacher.TeacherID <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid teacher data.");
                }
                int Id = new TeacherDAL().SaveTeacherPost(teacher, file, videoFile);

                if (Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to update teacher");
                }
                return Request.CreateResponse(HttpStatusCode.OK, teacher);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }




        [HttpDelete]
        [Route("api/Teacher")]
        public HttpResponseMessage Delete([FromBody] TeacherRequest teacherRequest)
        {
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
