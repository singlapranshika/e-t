
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event_Attendees_Tracker_DAL.DBOperations;
using System.Diagnostics;

namespace Event_Attendees_Tracker_BAL.util
{
    public class EventRegistration : IEventRegistration
    {
        private readonly IMailSend _mailSend;

        public EventRegistration(IMailSend mailSend)
        {
            _mailSend = mailSend;
        }

        public List<String> InsertTblRegisteredStudents(DataTable StudentRegistrationData, int EventID, string EventName)
        {
            EventRegistrationDAL eventRegistration = new EventRegistrationDAL();
            List<String> studentsInsertReturnValue = eventRegistration.InsertTblRegisteredStudents(StudentRegistrationData);
            if (studentsInsertReturnValue.Count > 0)
            {
                var AttenddesInsertList = eventRegistration.InsertTblEventAttendees(studentsInsertReturnValue,EventID);
                if (AttenddesInsertList.Count > 0)
                {
                    _mailSend.SendRegistrationMail(AttenddesInsertList,EventID);
                    Debug.Print("Succesfully added data into Attendees Table");
                }
                else
                {
                    Debug.Print("Could not insert data into Attendees Table");
                }
            }
            else
            {
                Debug.Print("Could not insert data into Registration Table");
            }
            return studentsInsertReturnValue;
        }
    }
}
