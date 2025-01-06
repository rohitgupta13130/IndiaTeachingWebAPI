using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;

namespace IndiaTeachingWebAPI.Controllers
{
    public class LoginController : ApiController
    {
        string _loginController = "LoginController";

        [HttpGet]
        public HttpResponseMessage Login()
        {
            try
            {
                List<UserTypes> userTypes = new UserTypesDAL().GetUsers(new UserTypesRequest());
                Users users = new Users();

                if (userTypes != null && userTypes.Count > 0)
                {
                    users.lst = new List<UserTypes>
                    {
                        new UserTypes { UserTypeId = 0, UserTypeName = "Select" }
                    };
                    users.lst.AddRange(userTypes);
                }

                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetUsers", _loginController, "Users", ex.Message, DateTime.Now.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }




        [HttpPost]
        //[Route("api/Login/Verify")]
        public HttpResponseMessage LoginPost([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (loginRequest == null || string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.UserPassword))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid login request.");
                }

                Users user = new LoginDAL().GetUsers(loginRequest);

                if (user != null && user.UserName == loginRequest.UserName && user.UserPassword == loginRequest.UserPassword)
                {
                    Guid guid = Guid.NewGuid();
                    bool isUserValidated = new LoginDAL().SaveUserSession(user.UserId, guid);

                    if (isUserValidated)
                    {
                        var userId = user.UserId;

                        // Build the response object with session details
                        var responseContent = new
                        {
                            userId = user.UserId,
                            UserRole = user.UserType,
                            UserSession = guid
                        };

                        return Request.CreateResponse(HttpStatusCode.OK, responseContent);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to validate user session.");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}

               