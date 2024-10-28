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
    public class NotificationController : ApiController
    {


        // GET: api/Notification
        [HttpGet]
        public HttpResponseMessage GetNotification()
        {
            try
            {
                List<Notification> notifications = new NotificationDAL().GetNotificationList(new NotificationRequest());

                return Request.CreateResponse(HttpStatusCode.OK, notifications);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/Notification/5
        [HttpGet]
        public HttpResponseMessage GetNotification(int id)
        {
            try
            {
                Notification notification = new NotificationDAL().GetNotification(new NotificationRequest() { Id = id });
                return Request.CreateResponse(HttpStatusCode.OK, notification);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        // POST: api/Notification
        public HttpResponseMessage SaveNotification([FromBody] Notification notification)
        {
            try
            {
                int Id = new NotificationDAL().SaveNotification(notification);
                return Request.CreateResponse(HttpStatusCode.OK, Id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        // PUT: api/Notification/5
        public HttpResponseMessage Put(int id, [FromBody] Notification notification)
        {

            try
            {
                if (notification == null || notification.Id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data or ID.");
                }
                int Id = new NotificationDAL().SaveNotification(notification);
                return Request.CreateResponse(HttpStatusCode.OK, notification);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Notification/5
        public void Delete(int id)
        {
        }
    }
}
