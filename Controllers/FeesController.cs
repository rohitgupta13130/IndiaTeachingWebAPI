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

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class FeesController : ApiController
    {



        // GET: api/Fees
        [HttpGet]
        public HttpResponseMessage GetFees()
        {
            try
            {
                List<Fees> skills = new FeesDAL().GetFeesList(new FeesRequest());

                return Request.CreateResponse(HttpStatusCode.OK, skills);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/Fees/5
        [HttpGet]
        public HttpResponseMessage GetFees(int id)
        {
            try
            {
                Fees fees = new FeesDAL().GetFees(new FeesRequest() { Id = id });
                return Request.CreateResponse(HttpStatusCode.OK, fees);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        // POST: api/Fees
        public HttpResponseMessage SaveFees([FromBody] Fees fees)
        {
            try
            {
                int feesId = new FeesDAL().SaveFees(fees);
                return Request.CreateResponse(HttpStatusCode.OK, feesId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpPut]
        // PUT: api/Fees/5
        public HttpResponseMessage Put(int id, [FromBody] Fees fees)
        {
            try
            {
                if (fees == null || fees.Id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }

               int Id = new FeesDAL().SaveFees(fees); 

                return Request.CreateResponse(HttpStatusCode.OK, fees);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // DELETE: api/Fees/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                FeesRequest feesRequest = new FeesRequest { Id = id };
                bool isDeleted = new FeesDAL().DeleteFees(feesRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Fees deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Fees not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
