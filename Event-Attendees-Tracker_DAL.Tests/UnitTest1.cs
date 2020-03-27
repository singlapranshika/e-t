using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using Event_Attendees_Tracker_DAL.DBQueries;
using Moq;
using Event_Attendees_Tracker_DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Event_Attendees_Tracker_DAL.Tests
{
    [TestClass]
    public  class UnitTest1
    {
        public UnitTest1()
        {
            IList<EventDetails> details = new List<EventDetails>
            {
                new EventDetails {ID = 1, EventName = ".Net", Description = "Basics of .net", Venue = "Hyderabad"},
                new EventDetails {ID = 1, EventName = ".Net", Description = "Basics of .net", Venue = "Hyderabad"}
            };
            //IList<EventAttendees> attendee = new List<EventAttendees>
            //{
            //    new EventAttendees {ID = 1, QRString = "12324"}
            //};
            //Mock<EAT_DBContext> context = new Mock<EAT_DBContext>();
            //context.Setup(x=>x.)
           Mock<IFetchEvents> mockEvent = new Mock<IFetchEvents>();
            // Mock<EventAttendees>mockAttendee=new Mock<EventAttendees>();
            mockEvent.Setup(m => m.GetActiveEvents(It.IsAny<int>()))
                .Returns((int i) => details.Where(x => x.ID == i).ToList());
            this.MockRepository = mockEvent.Object;
        }

        public TestContext TestContext { get; set; }
       
      
    public readonly IFetchEvents MockRepository;
       [TestMethod]
        public void GetEventsTest()
        {
            
           List<EventDetails> d = this.MockRepository.GetActiveEvents(1);
           Assert.IsNotNull(d);
           Assert.IsInstanceOfType(d,typeof(List<EventDetails>));
           // Assert.AreEqual(".Net",d.EventName);
        }

        //public bool MarkAttendanceTest()
        //{

        //}

    }
}
