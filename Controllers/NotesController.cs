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
    public class NotesController : ApiController
    {
        // GET: api/Notes


        public HttpResponseMessage GetNotes()
        {
            try
            {
                List<Notes> notes = new NotesDAL().GetNotesList(new NotesRequest());
                return Request.CreateResponse(HttpStatusCode.OK, notes);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        
    }
}
