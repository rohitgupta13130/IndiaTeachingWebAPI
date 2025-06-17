using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class FeeTransactionController : ApiController
    {

        //GET : api/FeeTransaction
        [HttpGet]
        public HttpResponseMessage GetFeeTransactions([FromUri] FeeTransactionRequest feeTransactionRequest)
        {
            try
            {
                List<FeeTransaction> feeTransactions = new FeeTransactionDAL().GetFeeTransactionList(feeTransactionRequest ?? new FeeTransactionRequest());
                if (feeTransactions == null)
                {
                    feeTransactions = new List<FeeTransaction>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, feeTransactions);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //GET : api/FeeTransaction?Id=5
     
        [HttpGet]
        [Route("api/FeeTransaction")]
        public HttpResponseMessage GetFeeTransaction([FromUri] FeeTransactionRequest feeTransactionRequest)
        {
            try
            {
                if (feeTransactionRequest == null || feeTransactionRequest.FeetransactionId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid FeeTransaction Request");
                }
                FeeTransaction feeTransaction = new FeeTransactionDAL().GetFeeTransaction(feeTransactionRequest);

                if (feeTransaction == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "FeeTransaction not found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, feeTransaction);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        //POST: api/FeeTransaction
        [HttpPost]
        public HttpResponseMessage SaveFeeTransaction([FromBody] FeeTransaction feeTransaction)
        {
            try
            {
                int feeTransactionId = new FeeTransactionDAL().SaveFeeTransaction(feeTransaction);
                return Request.CreateResponse(HttpStatusCode.OK, feeTransactionId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //PUT: api/FeeTranaction?Id=5
        [HttpPut]
        [Route("api/FeeTransaction")]
        public HttpResponseMessage Put( [FromBody] FeeTransaction feeTransaction)
        {
            try
            {
                if (feeTransaction == null || feeTransaction.FeetransactionId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid feetransaction data");
                }

                int feeTransactionId = new FeeTransactionDAL().SaveFeeTransaction(feeTransaction);

                if (feeTransactionId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to update FeeTrascation");
                }
                return Request.CreateResponse(HttpStatusCode.OK, feeTransaction);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //DELETE : api/FeeTransaction/5
        public void Delete(int id)
        {

        }
    }
}
