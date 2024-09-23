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
            if (batches == null || batches.Id != id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
            }
            try
            {
                
                int batchId = new BatchesDAL().SaveBatches(batches);
                return Request.CreateResponse(HttpStatusCode.OK, batchId);  
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        // DELETE: api/Batches/5
        public void Delete(int id)
        {
        }
    }
}
