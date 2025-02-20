using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.Models;

namespace IndiaTeachingWebAPI.Controllers
{
    public class StudentCodeVerifyController : ApiController
    {

        // GET: api/StudentCodeVerify
        [HttpGet]
        public HttpResponseMessage GetStudentCodeVerify()
        {
            try
            {
                List<StudentCodeVerify> studentCodeVerify = new StudentCodeVerifyDAL().GetStudentCodeVerifyList(new StudentCodeVerifyRequest());

                return Request.CreateResponse(HttpStatusCode.OK, studentCodeVerify);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // GET: api/StudentCodeVerify/2
        [HttpGet]
        public HttpResponseMessage GetStudentCodeVerify(int id)
        {
            try
            {
                StudentCodeVerify studentCodeVerify = new StudentCodeVerifyDAL().GetStudentCodeVerify(new StudentCodeVerifyRequest() { Id = id });

                return Request.CreateResponse(HttpStatusCode.OK, studentCodeVerify);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/StudentCodeVerify
        [HttpPost]
        public HttpResponseMessage SaveStudentCodeVerify([FromBody] StudentCodeVerify studentCodeVerify)
        {
            try
            {
                int id = new StudentCodeVerifyDAL().SaveStudentCodeVerify(studentCodeVerify);

                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // GET: api/StudentCodeVerify/5
        [HttpPut]
        public HttpResponseMessage UpdateStudentCodeVerify(int id,[FromBody] StudentCodeVerify studentCodeVerify)
        {
            try
            {
                if (studentCodeVerify == null || studentCodeVerify.Id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }
                int studentCodeId = new StudentCodeVerifyDAL().SaveStudentCodeVerify(studentCodeVerify);

                return Request.CreateResponse(HttpStatusCode.OK, studentCodeVerify);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


    }
}
