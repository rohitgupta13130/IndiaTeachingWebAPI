﻿using India_Teaching.CustomAuthenticationFilter;
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
        public HttpResponseMessage GetStudentbatches(string argStudentName, int batchId = 0)
        {
            try
            {
                StudentbatchesRequest studentBatchesRequest = new StudentbatchesRequest() {  FirstName =  argStudentName , BatchId = batchId };
                List<Studentbatches> studentbatches = new StudentbatchesDAL().GetStudentbatchesList(studentBatchesRequest);
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

        //GET: api/Studentbatches/2
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
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                StudentbatchesRequest studentbatchesRequest = new StudentbatchesRequest { Id = id };
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
