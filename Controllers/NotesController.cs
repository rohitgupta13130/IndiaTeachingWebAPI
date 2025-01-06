using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IndiaTeachingWebAPI.Controllers
{
    [CustomAuthenticationFilter]
    public class NotesController : ApiController
    {
        // GET: api/Notes

        [HttpGet]
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

        //Get: api/Notes/2
        [HttpGet]
        public HttpResponseMessage GetNotes(int id)
        {
            try
            {
                Notes notes = new NotesDAL().GetNotes(new NotesRequest() { Id = id });
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //post : api/notes
        //[HttpPost]
        //public HttpRequestMessage SaveNotes([FromBody] Notes notes , HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        int notesId = new NotesDAL().SaveNotes(notes , file);
        //        return Request.CreateResponse(HttpStatusCode.OK, notesId);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}








    }
}
