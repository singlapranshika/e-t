using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event_Attendees_Tracker_DAL.Models;
namespace Event_Attendees_Tracker_DAL.DBQueries
{
    public interface IFetchEvents
    {
        List<EventDetails> GetActiveEvents(int userId);
       // bool MarkStudentAttendance(String QRString);
    }
}
