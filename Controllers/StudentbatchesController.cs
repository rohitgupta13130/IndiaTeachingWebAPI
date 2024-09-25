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

    public class StudentbatchesController : ApiController
    {
        //GET: api/Skill
        [HttpGet]
        public HttpResponseMessage GetStudentbatches()
        {
            try
            {
                List<Studentbatches> studentbatches = new StudentbatchesDAL().GetStudentbatchesList(new StudentbatchesRequest());
                return Request.CreateResponse(HttpStatusCode.OK, studentbatches);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //GET: api/Student/2
        [HttpGet]
        public HttpResponseMessage GetStudentbatches(int id)
        {
            try
            {
                Studentbatches studentbatches = new StudentbatchesDAL().GetStudentbatches(new StudentbatchesRequest() { Id = id });
                return Request.CreateResponse(HttpStatusCode.OK, studentbatches);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //POST: api/Studentbatches
        [HttpPost]
        public HttpResponseMessage SaveStudentbatches([FromBody] Studentbatches studentbatches)
        {
            try
            {
                int studentbatchId = new StudentbatchesDAL().SaveStudentbatches(studentbatches);
                return Request.CreateResponse(HttpStatusCode.OK, studentbatchId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //PUT: api/Studentbatches/2
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Studentbatches studentbatches)
        {
            try
            {
                if (studentbatches == null || studentbatches.Id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }

                int studentbatcheId = new StudentbatchesDAL().SaveStudentbatches(studentbatches);
                return Request.CreateResponse(HttpStatusCode.OK, studentbatches);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Delete : api/Studentbatches/6
        public void Delete(int id)
        {

        }

    }
}
