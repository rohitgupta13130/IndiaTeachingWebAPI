using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IndiaTeachingWebAPI.Controllers
{
    public class TeacherController : ApiController
    {

        //GET: api/Teacher
        [HttpGet]
        public HttpResponseMessage GetTeacher()
        {
            try
            {
                List<Teacher> teachers = new TeacherDAL().GetTeacherList(new TeacherRequest());
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


        //POST : api/Teacher
        //[HttpPost]
        //public HttpResponseMessage SaveTeacher([FromBody] Teacher teacher)
        //{
        //    try
        //    {
        //        int teacherId = new TeacherDAL().SaveTeacherPost(teacher,);

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
