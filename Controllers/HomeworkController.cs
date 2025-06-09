using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using India_Teaching.Models;
using India_Teaching.DAL;
using India_Teaching.Request;
using India_Teaching.CustomAuthenticationFilter;
using IndiaTechingClassLibray.DAL;

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class HomeWorkController : ApiController
    {
        // GET: api/HomeWork
        string _HomeWorkController = "HomeWorkController";

        [HttpGet]
        public HttpResponseMessage GetHomeWorks([FromUri] HomeWorkRequest homeWorkRequest)
        {
            try
            {
                
                List<HomeWork> homeWorks = new HomeworkDAL().GetHomeWorkList(homeWorkRequest ?? new HomeWorkRequest());
                if (homeWorks == null)
                {
                    homeWorks = new List<HomeWork>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, homeWorks);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetHomeWorks", _HomeWorkController, "HomeWork", ex.Message, DateTime.Now.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/HomeWork?Id=5
        [HttpGet]
        [Route("api/HomeWork")]
        public HttpResponseMessage GetHomeWork([FromUri] HomeWorkRequest homeWorkRequest)
        {
            try
            {
                if (homeWorkRequest == null || homeWorkRequest.Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Homework request");
                }
                HomeWork homeWork = new HomeworkDAL().GetHomeWork(homeWorkRequest);

                if (homeWork == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "HomeWork not found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, homeWork);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/HomeWork
        [HttpPost]
        public HttpResponseMessage SaveHomeWork([FromBody] HomeWork homeWork)
        {
            try
            {
                int homeWorkId = new HomeworkDAL().SaveHomeWork(homeWork);
                return Request.CreateResponse(HttpStatusCode.OK, homeWorkId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT: api/HomeWork?Id=5
        [HttpPut]
        [Route("api/HomeWork")]
        public HttpResponseMessage UpdateHomeWork([FromBody] HomeWork homeWork)
        {
            try
            {
                if (homeWork == null || homeWork.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid homework data");
                }

                int homeWorkId = new HomeworkDAL().SaveHomeWork(homeWork);

                if (homeWorkId <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to Update Homework");
                }
                return Request.CreateResponse(HttpStatusCode.OK, homeWork);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Homework?Id=5
        [HttpDelete]
        [Route("api/HomeWork")]
        public HttpResponseMessage DeleteHomeWork([FromBody] HomeWorkRequest homeWorkRequest)
        {
            try
            {
                if (homeWorkRequest == null || homeWorkRequest.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                bool isDeleted = new HomeworkDAL().Delete(homeWorkRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Homework deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Homework not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
