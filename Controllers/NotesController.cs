using India_Teaching.CustomAuthenticationFilter;
using India_Teaching.DAL;
using India_Teaching.Models;
using India_Teaching.Request;
using IndiaTechingClassLibray.DAL;
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

        [HttpGet]
        public HttpResponseMessage GetNotes(string notesTitle = null)
        {
            try
            {
                NotesRequest notesRequest = new NotesRequest() { Title = notesTitle };
                List<Notes> notes = new NotesDAL().GetNotesList(notesRequest);

                if (notes == null)
                {
                    notes = new List<Notes>();
                }
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
