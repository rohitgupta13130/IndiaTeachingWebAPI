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
    public class NotificationController : ApiController
    {


        // GET: api/Notification
        string _NotificationController = "NotificationController";

        [HttpGet]
        public HttpResponseMessage GetNotifications([FromUri] NotificationRequest notificationRequest)
        {
            try
            {
               
                List<Notification> notifications = new NotificationDAL().GetNotificationList(notificationRequest ?? new NotificationRequest());

                if(notifications == null)
                {
                    notifications = new List<Notification>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, notifications);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetNotification", _NotificationController, "Notification", ex.Message, DateTime.Now.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/Notification?Id=5
        [HttpGet]
        [Route("api/Notification")]
        public HttpResponseMessage GetNotification([FromUri] NotificationRequest notificationRequest)
        {
            try
            {
                if (notificationRequest == null || notificationRequest.Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Notification request");
                }

                Notification notification = new NotificationDAL().GetNotification(notificationRequest);

                if (notification == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Notification not found");
                }
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
        [Route("api/Notification")]
        // PUT: api/Notification?Id=5
        public HttpResponseMessage Put([FromBody] Notification notification)
        {

            try
            {
                if (notification == null || notification.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid notification data.");
                }
                int Id = new NotificationDAL().SaveNotification(notification);

                if (Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to update notification");
                }
                return Request.CreateResponse(HttpStatusCode.OK, notification);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Notification?Id=5
        [HttpDelete]
        [Route("api/Notification")]
        public HttpResponseMessage Delete([FromBody] NotificationRequest notificationRequest)
        {
            try
            {
                if (notificationRequest == null || notificationRequest.Id <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Notification request.");
                }

                
                bool isDeleted = new NotificationDAL().DeleteNotification(notificationRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Notification deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Notification not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
