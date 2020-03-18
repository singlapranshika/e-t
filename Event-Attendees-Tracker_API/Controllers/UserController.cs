//System namespace Imports
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using Antlr.Runtime.Tree;

//Custom Namespace Imports
using Event_Attendees_Tracker_API.Models;
using Event_Attendees_Tracker_BAL.User_Actions;


namespace Event_Attendees_Tracker_API.Controllers
{

    public class UserController : ApiController
    {
        private readonly IEvents _events;

        public UserController(IEvents events)
        {
            _events = events;
        }

        public HttpResponseMessage CreateEvent(EventModel requestEventData)
        {
            try
            {
                var response = _events.AddEvent(requestEventData.Name, requestEventData.Description,
                    requestEventData.Venue, requestEventData.PosterImagePath, requestEventData.StartTime,
                    requestEventData.EndTime, requestEventData.EventDate, requestEventData.AttendeesDataTable,
                    requestEventData.CreatedBy);
                if (!response) return Request.CreateResponse(HttpStatusCode.BadRequest, "Check the Data");
            }


            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error");
            }


            return Request.CreateResponse(HttpStatusCode.Created, new {Message = "Successfully Created!"});
        }

        [HttpGet]
        public IHttpActionResult FetchActiveEvents(int userId)
        {
            try
            {
                return Content(HttpStatusCode.OK, _events.fetchActiveEvents(userId));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new {error = ex.Message});
                throw;
            }
        }

        
    }
}
