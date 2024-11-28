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
    public class BatchesController : ApiController
    {

        // GET: api/Batches
        [HttpGet]
        public HttpResponseMessage GetBatches()
        {
            try
            {
                List<Batches> batches = new BatchesDAL().GetBatchesList(new BatchesRequest());

                return Request.CreateResponse(HttpStatusCode.OK, batches);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // GET: api/Batches/5
        [HttpGet]
        public HttpResponseMessage GetBatches(int id)
        {
            try
            {
                Batches batches = new BatchesDAL().GetBatches(new BatchesRequest() { Id = id });
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
        public HttpResponseMessage Put(int id, [FromBody] Batches batches)
        {
            
            try
            {
                if (batches == null || batches.Id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }

                int batchId = new BatchesDAL().SaveBatches(batches);
                return Request.CreateResponse(HttpStatusCode.OK, batchId);  
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        // DELETE: api/Batches/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                BatchesRequest batchRequest = new BatchesRequest { Id = id };
                bool isDeleted = new BatchesDAL().DeleteBatches(batchRequest);

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
