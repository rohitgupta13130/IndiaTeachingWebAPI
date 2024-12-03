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
    public class FeeTransactionController : ApiController
    {

        //GET : api/FeeTransaction
        [HttpGet]
        public HttpResponseMessage GetFeeTransaction()
        {
            try
            {
                List<FeeTransaction> feeTransactions = new FeeTransactionDAL().GetFeeTransactionList(new FeeTransactionRequest());
                return Request.CreateResponse(HttpStatusCode.OK, feeTransactions);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //GET : api/FeeTransaction /2
        [HttpGet]
        public HttpResponseMessage GetFeeTransaction(int id)
        {
            try
            {
                FeeTransaction feeTransaction = new FeeTransactionDAL().GetFeeTransaction(new FeeTransactionRequest() { FeetransactionId = id });
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

        //PUT: api/FeeTransaction / 2
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] FeeTransaction feeTransaction)
        {
            try
            {
                if (feeTransaction == null || feeTransaction.FeetransactionId != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or Id");
                }

                int feeTransactionId = new FeeTransactionDAL().SaveFeeTransaction(feeTransaction);
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
