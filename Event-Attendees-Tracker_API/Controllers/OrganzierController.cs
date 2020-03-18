using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Event_Attendees_Tracker_BAL.User_Actions;
using System.Web.Http;

namespace Event_Attendees_Tracker_API.Controllers
{
    public class OrganzierController : ApiController
    {
        public IQueryable GetDetailsOfStudents(int? EventId)
        {
            StudentListForTable studentList = new StudentListForTable();
            var responseData = studentList.fetchAttendeesData(Convert.ToInt32(EventId));
            return responseData;
        }
    }
}
