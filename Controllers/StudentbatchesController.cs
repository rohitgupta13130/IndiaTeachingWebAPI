using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class StudentbatchesController : ApiController
    {
        //GET: api/Studentbatches
        string _StudentbatchesController = "StudentbatchesController";

        [HttpGet]
        public HttpResponseMessage GetStudentbatches([FromUri] StudentbatchesRequest studentbatchesRequest)
        {
            try
            {
                
                List<Studentbatches> studentbatches = new StudentbatchesDAL().GetStudentbatchesList(studentbatchesRequest ?? new StudentbatchesRequest());
                if (studentbatches == null)
                {
                    studentbatches = new List<Studentbatches>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, studentbatches);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetStudnetbatches", _StudentbatchesController, "Studentbatches", ex.Message, DateTime.Now.ToString());

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //GET: api/Studentbatches?Id=5
        [HttpGet]
        [Route("api/Studentbatches")]
        public HttpResponseMessage GetStudentbatch([FromUri] StudentbatchesRequest studentbatchesRequest)
        {
            try
            {
                if (studentbatchesRequest == null || studentbatchesRequest.Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Studentbatches request");
                }

                Studentbatches studentbatches = new StudentbatchesDAL().GetStudentbatches(studentbatchesRequest);

                if (studentbatches == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Studentbatches not found");
                }
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

        //PUT: api/Studentbatches?Id=5
        [HttpPut]
        [Route("api/Studentbatches")]
        public HttpResponseMessage Put([FromBody] Studentbatches studentbatches)
        {
            try
            {
                if (studentbatches == null || studentbatches.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid studentbatches data .");
                }

                int studentbatcheId = new StudentbatchesDAL().SaveStudentbatches(studentbatches);

                if (studentbatcheId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to update studentbatches");
                }
                return Request.CreateResponse(HttpStatusCode.OK, studentbatches);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Delete : api/Studentbatches?Id=5
        [HttpDelete]
        [Route("api/Studentbatches")]
        public HttpResponseMessage Delete([FromBody] StudentbatchesRequest studentbatchesRequest)
        {
            try
            {
                if (studentbatchesRequest == null || studentbatchesRequest.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid studentbatches request.");
                }

                
                bool isDeleted = new StudentbatchesDAL().Delete(studentbatchesRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Studentbatches deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Studentbatches not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
