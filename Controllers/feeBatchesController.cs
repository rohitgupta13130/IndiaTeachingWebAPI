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
        string _FeeBatchesController = "FeeBatchesController";

        [HttpGet]
        public HttpResponseMessage GetfeeBatches([FromUri] FeeBatchesRequest feeBatchesRequest)
        {
            try
            {
               
                List<FeeBatches> feeBatches = new feeBatchesDAL().GetFeeBatchList(feeBatchesRequest?? new FeeBatchesRequest());
                if (feeBatches == null)
                {
                    feeBatches = new List<FeeBatches>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, feeBatches);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetfeeBatches", _FeeBatchesController, "FeeBatches", ex.Message, DateTime.Now.ToString());

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        //GET: api/feeBatches?Id=5
        [HttpGet]
        [Route("api/feeBatches")]
        public HttpResponseMessage GetfeeBatche([FromUri] FeeBatchesRequest feeBatchesRequest)
        {
            try
            {
                if (feeBatchesRequest == null || feeBatchesRequest.Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid feebatches request");
                }
                FeeBatches feeBatches = new feeBatchesDAL().GetFeeBatch(feeBatchesRequest);
                if (feeBatches == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Feebatches not found");
                }
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

        //PUT : api/feeBatches?Id=5
        [HttpPut]
        [Route("api/feeBatches")]
        public HttpResponseMessage Put([FromBody] FeeBatches feeBatches)
        {

            try
            {
                if (feeBatches == null || feeBatches.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid feebatches data");
                }

                int feeBatchId = new feeBatchesDAL().SaveFeeBatches(feeBatches);
                if (feeBatchId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to update feebatches");
                }
                return Request.CreateResponse(HttpStatusCode.OK, feeBatches);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        //DELETE : api/feeBatches?Id=5
        [HttpDelete]
        [Route("api/feeBatches")]
        public HttpResponseMessage Delete([FromBody] FeeBatchesRequest feeBatchesRequest)
        {
            try
            {
                if (feeBatchesRequest == null || feeBatchesRequest.Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

              
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
