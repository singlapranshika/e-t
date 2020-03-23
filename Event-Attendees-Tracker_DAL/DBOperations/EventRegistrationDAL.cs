using System;
using System.Collections.Generic;
using System.Data;
using Event_Attendees_Tracker_DAL.Instances;
using Event_Attendees_Tracker_DAL.Models;
using System.Diagnostics;
using Event_Attendees_Tracker_DAL.DBQueries;
using System.Linq;

namespace Event_Attendees_Tracker_DAL.DBOperations
{
    public class EventRegistrationDAL
    {
        static Event_Attendees_Tracker_DAL.Database_Context.EAT_DBContext _eatDBContext = DBInstance.getDBInstance();
        public Dictionary<string, string> InsertTblRegisteredStudents(DataTable studentsData)
        {
            var StudentList = new Dictionary<string, string>();

            foreach (DataRow student in studentsData.Rows)
            {
                string EmailID = student.Field<string>("EmailID");
                if (CheckStudentExists.CheckStudent(EmailID))
                {
                    RegisteredStudents registeredStudents = null;
                    try
                    {
                        string collegeName = student.Field<string>("CollegeName");
                        registeredStudents = new RegisteredStudents
                        {
                            FirstName = student.Field<string>("FirstName"),
                            LastName = student.Field<string>("LastName"),
                            ContactNumber = student.Field<string>("ContactNumber"),
                            EmailID = student.Field<string>("EmailID"),
                            StudentRollNumber = student.Field<string>("StudentRollNo"),
                            CollegeDetails =
                                _eatDBContext.Master_CollegeDetails.Single(m => m.CollegeName.Equals(collegeName)),
                            CreatedBy = 1, //Update with user sessionID
                            CreatedDate = DateTime.Now
                        };
                        _eatDBContext.RegisteredStudents.Add(registeredStudents);
                        int temp = _eatDBContext.SaveChanges();
                        if (!StudentList.ContainsKey(EmailID))
                        {
                            StudentList.Add(EmailID, "Successfully Inserted Data");
                        }
                    }
                    catch (Exception)
                    {
                        if (!StudentList.ContainsKey(EmailID))
                        {
                            StudentList.Add(EmailID, "Invalid Data");

                        }
                        _eatDBContext.RegisteredStudents.Remove(registeredStudents);

                    }

                }
                else
                {
                    if (!StudentList.ContainsKey(EmailID))
                    {
                        StudentList.Add(EmailID, "Duplicate Data");
                    }
                }
            }

            return StudentList;
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
