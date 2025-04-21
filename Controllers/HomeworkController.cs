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
        public HttpResponseMessage GetHomeWorks(string argHomework, int teacherId = 0)
        {
            try
            {
                HomeWorkRequest homeWorkRequest = new HomeWorkRequest() { Homework = argHomework, TeacherId = teacherId };
                List<HomeWork> homeWorks = new HomeworkDAL().GetHomeWorkList(homeWorkRequest);
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

        // GET: api/HomeWork/5
        [HttpGet]
        public HttpResponseMessage GetHomeWork(int id)
        {
            try
            {
                HomeWork homeWork = new HomeworkDAL().GetHomeWork(new HomeWorkRequest { Id = id });
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

        // PUT: api/HomeWork/5
        [HttpPut]
        public HttpResponseMessage UpdateHomeWork(int id, [FromBody] HomeWork homeWork)
        {
            try
            {
                if (homeWork == null || homeWork.Id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }

                int homeWorkId = new HomeworkDAL().SaveHomeWork(homeWork);
                return Request.CreateResponse(HttpStatusCode.OK, homeWork);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/HomeWork/5
        [HttpDelete]
        public HttpResponseMessage DeleteHomeWork(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                HomeWorkRequest homeWorkRequest = new HomeWorkRequest { Id = id };
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
