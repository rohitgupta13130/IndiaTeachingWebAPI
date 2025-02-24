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
    public class StudentInfoController : ApiController
    {
        // GET: api/StudentInfo
        [HttpGet]
        public HttpResponseMessage GetStudentInfo()
        {
            try
            {
                List<StudentInfo> studentInfo = new StudentInfoDAL().GetStudentInfoList(new StudentInfoRequest());

                return Request.CreateResponse(HttpStatusCode.OK, studentInfo);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // GET: api/StudentInfo/1
        [HttpGet]
        public HttpResponseMessage GetStudentInfo(int id)
        {
            try
            {

                StudentInfo studentInfo = new StudentInfoDAL().GetStudentInfo(new StudentInfoRequest() { Id = id });

                return Request.CreateResponse(HttpStatusCode.OK, studentInfo);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // Post: api/StudentInfo
        [HttpPost]
        public HttpResponseMessage SaveStudentInfo([FromBody] StudentInfo studentInfo)
        {
            try
            {

                int Id = new StudentInfoDAL().SaveStudentInfo(studentInfo);

                return Request.CreateResponse(HttpStatusCode.OK, Id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // Put: api/StudentInfo/2
        [HttpPut]
        public HttpResponseMessage UpdateStudentInfo(int id, [FromBody] StudentInfo studentInfo)
        {

            try
            {
                if (studentInfo == null || studentInfo.Id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }
                int Id = new StudentInfoDAL().SaveStudentInfo(studentInfo);

                return Request.CreateResponse(HttpStatusCode.OK, Id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


       
        
    }
}
