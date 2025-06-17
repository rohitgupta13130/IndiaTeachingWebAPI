using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Request;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class BatchesController : ApiController
    {

        // GET: api/Batches
        string _BatchesController = "BatchesController";

        [HttpGet]
        public HttpResponseMessage GetBatches([FromUri] BatchesRequest batchesRequest)
        {
            try
            {
               
                List<Batches> batches = new BatchesDAL().GetBatchesList(batchesRequest ?? new BatchesRequest());
                if (batches == null)
                {
                    batches = new List<Batches>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, batches);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetBatches", _BatchesController, "Batches", ex.Message, DateTime.Now.ToString());

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // GET: api/Batches?Id=5
        [HttpGet]
        [Route("api/Batches")]
        public HttpResponseMessage GetBatch([FromUri] BatchesRequest batchesRequest)
        {
            try
            {
                if (batchesRequest == null || batchesRequest.Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid skill request.");
                }
                Batches batches = new BatchesDAL().GetBatches(batchesRequest);
                if (batches == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Batches not found.");
                }
                return Request.CreateResponse(HttpStatusCode.OK, batches);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpPost]
        // POST: api/Batches
        public HttpResponseMessage SaveBatches([FromBody] Batches batches)
        {

            try
            {

                int batchesId = new BatchesDAL().SaveBatches( batches);
                return Request.CreateResponse(HttpStatusCode.OK, batchesId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpPut]
        [Route("api/Batches")]
        public HttpResponseMessage Put( [FromBody] Batches batches)
        {
            
            try
            {
                if (batches == null || batches.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }

                int batchId = new BatchesDAL().SaveBatches(batches);
                if (batchId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Falid to update batches.");
                }
                return Request.CreateResponse(HttpStatusCode.OK, batches);  
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        // DELETE: api/Batches/5
        [HttpDelete]
        [Route("api/Batches")]
        public HttpResponseMessage Delete([FromBody] BatchesRequest batchesRequest)
        {
            try
            {
                if (batchesRequest == null || batchesRequest.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Batches request.");
                }

               
                bool isDeleted = new BatchesDAL().DeleteBatches(batchesRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Batch deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Batch not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
