using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Event_Attendees_Tracker_DAL.Instances;
using Event_Attendees_Tracker_DAL.Models;
using System.Diagnostics;
using Event_Attendees_Tracker_DAL.DBQueries;

namespace Event_Attendees_Tracker_DAL.DBOperations
{
    public class EventRegistrationDAL
    {
        static Event_Attendees_Tracker_DAL.Database_Context.EAT_DBContext _eatDBContext = DBInstance.getDBInstance();
        public List<String> InsertTblRegisteredStudents(DataTable StudentsData)
        {
            List<String> StudentList = new List<string>();
            try
            {
                foreach (DataRow student in StudentsData.Rows)
                {
                    String EmailID = student.ItemArray[3].ToString();
                    if (CheckStudentExists.CheckStudent(EmailID))
                    {
                        String CollegeName = student.ItemArray[5].ToString();
                        _eatDBContext.RegisteredStudents.Add(new RegisteredStudents
                        {
                            FirstName = student.ItemArray[0].ToString(),
                            LastName = student.ItemArray[1].ToString(),
                            ContactNumber = student.ItemArray[2].ToString(),
                            EmailID = student.ItemArray[3].ToString(),
                            StudentRollNumber = student.ItemArray[4].ToString(),
                            CollegeDetails = _eatDBContext.Master_CollegeDetails.Where(m => m.CollegeName.Equals(CollegeName)).FirstOrDefault(),
                            CreatedBy = 1,//Update with user sessionID
                            CreatedDate = DateTime.Now
                        });
                    }
                    StudentList.Add(EmailID);
                }
                _eatDBContext.SaveChanges();
                return StudentList;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return null;
            }



        }
        public List<String> InsertTblEventAttendees(List<String> StudentList, int EventID)
        {
            List<String> EmailList = new List<string>();
            try
            {
                foreach (var EmailID in StudentList)
                {
                    if (CheckStudentExists.CheckAttendee(EmailID, EventID))
                    {
                        EmailList.Add(EmailID);
                        _eatDBContext.EventAttendees.Add(
                        new EventAttendees
                        {
                            RegisteredStudents = _eatDBContext.RegisteredStudents.Where(m => m.EmailID == EmailID).FirstOrDefault(),
                            EventDetails = _eatDBContext.EventDetails.Where(m => m.ID == EventID).FirstOrDefault(),
                            QRString = "",
                            isPresent = false,
                            MailSent = false
                        });
                    }

                }
                _eatDBContext.SaveChanges();
                return EmailList;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
