using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Event_Attendees_Tracker_API.Controllers;
using Event_Attendees_Tracker_BAL.User_Actions;
using Event_Attendees_Tracker_DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Event_Attendees_Tracker_API.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void FetchEventsTest()
        {
            IList<EventDetails> details = new List<EventDetails>
            {
                new EventDetails {ID = 1, EventName = ".Net", Description = "Basics of .net", Venue = "Hyderabad"},
                new EventDetails {ID = 1, EventName = ".Net", Description = "Basics of .net", Venue = "Hyderabad"}
            };
            Mock<IEvents> repository=new Mock<IEvents>();
            repository.Setup(x => x.fetchActiveEvents(It.IsAny<int>())).Returns((int i) => details.Where(x => x.ID == i).ToList());
            var controller = new UserController(repository.Object);
            IHttpActionResult actionResult = controller.FetchActiveEvents(1);
            var contentResult = actionResult as NegotiatedContentResult<System.Collections.Generic.List<EventDetails>>;

            // Assert
            Assert.IsNotNull(contentResult);

        }
    }
}
