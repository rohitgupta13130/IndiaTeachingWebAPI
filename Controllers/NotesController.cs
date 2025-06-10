using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
using IndiaTechingClassLibray.Models;
using IndiaTechingClassLibray.Request;
using Newtonsoft.Json;
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
        string _NotesController = "NotesController";

        [HttpGet]
        public HttpResponseMessage GetNotes([FromUri] NotesRequest notesRequest)
        {
            try
            {
                
                List<Notes> notes = new NotesDAL().GetNotesList(notesRequest ?? new NotesRequest());

                if (notes == null)
                {
                    notes = new List<Notes>();
                }
                return Request.CreateResponse(HttpStatusCode.OK, notes);
            }
            catch (Exception ex)
            {
                new LogsDAL().SaveLogs("GetNotes", _NotesController, "Notes", ex.Message, DateTime.Now.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Get: api/Notes?Id = 5
       
        [HttpGet]
        [Route("api/Notes")]
        public HttpResponseMessage GetNote([FromUri] NotesRequest notesRequest)
        {
            try
            {
                if (notesRequest == null || notesRequest.Id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Notest request");
                }
                Notes notes = new NotesDAL().GetNotes(notesRequest);

                if (notes == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Notes not found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, notes);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        //POST: api/notes
       [HttpPost]
        public HttpResponseMessage SaveNotes([FromBody] Notes notes, HttpPostedFileBase file)
        {
            try
            {
                int notesId = new NotesDAL().SaveNotes(notes, file);
                return Request.CreateResponse(HttpStatusCode.OK, notesId);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //[HttpPost]
        //public HttpResponseMessage SaveNotes()
        //{
        //    try
        //    {
        //        var httpRequest = HttpContext.Current.Request;
        //        var file = httpRequest.Files["file"];
        //        var notes = JsonConvert.DeserializeObject<Notes>(httpRequest.Form["notes"]);

        //        HttpPostedFileBase fileBase = file != null ? new HttpPostedFileWrapper(file) : null;

        //        int notesId = new NotesDAL().SaveNotes(notes, fileBase);
        //        return Request.CreateResponse(HttpStatusCode.OK, notesId);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}


        // DELETE: api/Notes/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID.");
                }

                NotesRequest notesRequest = new NotesRequest { Id = id };
                bool isDeleted = new NotesDAL().DeleteNotes(notesRequest);

                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Notes deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Notes not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }




    }
}
