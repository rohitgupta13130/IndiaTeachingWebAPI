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
        [HttpGet]
        public HttpResponseMessage GetTeacher(string teacherName = null)
        {
            try
            {
                TeacherRequest teacherRequest = new TeacherRequest() { Fullname = teacherName };
                List<Teacher> teachers = new TeacherDAL().GetTeacherList(teacherRequest);
                if (teachers == null)
                {
                    teachers = new List<Teacher>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, teachers);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //GET: api/Teacher/2
        [HttpGet]
        public HttpResponseMessage GetTeacher(int id)
        {
            try
            {
                Teacher teacher = new TeacherDAL().GetTeacher(new TeacherRequest() { TeacherID = id });
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
        // PUT: api/Teacher/5
        public HttpResponseMessage Put(int id, [FromBody] Teacher teacher, HttpPostedFileBase file, HttpPostedFileBase videoFile)
        {

            try
            {
                if (teacher == null || teacher.TeacherID != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }
                int Id = new TeacherDAL().SaveTeacherPost(teacher, file, videoFile);
                return Request.CreateResponse(HttpStatusCode.OK, teacher);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }




        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                TeacherRequest teacherRequest = new TeacherRequest { TeacherID = id };
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
