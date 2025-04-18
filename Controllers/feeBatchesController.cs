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

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class feeBatchesController : ApiController
    {

        //GET: api/feeBatches
        [HttpGet]
        public HttpResponseMessage GetfeeBatches(int feeId = 0, int batchId = 0)
        {
            try
            {
                FeeBatchesRequest feeBatchesRequest = new FeeBatchesRequest() { FeeId = feeId, batchId = batchId };
                List<FeeBatches> feeBatches = new feeBatchesDAL().GetFeeBatchList(feeBatchesRequest);
                if (feeBatches == null)
                {
                    feeBatches = new List<FeeBatches>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, feeBatches);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        //GET: api/feeBatches/2
        [HttpGet]
        public HttpResponseMessage GetfeeBatches(int id)
        {
            try
            {

                FeeBatches feeBatches = new feeBatchesDAL().GetFeeBatch(new FeeBatchesRequest() { Id = id });
                return Request.CreateResponse(HttpStatusCode.OK, feeBatches);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        //POST :api/feeBatches
        [HttpPost]
        public HttpResponseMessage SavefeeBatches([FromBody] FeeBatches feeBatches)
        {
            try
            {
                int feeBatchId = new feeBatchesDAL().SaveFeeBatches(feeBatches);
                return Request.CreateResponse(HttpStatusCode.OK, feeBatchId);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //PUT : api/feeBatches/2
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] FeeBatches feeBatches)
        {

            try
            {
                if (feeBatches == null || feeBatches.Id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Data or ID");
                }

                int feeBatchId = new feeBatchesDAL().SaveFeeBatches(feeBatches);
                return Request.CreateResponse(HttpStatusCode.OK, feeBatches);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        //DELETE : api/feeBatches/2
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                FeeBatchesRequest feeBatchesRequest = new FeeBatchesRequest { Id = id };
                bool isDeleted = new feeBatchesDAL().DeleteFeeBatches(feeBatchesRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "FeeBatches deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "FeeBatches not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
