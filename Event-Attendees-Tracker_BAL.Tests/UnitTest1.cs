using System;
using System.Collections.Generic;
using System.Linq;
using Event_Attendees_Tracker_BAL.User_Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Event_Attendees_Tracker_DAL.Models;
using Moq;

namespace Event_Attendees_Tracker_BAL.Tests
{
    [TestClass]
    public class UnitTest1
    {
       public UnitTest1()
        {
            IList<EventDetails> details = new List<EventDetails>
            {
                new EventDetails {ID = 1, EventName = ".Net", Description = "Basics of .net", Venue = "Hyderabad"},
                new EventDetails {ID = 1, EventName = ".Net", Description = "Basics of .net", Venue = "Hyderabad"}
            };

            Mock<IEvents> fetchActive = new Mock<IEvents>();
            fetchActive.Setup(m => m.fetchActiveEvents(It.IsAny<int>()))
                .Returns((int i) => details.Where(x => x.ID == i).ToList());
            this.MockRepository = fetchActive.Object;
        }
        public TestContext TestContext { get; set; }
        public readonly IEvents MockRepository;
        
        [TestMethod]
        public void TestMethod1()
        {
           
            
            List<EventDetails> d = this.MockRepository.fetchActiveEvents(1);
            Assert.IsNotNull(d);
            Assert.IsInstanceOfType(d, typeof(List<EventDetails>));
        }
    }
}
